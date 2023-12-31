﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrabbyController : MonoBehaviour
{
    bool collide = false;
    private Rigidbody2D rb;
    public Animator ani;
    private bool hitted;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("Collide:" + collide);*/
        if (collide)
        {
            transform.Translate(Time.deltaTime * 0.5f, 0, 0);
            rb.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.Translate(-Time.deltaTime * 0.5f, 0, 0);
            rb.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //quái vật di chuyển trong phạm vi cho phép
        if(collision.gameObject.tag == "left_limit")
        {
            collide = true;
        }
        else if(collision.gameObject.tag == "right_limit")
        {
            collide = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player_attackarea")
        {
            hitted = true; 
        }
        else
        {
            hitted = false;
        }
        ani.SetBool("hit", hitted);
        rb.AddForce(new Vector2(0.5f, 0));
    }
}
