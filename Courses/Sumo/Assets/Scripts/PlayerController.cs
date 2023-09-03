using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpForce = 15.0f;
    public float speed = 2.0f;
    public bool isPoweredUp = false;
    public GameObject powerupIndicator;
    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject missilePrefab;
    private GameObject tmpMissile;
    private Coroutine powerUpCountdown;
    public float hangTime;
    public float slamSpeed;
    public float explosionForce;
    public float explosionRadius;
    bool slamming = false;
    float floorY;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        
        if (currentPowerUp == PowerUpType.Missile && Input.GetKeyDown(KeyCode.F)) {
            LaunchMissiles();
        }

        if (currentPowerUp == PowerUpType.Slam && Input.GetKeyDown(KeyCode.Space) && slamming == false) {
            slamming = true;
            StartCoroutine(Slam());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup")) {
            isPoweredUp = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

            if (powerUpCountdown != null) {
                StopCoroutine(powerUpCountdown);
            }
            powerUpCountdown = StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    IEnumerator PowerUpCountdownRoutine() {
        yield return new WaitForSeconds(7);
        isPoweredUp = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator Slam() {
        var enemies = FindObjectsOfType<Enemy>();
        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime) {
            playerRb.velocity = new Vector2(playerRb.velocity.x, slamSpeed);
            yield return null;
        }
        while (transform.position.y < floorY) {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -slamSpeed * 2);
            yield return null;
        }
        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i] != null) {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }
        slamming = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Repel) {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
        }
    }

    void LaunchMissiles () {
        foreach (var enemy in FindObjectsOfType<Enemy>()) {
            tmpMissile = Instantiate(missilePrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpMissile.GetComponent<MissileBehavior>().Fire(enemy.transform);
        }
    }
}