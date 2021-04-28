using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Watchman : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Transform tr;

    public float speed = 1;
    public int patrolRoute; //маршрут патрулирования
    public Transform point;
    bool moveRight = true;

    public LineRenderer lineRenderer;

    //------------------ переменные ниже нужны для того, чтобы манипулировать состояниями противника можно было разными условиями
    public bool chill = true;
    public bool angry = false;
    //------------------

    Transform player;
    public float enemyTriggerDistance;

    private PlayerController pcScript;
    private Rigidbody2D playerRB;
    private Transform playerTR;

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
        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance)
        {
            angry = true;
            chill = false;
        }

        if (Vector2.Distance(transform.position, player.position) > enemyTriggerDistance)
        {
            chill = true;
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

        anim.SetBool("doAttack", false);
        anim.SetBool("isWalking", true);
    }

    void Angry()
    {
        anim.SetBool("doAttack", true);
        anim.SetBool("isWalking", false);


        RaycastHit2D hit = Physics2D.Raycast(playerRB.position + Vector2.up * 2, playerTR.TransformDirection(Vector2.up));
        lineRenderer.SetPosition(0, playerTR.position);
        lineRenderer.SetPosition(1, hit.point);

        if (hit)
        {
            Debug.Log(hit.transform.name);

            lineRenderer.SetPosition(0, playerTR.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, playerTR.position);
            lineRenderer.SetPosition(1, playerTR.position + playerTR.right * 100);
        }

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance / 2)
        {
            SceneManager.LoadScene(FindObjectOfType<PlayerController>().levelNumber);
        }
        StartCoroutine(StealthFailed());
    }

    IEnumerator StealthFailed()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(FindObjectOfType<PlayerController>().levelNumber);
    }
}
