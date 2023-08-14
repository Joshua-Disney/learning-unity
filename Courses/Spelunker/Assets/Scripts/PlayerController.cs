using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float verticalInput;
    public float horizontalInput;
    public float speed = 5.0f;
    private float playerBound = 24.5f;
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > playerBound) {
            transform.position = new Vector3(playerBound, transform.position.y, transform.position.z);
        } else if (transform.position.x < -playerBound) {
            transform.position = new Vector3(-playerBound, transform.position.y, transform.position.z);
        } else if (transform.position.z > playerBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, playerBound);
        } else if (transform.position.z < -playerBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -playerBound);
        }

        
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(move * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Debug.Log("A zombie got you!  RIP");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Treasure")) {
            Destroy(other.gameObject);
            Debug.Log("A piece of treasure was found!");
        }
    }
}
