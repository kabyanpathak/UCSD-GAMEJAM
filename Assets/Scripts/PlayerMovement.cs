using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpStrength;
    public Vector2 RespawnPoint;

    private Rigidbody2D playerRB;
    private Collider2D playerCollider;
    private SpriteRenderer playerSprite;
    private Animator playerAnimator;

    public bool isGrounded;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<Collider2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        playerAnimator.SetBool("Running", isGrounded && playerRB.velocity.magnitude > 0.01f);
        playerAnimator.SetBool("Is Grounded", isGrounded);
    }

    void FixedUpdate()
    {
        // if (Input.GetKey(KeyCode.D))
        // {
        //     playerRB.velocity = new Vector2(Speed, playerRB.velocity.y);
        //     playerSprite.flipX = false;
        // }
        // else if (Input.GetKey(KeyCode.A))
        // {
        //     playerRB.velocity = new Vector2(-Speed, playerRB.velocity.y);
        //     playerSprite.flipX = true;
        // }
        // else if (isGrounded)
        // {
        //     playerRB.velocity = new Vector2(playerRB.velocity.x * 0.75f, playerRB.velocity.y);
        // }

        // if (isGrounded && Input.GetKey(KeyCode.Space))
        // {
        //     playerRB.velocity = new Vector2(playerRB.velocity.x, JumpStrength);
        // }
        
        if (isGrounded)
        {
            playerRB.velocity = new Vector2(Speed, playerRB.velocity.y);
        }

        if (transform.position.y < -10.0f)
        {
            transform.position = new Vector3(RespawnPoint.x, RespawnPoint.y);
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
            case "Flag":
                transform.position = new Vector3(RespawnPoint.x, RespawnPoint.y);
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
