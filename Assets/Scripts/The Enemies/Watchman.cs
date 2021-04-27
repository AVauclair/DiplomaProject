using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Watchman : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    Transform tr;

    public float speed = 1;
    public int patrolRoute; //������� ��������������
    public Transform point;
    bool moveRight = true;

    public LineRenderer lineRenderer;

    //------------------ ���������� ���� ����� ��� ����, ����� �������������� ����������� ���������� ����� ���� ������� ���������
    public bool chill = true;
    public bool angry = false;
    //------------------

    Transform player;
    public float enemyTriggerDistance;

    private PlayerController pcScript;
    private GameObject PlayerObject;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        pcScript = player.GetComponent<PlayerController>();
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

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance / 2)
        {
            //RaycastHit2D hit = Physics2D.Raycast(rb.position, tr.right);
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, hit.point);

            //if (hit)
            //{
            //    Debug.Log(hit.transform.name);

            //    lineRenderer.SetPosition(0, transform.position);
            //    lineRenderer.SetPosition(1, hit.point);
            //}
            //else
            //{
            //    lineRenderer.SetPosition(0, transform.position - new Vector3(0f, 0.05f, 0f));
            //    lineRenderer.SetPosition(1, transform.position + transform.right * 100);
            //}
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
