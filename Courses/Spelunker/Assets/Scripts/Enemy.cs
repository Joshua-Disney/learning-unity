using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private float horizontalBound = 25.0f;
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        
        
        if (transform.position.x > horizontalBound) {
            Destroy(gameObject);
        } else if (transform.position.x < -horizontalBound) {
            Destroy(gameObject);
        } else if (transform.position.z > horizontalBound) {
            Destroy(gameObject);
        } else if (transform.position.z < -horizontalBound) {
            Destroy(gameObject);
        }
    }
}
