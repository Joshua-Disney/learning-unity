using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float verticalInput;
    public float horizontalInput;
    public float speed = 5.0f;
    private float playerBound = 23.0f;
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
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Debug.Log("A zombie got you!  RIP");
        }
    }
}
