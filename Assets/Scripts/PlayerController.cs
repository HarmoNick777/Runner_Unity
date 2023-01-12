using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 10.0f;
    [SerializeField] float gravityModifier = 1.5f;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem dirtParticle;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip crashSound;
    public bool gameOver;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private bool isOnGround;
    
    //Player Input Behavior: Send Messages
    /*void OnJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }*/

    //Player Input Behavior: Invoke Unity Events
    public void JumpPlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            //jumpSound.Play;
        }
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        isOnGround = true;
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
        }
    }
}
