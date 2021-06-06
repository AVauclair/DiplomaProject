using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warrior : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Transform tr;

    public float speed = 0.7f;
    public float patrolRoute; //маршрут патрулирования
    public Transform point;
    public bool moveRight = true;

    //------------------ переменные ниже нужны для того, чтобы манипулировать состояниями противника можно было разными условиями
    public bool chill = true;
    public bool angry = false;
    public bool willPatroul = false;
    //------------------

    Transform player;
    public float enemyTriggerDistance;

    private PlayerController pcScript;
    private Rigidbody2D playerRB;
    private Transform playerTR;

    public int hp = 100;

    bool canAttack = true;
    public bool catchPlayer = false;
    
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
            anim.SetBool("walk", false);
            anim.SetBool("dead", true);

            StartCoroutine(Dead());
        }
        if (FindObjectOfType<PlayerController>().punchToCheck == true && catchPlayer == true)
        {
            FindObjectOfType<PlayerController>().punchToCheck = false;
            hp -= Random.Range(30, 45);
        }

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance) //&& (player.position.y < transform.position.y + 0.2f || player.position.y > transform.position.y - 0.2f))
        {
            angry = true;
            chill = false;
        }

        if (Vector2.Distance(transform.position, player.position) > enemyTriggerDistance) //|| (player.position.y > transform.position.y + 0.2f || player.position.y < transform.position.y - 0.2f))
        {
            chill = true;
            canAttack = true;
            angry = false;
        }

        if (willPatroul == true && angry == false)
        {
            if (chill == true)
            {
                Chill();
            }
        }

        else if (angry == true)
        {
            Angry();
        }
    }

    void Chill()
    {
        StopAllCoroutines();
        if (transform.position.x > point.position.x + patrolRoute) //если враг доходит до точки конца, то разворачивается влево и идет туда до точки
        {
            moveRight = false;
            transform.localScale *= new Vector2(-1, 1);
        }
        else if (transform.position.x < point.position.x - patrolRoute) //если враг доходит до точки начала, то разворачивается вправо и идет туда до точки
        {
            moveRight = true;
            transform.localScale *= new Vector2(-1, 1);
        }

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
        anim.SetBool("walk", true);
    }

    void Angry()
    {
        if (willPatroul == true)
        {
            if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance && Vector2.Distance(transform.position, player.position) > enemyTriggerDistance / 2)
            {
                //anim.SetBool("attack1", false);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
        if (catchPlayer == true)
        {
            if (canAttack == true)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        anim.SetBool("attack1", true);

        yield return new WaitForSeconds(0.4f);
        if (catchPlayer == true)
        {
            FindObjectOfType<PlayerController>().hp -= Random.Range(10, 15);
            FindObjectOfType<PlayerController>().hpValue.text = FindObjectOfType<PlayerController>().hp.ToString();
        }

        yield return new WaitForSeconds(0.6f);
        canAttack = true;
        anim.SetBool("attack1", false);
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
