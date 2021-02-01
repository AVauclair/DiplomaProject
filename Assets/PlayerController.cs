using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool ground;
    float dirX;
    public float speedUp = 1.5f;

    private bool afterJump = false;
    private bool isFacingRight = true;
    public bool isWalking = false;

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
        Flip();
        Run();
    }

    private void Update()
    {
        anim.SetBool("ground", ground);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetAxis("Horizontal") != 0 && ground == true)
            {
                anim.SetBool("runIsOn", true);
                isWalking = true;
            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("runIsOn", false);
            isWalking = false;
        }

        Jump();
        Push();

        if (rb.velocity.y == 0 && afterJump == true)
        {
            anim.SetBool("jumpIsOn", false);
            afterJump = false;
        }
    }

    public float jumpForce = 2.8f;
    private int jumpCount = 0;
    public int maxJumpValue = 1;
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (ground == true || (++jumpCount < maxJumpValue)))
        {
            if (jumpCount > 0)
            {
                rb.velocity = new Vector2(dirX, 0);
            }
            afterJump = true;

            anim.SetBool("runIsOn", false);
            anim.SetBool("jumpIsOn", true);
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce); //один из методов установки прыжка
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); //один из методов установки

            if (ground == true)
            {
                jumpCount = 0;
            }
        }
    }

    public void Walk()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * speedUp, rb.velocity.y);
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && ground == true)
        {
            rb.velocity = new Vector2(dirX * speedUp * 1.5f, rb.velocity.y);
        }
    }

    public int pushImpulse = 500;
    private bool pushLock = false;
    private void Push()
    {
        if (Input.GetKeyDown(KeyCode.F) && pushLock == false)
        {
            pushLock = true;
            PushLock(); //Invoke("PushLock", 2f);
            if (isFacingRight == false)
            {
                rb.AddForce(Vector2.left * pushImpulse); //первый параметр - в каком направлении подтолкнуть, второй - с какой силой
            }
            else
            {
                rb.AddForce(Vector2.right * pushImpulse);
            }
        }
    }

    private void PushLock()
    {
        pushLock = false;
    }

    private void Flip()
    {
        if ((dirX > 0 && !isFacingRight) || (dirX < 0 && isFacingRight))
        {
            //transform.Rotate(0.0f, 180.0f, 0.0f); //один из методов поворота персонажа
            transform.localScale *= new Vector2(-1, 1); //один из методов поворота персонажа
            isFacingRight = !isFacingRight;
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
