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
    public float patrolRoute; //������� ��������������
    public Transform point;
    public bool moveRight = true;

    //------------------ ���������� ���� ����� ��� ����, ����� �������������� ����������� ���������� ����� ���� ������� ���������
    public bool chill = true;
    public bool angry = false;
    public bool willPatroul = false;
    //------------------

    Transform player;
    public float enemyTriggerDistance;

    private PlayerController pcScript;
    private Rigidbody2D playerRB;
    private Transform playerTR;

    int hp = 100;

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

        if (hp <= 0)
        {
            anim.SetBool("attack1", false);
            anim.SetBool("attack2", false);
            anim.SetBool("walk", false);
            anim.SetBool("dead", true);

            StartCoroutine(Dead());
        }
    }

    void Chill()
    {
        StopAllCoroutines();
        if (transform.position.x > point.position.x + patrolRoute) //���� ���� ������� �� ����� �����, �� ��������������� ����� � ���� ���� �� �����
        {
            moveRight = false;
            transform.localScale *= new Vector2(-1, 1);
        }
        else if (transform.position.x < point.position.x - patrolRoute) //���� ���� ������� �� ����� ������, �� ��������������� ������ � ���� ���� �� �����
        {
            moveRight = true;
            transform.localScale *= new Vector2(-1, 1);
        }

        if (moveRight == true)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y); //time.deltatime ��������� ��������� ���������, �� ���� ���-����� ������ �������
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y); //time.deltatime ��������� ��������� ��������� ���� ����� ������ � �������
        }

        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);
        anim.SetBool("walk", true);
    }

    void Angry()
    {


        //RaycastHit2D hit = Physics2D.Raycast(playerRB.position * 2, playerTR.TransformDirection(Vector2.right));
        //lineRenderer.SetPosition(0, playerTR.position);
        //lineRenderer.SetPosition(1, hit.point);

        //if (hit)
        //{
        //    Debug.Log(hit.transform.name);

        //    lineRenderer.SetPosition(0, playerTR.position);
        //    lineRenderer.SetPosition(1, hit.point);
        //}
        //else
        //{
        //    lineRenderer.SetPosition(0, playerTR.position);
        //    lineRenderer.SetPosition(1, playerTR.position + playerTR.right * 100);
        //}

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance && Vector2.Distance(transform.position, player.position) > enemyTriggerDistance / 2)
        {
            //anim.SetBool("attack1", false);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance / 2)
        {
            if (canAttack == true)
            {
                StartCoroutine(Attack());
            }
        }
    }

    int animNumber = 0;
    IEnumerator Attack()
    {
        //anim.SetBool("walk", false);
        //nimNumber = Random.Range(1, 3);
        //Debug.Log(animNumber);
        //anim.SetBool($"attack{animNumber}", true);

        canAttack = false;
        anim.SetBool("attack1", true);

        yield return new WaitForSeconds(0.4f);
        if (catchPlayer == true)
        {
            FindObjectOfType<PlayerController>().hp -= Random.Range(15, 25);
            FindObjectOfType<PlayerController>().hpValue.text = FindObjectOfType<PlayerController>().hp.ToString();
        }

        yield return new WaitForSeconds(0.6f);
        canAttack = true;
        anim.SetBool("attack1", false);
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