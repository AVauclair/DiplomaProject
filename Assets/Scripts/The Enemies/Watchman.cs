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
    public float patrolRoute; //������� ��������������
    public Transform point;
    bool moveRight = true;

    public LineRenderer lineRenderer;

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
        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance && (player.position.y < transform.position.y + 0.2f || player.position.y > transform.position.y - 0.2f))
        {
            angry = true;
            chill = false;
        }

        if (Vector2.Distance(transform.position, player.position) > enemyTriggerDistance || (player.position.y > transform.position.y + 0.2f || player.position.y < transform.position.y - 0.2f))
        {
            chill = true;
            angry = false;
        }

        if (willPatroul == true && angry == false)
        {
            if (chill == true)
            {
                Chill();
            }
        }
        else if (willPatroul == false && angry == false)
        {
            StopAllCoroutines();
            anim.SetBool("doAttack", false);
            anim.SetBool("isWalking", false);
        }

        else if (angry == true)
        {
            Angry();
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

        anim.SetBool("doAttack", false);
        anim.SetBool("isWalking", true);
    }

    void Angry()
    {
        anim.SetBool("doAttack", true);
        anim.SetBool("isWalking", false);


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

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance / 2)
        {
            SceneManager.LoadScene(FindObjectOfType<LevelCount>().levelNumber);
        }
        StartCoroutine(StealthFailed());
    }

    IEnumerator StealthFailed()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(FindObjectOfType<LevelCount>().levelNumber);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "death")
        {
            Destroy(gameObject);
        }
    }
}
