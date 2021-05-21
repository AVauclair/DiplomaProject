using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    public bool ground;
    public bool topDetector;

    public int maxHp = 100;
    public int hp;

    public int souls = 0;

    public int havingKey = 0;
    public int havingWarriorSoul = 0;

    float dirX;
    public float speedUp = 1f;

    private bool afterJump = false;
    public bool isFacingRight = true;
    public bool isWalking = false;

    public bool isDead = false;
     
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Transform tr;

    private ScriptTrigger scriptTrigger;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();

        FindObjectOfType<CheckpointStartValues>().CheckpointCheck();
        FindObjectOfType<CheckpointStartValues>().CheckpointStart();
        FindObjectOfType<ConditionScript>().ConditionsChecker();

        hp = maxHp;
    }

    //Input.GetAxis для оси Х. Возвращает значение оси в пределах от -1 до 1.
    //при стандартных настройках проекта
    //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
    //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D).

    private void FixedUpdate()
    {
        if (FindObjectOfType<DialogManager>().checker != null)
        {
            if (FindObjectOfType<DialogManager>().checker.GetComponent<SelectDialog>().inDialog == false)
            {
                Walk();
                Flip();
                Run();
                Crawl();
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                anim.SetBool("isRun", false);
                anim.SetBool("isJump", false);
                anim.SetBool("isCrawl", false);
                anim.SetBool("ground", true);
            }
        }
        else
        {
            Walk();
            Flip();
            Run();
            Crawl();
        }
    }

    private void Update()
    {
        DeleteAllPrefs();

        if (FindObjectOfType<DialogManager>().checker != null)
        {
            if (FindObjectOfType<DialogManager>().checker.GetComponent<SelectDialog>().inDialog == false)
            {
                anim.SetBool("ground", ground);
                Move();
                Jump();
                Push();
                ShowInterface();
                OpenMenu();
                if (rb.velocity.y == 0 && afterJump)
                {
                    anim.SetBool("isJump", false);
                    afterJump = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                anim.SetBool("isRun", false);
                anim.SetBool("isJump", false);
                anim.SetBool("isCrawl", false);
                anim.SetBool("ground", true);
            }
        }
        else
        {
            anim.SetBool("ground", ground);
            Move();
            Jump();
            Push();
            ShowInterface();
            OpenMenu();
            if (rb.velocity.y == 0 && afterJump)
            {
                anim.SetBool("isJump", false);
                afterJump = false;
            }
        }
    }

    public float jumpForce = 2.8f;
    public int jumpCount = 0;
    public int maxJumpValue = 1;
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCrawling && (ground || (jumpCount < maxJumpValue)))
        {
            //if (jumpCount > 0)
            //{
            //    rb.velocity = new Vector2(dirX, 0);
            //}
            jumpCount++;
            afterJump = true;

            anim.SetBool("isRun", false);
            anim.SetBool("isJump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //один из методов установки прыжка
            //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); //один из методов установки


            StartCoroutine(ZeroizeJumpCount());
        }
    }

    IEnumerator ZeroizeJumpCount()
    {
        yield return new WaitForSeconds(1f);

        if (ground == true)
        {
            jumpCount = 0;
        }
    }

    public void Walk()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("moveX", Mathf.Abs(dirX));
        rb.velocity = new Vector2(dirX * speedUp, rb.velocity.y);
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetAxis("Horizontal") != 0 && ground)
            {
                anim.SetBool("isRun", true);
                isWalking = true;
            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("isRun", false);
            isWalking = false;
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && ground && !isCrawling)
        {
            rb.velocity = new Vector2(dirX * speedUp * 1.5f, rb.velocity.y);
        }
    }

    public int pushImpulse = 500;
    private bool pushLock = false;
    private void Push()
    {
        if (Input.GetKeyDown(KeyCode.F) && !pushLock)
        {
            pushLock = true;
            PushLock(); //Invoke("PushLock", 2f);
            if (!isFacingRight)
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

    public Transform topCheck;
    public LayerMask Roof;
    public Collider2D poseStand;
    public Collider2D poseStand2;
    public Collider2D poseCrawl;
    private bool isCrawling = false;

    private void Crawl()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("isCrawl", true);
            poseStand.enabled = false;
            poseStand2.enabled = false;
            poseCrawl.enabled = true;

            speedUp = 0.65f;

            isCrawling = true;
        }
        else if (!topDetector && isCrawling)
        {
            anim.SetBool("isCrawl", false);
            poseStand.enabled = true;
            poseStand2.enabled = true;
            poseCrawl.enabled = false;

            speedUp = 1f;

            isCrawling = false;
        }
    }

    private void ShowInterface()
    {
        ShowEquipment();
        ShowInventory();
    }

    public Animator animEq;
    bool isEquipOpen = false;
    private void ShowEquipment()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isEquipOpen = !isEquipOpen;
            if (isEquipOpen == true)
            {
                animEq.SetBool("isOpen", true);
            }
            else
            {
                animEq.SetBool("isOpen", false);
            }
        }
        
    }

    public Animator animInventory;
    bool isInventoryOpen = false;
    private void ShowInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            if (isInventoryOpen == true)
            {
                animInventory.SetBool("isOpen", true);
            }
            else
            {
                animInventory.SetBool("isOpen", false);
            }
        }

    }

    private void DeleteAllPrefs()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "TheDarkness")
        {
            SceneManager.LoadScene(FindObjectOfType<LevelCount>().levelNumber);
        }

        if (other.tag == "SecretRoom" && FindObjectOfType<LevelCount>().levelNumber == 1)
        {
            Destroy(other.gameObject);
            FindObjectOfType<WatchPlayer>().rightLimit = 31.4f;
        }

        if (other.tag == "death")
        {
            SceneManager.LoadScene(FindObjectOfType<LevelCount>().levelNumber);
        }

        IEnumerator PillReturn()
        {
            yield return new WaitForSeconds(2f);

            other.gameObject.SetActive(true);
        }

        if (other.tag == "Pill")
        {
            other.gameObject.SetActive(false);
            jumpCount = 0;

            StartCoroutine(PillReturn());
        }

        if (other.tag == "EndTrigger")
        {
            FindObjectOfType<ConditionScript>().EndingWin.Play();
            Destroy(other);
            Destroy(GameObject.Find("StealthTrigger"));
            Destroy(GameObject.Find("EndTrigger"));
        }

        if (other.tag == "NextLevelTrigger")
        {
            PlayerPrefs.SetFloat("posX", FindObjectOfType<PlayerController>().transform.position.x);
            PlayerPrefs.SetFloat("posY", FindObjectOfType<PlayerController>().transform.position.y);
            PlayerPrefs.SetFloat("posZ", FindObjectOfType<PlayerController>().transform.position.z);
            FindObjectOfType<LevelCount>().levelNumber++;
            FindObjectOfType<ConditionScript>().sceneNumber++;
            PlayerPrefs.SetInt("levelNumber", FindObjectOfType<LevelCount>().levelNumber);
            PlayerPrefs.SetInt("sceneNumber", FindObjectOfType<ConditionScript>().sceneNumber);

            SceneManager.LoadScene(FindObjectOfType<LevelCount>().levelNumber);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "StealthTrigger" && isCrawling == false)
        {
            FindObjectOfType<ConditionScript>().EndingDeath.Play();
        }
    }
}
