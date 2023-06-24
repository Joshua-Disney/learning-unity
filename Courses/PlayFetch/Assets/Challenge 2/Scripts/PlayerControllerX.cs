using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    // [Range(0f,2f)]
    private float timer = 2.0f;
    private float waitTime = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // Delay Input Timer - only execute the spacebar command if timer has caught up with waitTime
        if (timer < waitTime)
        {}
        // On spacebar press, send dog
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            // Resets Timer
            timer = 0;
        }
        // Run Timer every frame
        timer += Time.deltaTime;
    }
}
