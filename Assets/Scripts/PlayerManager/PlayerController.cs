using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Collider2D col;
    public Animator animator;

    [Header("Movement")]
    public float movementSpeedForward = 1.0f;
    public float movementSpeedBack = 1.0f;
    private bool isRunning = false;
    

    [Header("GroundCheck")]
    public Transform groundCheckCollider;
    const float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isFalled = false;

    //double jump
    [Header("Jump")]
    public bool doubleJump;
    public bool isGrounded;
    private bool isJumping = false;
    public float jumpForce = 4.0f;
    //health
    private Health health;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(moveX * movementSpeedForward, rigid.velocity.y);

        if (moveX != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        animator.SetBool("isRunning", isRunning);
        //flip X
        if (moveX > 0)
        {
            rigid.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        //Checks if Left Key is pressed
        else if (moveX < 0)
        {
            rigid.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
    private void Jump()
    {
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = true;
            if (isGrounded)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                doubleJump = false;
                
            }
        }
        else
        {
            isJumping = false;
        }

        animator.SetBool("isJumping", isJumping);
    }

    private void Fall()
    {
        if(rigid.velocity.y < 0)
        {
            isFalled = true;
        }
        else
        {
            isFalled = false;
        }
        animator.SetBool("isFalled", isFalled);
    }
    void GroundCheck()
    {
        bool canJump = Physics2D.OverlapCircle(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(canJump)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //nếu quái va chạm với nhân vật thì nhân vật mất máu
        /*Health playerHealth = GetComponent<Health>();
        if (collision.gameObject.tag == "monster_hitbox")
        {
            Debug.Log("Hitted");
            playerHealth.TakeDamage(1);
            animator.SetTrigger("hit");
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health playerHealth = GetComponent<Health>();
        if (collision.gameObject.tag == "monster_hitbox")
        {
            Debug.Log("Hitted");
            playerHealth.TakeDamage(1);
            animator.SetTrigger("hit");
        }
    }
}
