using JetBrains.Annotations;
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
    public Animator animator;

    //check is grounded
    public bool isGrounded = false;
    private bool isJumping = false;
    public Transform groundCheckCollider;
    const float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    //falling and grounded
    private bool isFalled = false;
    //attack
    [Header("Attack")]
    private bool canAttack = true;
    public int attackCount = 0;
    IEnumerator timeBetweenAttacks;
    //double jump
    [Header("Double Jump")]
    public int jumpCount = 0; //số lần nhảy đã thực hiện
    public int maxJumpCount = 2;//số lần nhảy tối đa cho phép 

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canAttack = true;
        timeBetweenAttacks = ResetCanAttack();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
        Fall();
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
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

        if (isGrounded)
        {
            jumpCount = 0; // Reset jumpCount khi đứng trên mặt đất
            isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && (isGrounded || jumpCount < maxJumpCount))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpCount++;

            // Đánh dấu nhân vật đang trong trạng thái nhảy
            isJumping = true;
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
    void Attack()
    {
        if (canAttack)
        {
            StopCoroutine(timeBetweenAttacks);
            attackCount++;
            canAttack = false;
            Invoke("DelayCanAttack", 0.5f);
            if(attackCount == 1)
            {
                animator.SetTrigger("attack1");
            }
            if(attackCount == 2)
            {
                animator.SetTrigger("attack2");
            }
            if(attackCount == 3)
            {
                animator.SetTrigger("attack3");
            }
        }
    }
    IEnumerator ResetCanAttack()
    {
        yield return new WaitForSeconds(0.5f);
        attackCount = 0;
    }
    void DelayCanAttack()
    {
        timeBetweenAttacks = ResetCanAttack();
        StartCoroutine(timeBetweenAttacks);
        canAttack = true;
        animator.SetTrigger("returnIdle");
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
}
