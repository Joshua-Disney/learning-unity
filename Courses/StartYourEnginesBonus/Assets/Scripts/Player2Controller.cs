using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{

    // variables
    public float speed = 20.0f;
    public float turnSpeed = 100.0f;
    public float forwardInput;
    public float horizontalInput;
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set inputs
        forwardInput = Input.GetAxis("VerticalP2");
        horizontalInput = Input.GetAxis("HorizontalP2");
        // Move the Player forward and turn based on input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        // Switch enabled Camera
        if(Input.GetKeyDown(switchKey)) {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}
