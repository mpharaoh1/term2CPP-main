using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer marioSprite;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public bool isAttacking;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        marioSprite = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please assign a ground check object");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }
        else 
        {
            anim.SetBool("isAttacking", true);
            isAttacking = true;
        }

        if (Input.GetButton("Fire2")&& Input.GetButton("Jump"))
        {

            anim.SetBool("isAttacking", false);
            anim.SetBool("JumpAttack", false);
            isAttacking = false;
        }
        else
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool("JumpAttack", true);
            isAttacking = true;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);

        if (marioSprite.flipX && horizontalInput > 0 || !marioSprite.flipX && horizontalInput < 0)
            marioSprite.flipX = !marioSprite.flipX;

    }
}
