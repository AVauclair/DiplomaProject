using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool ground;
    float dirX;
    private bool isFacingRight = true;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    Transform tr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
    }

    //Input.GetAxis для оси Х. Возвращает значение оси в пределах от -1 до 1.
    //при стандартных настройках проекта
    //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
    //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D).

    private void FixedUpdate()
    {
        Walk();

        if (Input.GetAxis("Horizontal") != 0 && ground == true)
        {
            anim.SetInteger("anim", 1);
        }
        if (Input.GetAxis("Horizontal") == 0 && ground == true)
        {
            anim.SetInteger("anim", 0);
        }
        Flip();
        Run();
    }

    private void Update()
    {
        Jump();
    }

    private void Walk()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
        dirX = Input.GetAxis("Horizontal");
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && ground == true)
        {
            rb.velocity = new Vector2(dirX * 1.5f, rb.velocity.y); //1.5 - на сколько умножить скорость
            anim.SetFloat("Speed", Mathf.Abs(dirX * 1.5f));
        }
    }

    private void Flip()
    {
        if ((dirX > 0 && !isFacingRight) || (dirX < 0 && isFacingRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            isFacingRight = !isFacingRight;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ground == true)
        {
            anim.SetInteger("anim", 2);
            rb.AddForce(transform.up * 2.8f, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Respawn")
        {
            SceneManager.LoadScene(0);
        }
    }
}
