using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Knight : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Transform tr;

    public float speed = 1.2f;
    bool moveRight = true;

    //------------------ переменные ниже нужны для того, чтобы манипулировать состояниями противника можно было разными условиями
    public bool chill = true;
    public bool angry = true;
    //------------------

    Transform player;
    public float enemyTriggerDistance;

    private PlayerController pcScript;
    private Rigidbody2D playerRB;
    private Transform playerTR;

    public int hp = 100;
    public TextMeshProUGUI hpText;
    public GameObject hpObject;

    bool canAttack = true;
    public bool catchPlayer = false;

    public bool fightIsStarted = false;

    public GameObject[] point;

    public bool firstPhase = false;
    public bool secondPhase = false;
    public bool thirdPhase = false;

    private AudioSource audioSource;
    public AudioClip secondPhaseClip;
    public AudioClip thirdPhaseClip;

    public Object nuclear;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        pcScript = player.GetComponent<PlayerController>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerTR = player.GetComponent<Transform>();

        nuclear = Resources.Load("Nuclear");
    }

    bool firstStep = true;
    bool isEnd = false;
    private void Update()
    {
        if (hp <= 0)
        {
            if (isEnd == false)
            {
                isEnd = true;
                StopAllCoroutines();
                firstPhase = false;
                secondPhase = false;
                thirdPhase = false;
                anim.SetBool("attack1", false);
                anim.SetBool("attack2", false);
                anim.SetBool("attack3", false);
                anim.SetBool("isRun", false);
                anim.SetBool("isJump", false);
                fightIsStarted = false;

                hpObject.SetActive(false);

                FindObjectOfType<ConditionScript>().sceneNumber = 23;
                FindObjectOfType<ConditionScript>().ConditionsChecker();
            }
        }
        if (FindObjectOfType<PlayerController>().punchToCheck == true && catchPlayer == true)
        {
            FindObjectOfType<PlayerController>().punchToCheck = false;
            hp -= Random.Range(20, 40);
            hpText.text = hp.ToString();

            if (secondPhase == true || thirdPhase == true)
            {
                transform.position = point[Random.Range(0, 4)].transform.position;
            }
        }

        if (fightIsStarted == true)
        {
            if (hp > 350)
            {
                //firstPhase = true;
                if (firstStep == true)
                {
                    if (transform.position.x < -25.95f && transform.position.y < 17 && player.position.y < 17)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-25.95f, 15.74f), speed * Time.deltaTime);
                        anim.SetBool("isRun", true);
                        if (moveRight == true)
                        {
                            transform.localScale *= new Vector2(-1, 1);
                            moveRight = false;
                            speed = 1.8f;
                            Application.targetFrameRate = 300;
                        }

                        if (transform.position.x == -25.95f && transform.position.y < 17 && player.position.y < 17)
                        {
                            speed = 1.2f;
                            transform.position = new Vector2(-30, 17.51f);
                            firstPhase = true;
                            firstStep = false;
                            anim.SetBool("isRun", false);
                        }
                    }
                }
            }
            else if (hp > 250 && hp <= 350)
            {
                if (firstPhase == true)
                {
                    audioSource.PlayOneShot(secondPhaseClip);
                    firstPhase = false;
                }
                secondPhase = true;
                hpText.color = new Color32(255, 229, 0, 255);
            }
            else if (hp <= 250)
            {
                if (secondPhase == true)
                {
                    audioSource.PlayOneShot(thirdPhaseClip);
                    secondPhase = false;
                }
                thirdPhase = true;
                hpText.color = new Color32(255, 0, 0, 255);
            }

            if (firstPhase == true) FirstPhase();
            if (secondPhase == true) SecondPhase();
            if (thirdPhase == true) ThirdPhase();
        }
    }

    void FirstPhase()
    {
        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance / 2)
        {
            if (catchPlayer == true)
            {
                if (canAttack == true)
                {
                    StartCoroutine(Attack());
                }
            }
        }
        else if (Vector2.Distance(transform.position, player.position) > enemyTriggerDistance / 2)
        {
            if (player.position.x < transform.position.x)
            {
                if (moveRight == true)
                {
                    transform.localScale *= new Vector2(-1, 1);
                    moveRight = false;
                }
                anim.SetBool("isRun", false);

                if (transform.position.x >= -33.2f && transform.position.x <= 25.95f && transform.position.y < 17 && player.position.y < 17)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(-25.95f, 15.74f), speed * Time.deltaTime);
                    anim.SetBool("isRun", true);
                }
                else if (transform.position.x >= -33.2f && transform.position.x <= -27.26f && transform.position.y > 17 && transform.position.y < 19 && player.position.y < 19 && player.position.y > 17)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(-27.26f, 17.51f), speed * Time.deltaTime);
                    anim.SetBool("isRun", true);
                }
                else if (transform.position.x >= -34.35f && transform.position.x <= -26.42f && transform.position.y > 19 && player.position.y > 19)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(-26.42f, 19.59f), speed * Time.deltaTime);
                    anim.SetBool("isRun", true);
                }
            }
            else
            {
                if (moveRight == false)
                {
                    transform.localScale *= new Vector2(-1, 1);
                    moveRight = true;
                }
                anim.SetBool("isRun", false);

                if (transform.position.x >= -33.2f && transform.position.x <= 25.95f && transform.position.y < 17 && player.position.y < 17)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(-33.2f, 15.74f), speed * Time.deltaTime);
                    anim.SetBool("isRun", true);
                }
                else if (transform.position.x >= -33.2f && transform.position.x <= -27.26f && transform.position.y > 17 && transform.position.y < 19 && player.position.y < 19 && player.position.y > 17)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(-34.43f, 17.51f), speed * Time.deltaTime);
                    anim.SetBool("isRun", true);
                }
                else if (transform.position.x >= -34.35f && transform.position.x <= -26.42f && transform.position.y > 19 && player.position.y > 19)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(-34.35f, 19.59f), speed * Time.deltaTime);
                    anim.SetBool("isRun", true);
                }
            }

            if (transform.position.x >= -26 && transform.position.x <= -25.9f && transform.position.y < 17 && player.position.y < 17)
            {
                transform.position = new Vector2(-30, 17.51f);
                anim.SetBool("isRun", false);
            }
            else if (transform.position.x >= -27.3f && transform.position.x <= -27.1f && transform.position.y > 17 && transform.position.y < 19 && player.position.y < 19 && player.position.y > 17)
            {
                transform.position = new Vector2(-30, 19.59f);
                anim.SetBool("isRun", false);
            }
            else if (transform.position.x >= -34.5f && transform.position.x <= -34.3f && transform.position.y > 19 && player.position.y > 19)
            {
                transform.position = new Vector2(-30, 15.74f);
                anim.SetBool("isRun", false);
            }

            if (transform.position.x >= -33.3f && transform.position.x <= -33.1f && transform.position.y < 17 && player.position.y < 17)
            {
                transform.position = new Vector2(-30, 17.51f);
                anim.SetBool("isRun", false);
            }
            else if (transform.position.x >= -33.3f && transform.position.x <= -33.1f && transform.position.y > 17 && transform.position.y < 19 && player.position.y < 19 && player.position.y > 17)
            {
                transform.position = new Vector2(-30, 19.59f);
                anim.SetBool("isRun", false);
            }
            else if (transform.position.x >= -26.5f && transform.position.x <= -26.3f && transform.position.y > 19 && player.position.y > 19)
            {
                transform.position = new Vector2(-30, 15.74f);
                anim.SetBool("isRun", false);
            }
        }
    }

    void SecondPhase()
    {
        if (canSpawnEnemy == true) StartCoroutine(SpawnNuclear(2, 0.5f));
        if (canTP == true) StartCoroutine(Teleport());

        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);
        anim.SetBool("attack3", false);
        anim.SetBool("isRun", false);
        anim.SetBool("isJump", false);
    }

    void ThirdPhase()
    {
        if (canSpawnEnemy == true) StartCoroutine(SpawnNuclear(4, 1.5f));
        if (canTP == true) StartCoroutine(TeleportAndAttack());
    }

    int animNumber;
    IEnumerator Attack()
    {
        if (player.position.x < transform.position.x && moveRight == false)
        {
            moveRight = true;
            transform.localScale *= new Vector2(-1, 1);
        }
        else if (player.position.x > transform.position.x && moveRight == true)
        {
            moveRight = false;
            transform.localScale *= new Vector2(-1, 1);
        }

        animNumber = Random.Range(1, 4);
        canAttack = false;
        anim.SetBool($"attack{animNumber}", true);

        yield return new WaitForSeconds(0.4f);
        if (catchPlayer == true)
        {
            FindObjectOfType<PlayerController>().hp -= Random.Range(10, 15);
            FindObjectOfType<PlayerController>().hpValue.text = FindObjectOfType<PlayerController>().hp.ToString();
            FindObjectOfType<PlayerController>().GetComponent<AudioSource>().PlayOneShot(FindObjectOfType<PlayerController>().gettingDamage);
        }
        if (firstPhase == true || thirdPhase == true)
        {
            transform.position = point[Random.Range(0, 4)].transform.position;
        }

        yield return new WaitForSeconds(0.6f);
        canAttack = true;
        anim.SetBool($"attack{animNumber}", false);


        //anim.SetBool($"attack{animNumber}", false);
        //anim.SetBool("walk", true);
    }

    bool canSpawnEnemy = true;
    IEnumerator SpawnNuclear(int secondsToSpawn, float distanceToSpawn)
    {
        canSpawnEnemy = false;
        yield return new WaitForSeconds(secondsToSpawn);
        GameObject spawnNuclear = (GameObject)Instantiate(nuclear);
        if (FindObjectOfType<PlayerController>().isFacingRight == false)
        {
            spawnNuclear.transform.position = new Vector3(player.position.x - distanceToSpawn, player.position.y + 0.3f, player.position.z);
        }
        else
        {
            spawnNuclear.transform.position = new Vector3(player.position.x + distanceToSpawn, player.position.y + 0.3f, player.position.z);
        }
        canSpawnEnemy = true;

        yield return new WaitForSeconds(3);
        Destroy(spawnNuclear);
    }

    bool canTP = true;
    IEnumerator Teleport()
    {
        canTP = false;
        yield return new WaitForSeconds(10);
        transform.position = point[Random.Range(0, 4)].transform.position;
        hp += Random.Range(20, 25);
        hpText.text = hp.ToString();
        canTP = true;
    }

    IEnumerator TeleportAndAttack()
    {
        canTP = false;
        yield return new WaitForSeconds(2);
        if (FindObjectOfType<PlayerController>().isFacingRight == false)
        {
            transform.position = new Vector3(player.position.x + 0.3f, player.position.y + 0.3f, player.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x - 0.3f, player.position.y + 0.3f, player.position.z);
        }
        canTP = true;

        StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            catchPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            catchPlayer = false;
        }
    }
}
