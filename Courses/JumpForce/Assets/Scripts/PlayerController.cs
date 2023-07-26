using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float jumpForce = 350.0f;
    private AudioSource playerAudio;
    private Animator playerAnim;
    private Rigidbody playerRb;
    private MoveLeft moveLeft;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public float gravityModifier; 
    public bool gameOver = false;
    public AudioClip crashSound;
    public AudioClip jumpSound;
    public int jumpsLeft = 2;
    // Start is called before the first frame update
    void Start() {
        moveLeft = GameObject.Find("MoveLeft").GetComponent<MoveLeft>();
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; 
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)  && !gameOver && jumpsLeft > 0) {
            playerRb.AddForce(Vector3.up * jumpForce * jumpsLeft, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, .4f);
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            jumpsLeft -= 1;
        } else if (Input.GetKey(KeyCode.LeftShift) && !gameOver && jumpsLeft == 2) {
            // Code will go here increasing the speed variable from the MoveLeft script
            // Spent 40 minutes reading documents trying to figure out how to do this.
            moveLeft.speed = 30;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            dirtParticle.Play();
            jumpsLeft = 2;
        } else if (collision.gameObject.CompareTag("Obstacle") && !gameOver) {
            Debug.Log("Game Over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, .5f);
            jumpsLeft = 0;
            gameOver = true;
        }
    }
}
