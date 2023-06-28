using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables
    public float verticalInput;
    public float horizontalInput;
    public float speed = 15.0f;
    public float moveRange = 13.0f;
    public float zBottomBound = 0.0f;

    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.x < -moveRange) {
            transform.position = new Vector3(-moveRange, transform.position.y, transform.position.z);
        }
        
        if (transform.position.x > moveRange) {
            transform.position = new Vector3(moveRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < zBottomBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBottomBound);
        }

        if (transform.position.z > moveRange) {
            transform.position = new Vector3(transform.position.x, transform.position.y, moveRange);
        }
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // Launch a projectile from the player
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
