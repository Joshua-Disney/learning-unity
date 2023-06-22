using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float resetTimer = 0;
    public bool dogCanRun = true;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            resetTimer += Time.deltaTime;
            if (dogCanRun)
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                dogCanRun = false;
            } else if (resetTimer > 2)
            {
                resetTimer = 0;
                dogCanRun = true;
            }
        }
    }
}
