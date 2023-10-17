using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Collider2D col;
    public float movementSpeedForward = 2.0f;
    public float movementSpeedBack = 2.0f;
    public float jumpForce = 4.0f;
    private bool isRunning = false;
    private Animator animator;

    //check is grounded
    public bool isGrounded = false;
    private bool isJumping = false;
    public Transform groundCheckCollider;
    const float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    //falling and grounded
    private bool isFalled = false;
    //attack
    private bool isAttacking = false;
    private bool attackDelay = true;
    //jump
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
        Fall();
        Attack();
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
        if(isGrounded)
        {
            isJumping = false;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            } 
        }
        else
        {
            isJumping = true;
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
    private void Attack()
    {
        if (attackDelay)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isAttacking = false;
                animator.SetTrigger("attack(trigger)");
                Invoke(nameof(SetAttackDelay), 1.0f);
                attackDelay = false;
            }
        }
        
        /*else
        {
            isAttacking = false;
        }*/
        
    }
    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRadius,groundLayer);
        if(colliders.Length > 0)//grounded
        {
            isGrounded = true;
        }
    }
    public void SetAttackDelay()
    {
        attackDelay = true;
    }
}
