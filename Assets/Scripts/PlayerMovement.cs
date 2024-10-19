using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpStrength;

    private Rigidbody2D playerRB;
    private Collider2D playerCollider;
    private SpriteRenderer playerSprite;
    private Animator playerAnimator;

    private bool isGrounded;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<Collider2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        playerAnimator.SetBool("Running", isGrounded && playerRB.velocity.magnitude > 0.001f);
        playerAnimator.SetBool("Is Grounded", isGrounded);
    }

    void FixedUpdate()
    {
        /*
         * Temporary movement because eventually we want the character to move 
         * forward on their own while the player places objects in front of them.
         */
        if (Input.GetKey(KeyCode.D))
        {
            playerRB.velocity = new Vector2(Speed, playerRB.velocity.y);
            playerSprite.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerRB.velocity = new Vector2(-Speed, playerRB.velocity.y);
            playerSprite.flipX = true;
        }
        else if (isGrounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x * 0.9f, playerRB.velocity.y);
        }

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, JumpStrength);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.gameObject.tag)
        {
            case "Ground":
                isGrounded = true;
                break;
            case "Spring":
                Physics2D.IgnoreCollision(playerCollider, collision.collider);
                playerRB.velocity = new Vector2(playerRB.velocity.x, JumpStrength * 2.0f);
                Destroy(collision.gameObject);
                break;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.gameObject.tag)
        {
            case "Ground":
                isGrounded = false;
                break;
        }
    }
}
