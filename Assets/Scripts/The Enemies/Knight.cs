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
    public bool moveRight = true;

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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        pcScript = player.GetComponent<PlayerController>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerTR = player.GetComponent<Transform>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            anim.SetBool("attack1", false);
            anim.SetBool("attack2", false);
            anim.SetBool("attack3", false);
            anim.SetBool("isRun", false);
            anim.SetBool("dead", true);

            StartCoroutine(Dead());
        }
        if (FindObjectOfType<PlayerController>().punchToCheck == true && catchPlayer == true)
        {
            FindObjectOfType<PlayerController>().punchToCheck = false;
            hp -= Random.Range(20, 40);
            hpText.text = hp.ToString();
        }

        if (hp > 350 && fightIsStarted == true)
        {
            FirstPhase();
        }
        else if (hp > 250 && hp <= 350)
        {
            SecondPhase();
        }
        else if (hp <= 250)
        {
            ThirdPhase();
        }
    }

    void FirstPhase()
    {
        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance / 20)
        {
            //anim.SetBool("attack1", false);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (catchPlayer == true)
            {
                if (canAttack == true)
                {
                    StartCoroutine(Attack());
                }
            }
        }
        else if (Vector2.Distance(transform.position, player.position) > enemyTriggerDistance / 20)
        {
            transform.position = Vector2.MoveTowards(transform.position, -player.position, speed * Time.deltaTime);
        }
    }

    void SecondPhase()
    {
        StopAllCoroutines();

        if (moveRight == true)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); //time.deltatime позволяет двигаться постоянно, то есть как-будто зажата клавиша
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); //time.deltatime позволяет двигаться постоянно если метод вызван в апдейте
        }

        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);
        anim.SetBool("attack3", false);
        anim.SetBool("isRun", true);
    }

    void ThirdPhase()
    {

    }

    IEnumerator Attack()
    {
        //anim.SetBool("walk", false);
        //nimNumber = Random.Range(1, 3);
        //Debug.Log(animNumber);
        //anim.SetBool($"attack{animNumber}", true);

        canAttack = false;
        anim.SetBool($"attack{Random.Range(1,4)}", true);

        yield return new WaitForSeconds(0.4f);
        if (catchPlayer == true)
        {
            FindObjectOfType<PlayerController>().hp -= Random.Range(10, 15);
            FindObjectOfType<PlayerController>().hpValue.text = FindObjectOfType<PlayerController>().hp.ToString();
        }

        yield return new WaitForSeconds(0.6f);
        canAttack = true;
        anim.SetBool($"attack{Random.Range(1, 4)}", false);

        if (hp > 350)
        {
            transform.position = point[Random.Range(0, 4)].transform.position;
        }

        //anim.SetBool($"attack{animNumber}", false);
        //anim.SetBool("walk", true);
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
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
