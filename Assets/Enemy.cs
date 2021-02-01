using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public float speed = 5;
    public int i = 1;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Walk();
    }

    private void Update()
    {

    }

    private void Walk()
    {
        rb.velocity = new Vector2(i * speed, rb.velocity.y);
        anim.SetBool("isWalking", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "border")
        {
            if (i == 1)
            {
                i = -1;
            }
            else
            {
                i = 1;
            }
        }
    }
}
