using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpForce = 15.0f;
    // Public Variables
    public float speed = 2.0f;
    public bool isPoweredUp = false;
    public GameObject powerupIndicator;
    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject missilePrefab;
    private GameObject tmpMissile;
    private Coroutine powerupCountdown;
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

        if (currentPowerUp == PowerUpType.Missiles && Input.GetKeyDown(KeyCode.F)) {
            LaunchMissiles();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup")) {
            isPoweredUp = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            if(powerupCountdown != null) {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine() {
        yield return new WaitForSeconds(7);
        isPoweredUp = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Repel) {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + "with powerup set to " + currentPowerUp.ToString());
        }
    }

    void LaunchMissiles() {
        foreach(var enemy in FindObjectsOfType<Enemy>()) {
            tmpMissile = Instantiate(missilePrefab, transform.position, Vector3.up, Quaternion.identity);
            tmpMissle.GetComponent<MissiletBehavior>().Fire(enemy.transform);
        }
    }
}