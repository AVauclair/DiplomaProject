using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public bool ground;
    public bool topDetector;

    public int maxHP = 100;
    public int hp;

    public bool inTrigger = false;

    public int souls = 0;

    public int havingKey = 0;
    public int havingWarriorSoul = 0;

    float dirX;
    public float speedUp = 1f;

    private bool afterJump = false;
    public bool isFacingRight = true;
    public bool isWalking = false;
    public bool highSpeed = false;

    public bool isDead = false;
     
    public Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Transform tr;

    private ScriptTrigger scriptTrigger;

    public TextMeshProUGUI hpValue;

    public GameObject dialogTrader2;
    public GameObject dialogTrader3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();

        FindObjectOfType<CheckpointStartValues>().CheckpointCheck();
        FindObjectOfType<CheckpointStartValues>().CheckpointStart();
        FindObjectOfType<ConditionScript>().ConditionsChecker();

        if (FindObjectOfType<LevelCount>().levelNumber > 1)
        {
            hp = maxHP;
            hpValue.text = hp.ToString();
        }
    }

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
                if (FindObjectOfType<LevelCount>().levelNumber > 1)
                {
                    anim.SetBool("HighSpeed", false);
                }
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
        DevKit();

        if (hp <= 0 && FindObjectOfType<LevelCount>().levelNumber != 1)
        {
            SceneManager.LoadScene(FindObjectOfType<LevelCount>().levelNumber);
        }

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
                if (FindObjectOfType<LevelCount>().levelNumber > 1)
                {
                    anim.SetBool("HighSpeed", false);
                }
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
            jumpCount++;
            afterJump = true;

            anim.SetBool("isRun", false);
            if (FindObjectOfType<LevelCount>().levelNumber > 1)
            {
                anim.SetBool("HighSpeed", false);
            }
            anim.SetBool("isCrawl", false);
            anim.SetBool("isJump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
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
            if (FindObjectOfType<LevelCount>().levelNumber > 1)
            {
                anim.SetBool("HighSpeed", false);
            }
            isWalking = false;
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (ground && !isCrawling)
            {
                if (FindObjectOfType<LevelCount>().levelNumber > 1 && isWalking == true)
                {
                    anim.SetBool("HighSpeed", true);
                    highSpeed = true;
                }

                rb.velocity = new Vector2(dirX * speedUp * 1.5f, rb.velocity.y);
            }
        }

        else
        {
            if (FindObjectOfType<LevelCount>().levelNumber > 1)
            {
                anim.SetBool("HighSpeed", false);
                highSpeed = false;
            }
        }
    }

    public int pushImpulse = 500;
    private bool pushLock = false;
    private void Push()
    {
        if (Input.GetKeyDown(KeyCode.F) && !pushLock)
        {
            pushLock = true;
            PushLock(); 
            //Invoke("PushLock", 0.5f);
            if (!isFacingRight)
            {
                rb.AddForce(Vector2.left * pushImpulse);
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
            transform.localScale *= new Vector2(-1, 1);
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

    public bool devJump = false;
    public bool devPush = false;
    private void DevKit()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.SetInt("checkpointNumber", 4);
            PlayerPrefs.Save();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            devJump = !devJump;
            if (devJump == true)
            {
                maxJumpValue = 2;
            }
            else
            {
                maxJumpValue = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            devPush = !devPush;
            if (devPush == true)
            {
                pushImpulse = 1000;
            }
            else
            {
                pushImpulse = 0;
            }
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
        if (other.tag != "ground")
        {
            inTrigger = true;
        }

        if (other.tag == "TheDarkness")
        {
            SceneManager.LoadScene(FindObjectOfType<LevelCount>().levelNumber);
        }

        if (other.tag == "SecretRoom" && FindObjectOfType<LevelCount>().levelNumber == 1)
        {
            Destroy(other.gameObject);
            FindObjectOfType<WatchPlayer>().rightLimit = 31.4f;
        }
        if (other.tag == "SecretRoom" && FindObjectOfType<LevelCount>().levelNumber == 3)
        {
            FindObjectOfType<WatchPlayer>().upLimit = 25f;
            FindObjectOfType<WatchPlayer>().downLimit = -20.45f;
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
            PlayerPrefs.SetFloat("posX", transform.position.x);
            PlayerPrefs.SetFloat("posY", transform.position.y);
            PlayerPrefs.SetFloat("posZ", transform.position.z);
            FindObjectOfType<LevelCount>().levelNumber++;
            FindObjectOfType<ConditionScript>().sceneNumber++;
            PlayerPrefs.SetInt("levelNumber", FindObjectOfType<LevelCount>().levelNumber);
            PlayerPrefs.SetInt("sceneNumber", FindObjectOfType<ConditionScript>().sceneNumber);
            
            if (FindObjectOfType<LevelCount>().levelNumber == 2)
            {
                PlayerPrefs.SetFloat("downLimit", FindObjectOfType<WatchPlayer>().downLimit = -8.92f);
                PlayerPrefs.SetFloat("upLimit", FindObjectOfType<WatchPlayer>().upLimit = 0.1f);

                PlayerPrefs.SetInt("havingWarriorSoul", havingWarriorSoul);
            }

            if (FindObjectOfType<LevelCount>().levelNumber == 3)
            {
                PlayerPrefs.SetFloat("downLimit", FindObjectOfType<WatchPlayer>().downLimit = -20.45f);
                PlayerPrefs.SetFloat("upLimit", FindObjectOfType<WatchPlayer>().upLimit = 12.8f);

                PlayerPrefs.SetInt("havingWarriorSoul", havingWarriorSoul);
                PlayerPrefs.SetInt("maxJumpValue", maxJumpValue);
                PlayerPrefs.SetInt("pushImpulse", pushImpulse);
                PlayerPrefs.SetInt("maxHP", maxHP);
                PlayerPrefs.SetInt("souls", souls);
            }
            SceneManager.LoadScene(FindObjectOfType<LevelCount>().levelNumber);
        }



        if (other.tag == "Trader1")
        {
            if (souls > 0)
            {
                souls--;
                maxHP += 20;
                hp = maxHP;
                FindObjectOfType<Souls>().textSouls.text = (souls + 1).ToString();
                hpValue.text = hp.ToString();
            }
        }

        if (other.tag == "Trader2")
        {
            if (havingWarriorSoul == 1)
            {
                havingWarriorSoul = 0;
                FindObjectOfType<ConditionScript>().imageSlot1.sprite = null;
                pushImpulse = 1000;
                Destroy(dialogTrader3);
            }
        }

        if (other.tag == "Trader3")
        {
            if (havingWarriorSoul == 1)
            {
                havingWarriorSoul = 0;
                FindObjectOfType<ConditionScript>().imageSlot1.sprite = null;
                maxJumpValue = 2;
                Destroy(dialogTrader2);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "StealthTrigger" && isCrawling == false)
        {
            FindObjectOfType<ConditionScript>().EndingDeath.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }
}
