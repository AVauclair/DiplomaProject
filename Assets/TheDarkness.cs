using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDarkness : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public float speed = 1;
    public int patrolRoute; //маршрут патрулирования
    public Transform point;
    bool moveRight = true;

    //------------------ переменные ниже нужны для того, чтобы манипулировать состояниями противника можно было разными условиями
    public bool chill = true;
    public bool angry = false;
    public bool returns = false;
    //------------------

    Transform player;
    public float enemyTriggerDistance;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < patrolRoute && angry == false) //transform.position - точка, где стоит point.position - точка, где стоит созданный empty object и помещенный в transform
                                                                                //distance - это расстояние
        {
            chill = true;
            angry = false;
            returns = false;
        }

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance)
        {
            angry = true;
            chill = false;
            returns = false;
        }

        if (Vector2.Distance(transform.position, player.position) > enemyTriggerDistance)
        {
            returns = true;
            angry = false;
        }

        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (returns == true)
        {
            Returns();
        }
    }

    void Chill()
    {
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
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); //time.deltatime позволяет двигаться постоянно если метод вызван в апдейте
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); //time.deltatime позволяет двигаться постоянно если метод вызван в апдейте
        }
    }

    void Angry()
    {
        speed = 1.2f;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); //movetowards - идти к чему-то

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance / 2)
        {
            anim.SetBool("doAttack", true);
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("doAttack", false);
            anim.SetBool("isWalking", true);
        }
    }

    void Returns()
    {
        speed = 1;
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime); //movetowards - идти к чему-то
    }
}
