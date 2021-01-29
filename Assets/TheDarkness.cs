using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDarkness : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public int distance = 1;
    float maxDistance;
    float minDistance;
    float speed = 1f;
    private bool isFacingRight = true;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        maxDistance = transform.position.x + distance;
        minDistance = transform.position.x - distance;
    }

    private void FixedUpdate()
    {
          Walk();
    }

    private void Update()
    {

    }

    private void Walk()
    {
        transform.Translate(transform.right * Time.deltaTime * speed);
        anim.SetInteger("Darkness", 1);

        if (transform.position.x > maxDistance)
        {
            speed = -speed;

            transform.localScale *= new Vector2(-1, 1);
            isFacingRight = !isFacingRight;
        }
        else if (transform.position.x < minDistance)
        {
            speed = -speed;

            transform.localScale *= new Vector2(-1, 1);
            isFacingRight = !isFacingRight;
        }
    }
}
