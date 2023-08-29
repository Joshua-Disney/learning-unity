using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;
    private float missileStrength = 15.0f;
    private float aliveTimer = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if(homing && target != null) {
            Vector3 moveDirection = (target.transform.position -
            transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }
    public void Fire(Transform newTarget) {
        target = homingTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    void OnCollisionEnter(Collision collision) {
        if (target != null) {
            if (collision.gameObject.CompareTag(target.tag)) {
                Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRigidbody.AddForce(away * missileStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
