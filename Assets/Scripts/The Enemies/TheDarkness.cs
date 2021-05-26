using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDarkness : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public float speed = 1;
    public float patrolRoute; //������� ��������������
    public Transform point;
    bool moveRight = true;

    //------------------ ���������� ���� ����� ��� ����, ����� �������������� ����������� ���������� ����� ���� ������� ���������
    public bool chill = true;
    public bool angry = false;
    public bool returns = false;
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

        player = GameObject.FindGameObjectWithTag("Player").transform;

        pcScript = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < patrolRoute && angry == false) //transform.position - �����, ��� ����� point.position - �����, ��� ����� ��������� empty object � ���������� � transform
                                                                                                  //distance - ��� ����������
        {
            chill = true;
            returns = false;
        }

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance)
        {
            if (pcScript.isWalking == true)
            {
                angry = true;
                chill = false;
                returns = false;
            }
            else if (Vector2.Distance(transform.position, point.position) < patrolRoute && pcScript.isWalking == false)
            {
                chill = true;
                angry = false;
                anim.SetBool("doAttack", false);
                anim.SetBool("isWalking", true);
            }
            else if (Vector2.Distance(transform.position, player.position) > enemyTriggerDistance && pcScript.isWalking == false)
            {
                returns = true;
                angry = false;
                anim.SetBool("doAttack", false);
                anim.SetBool("isWalking", true);
            }
        }

        if (Vector2.Distance(transform.position, player.position) > enemyTriggerDistance && chill == false)
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
    }

    void AngryOff()
    {
        angry = false;
    }

    void Angry()
    {
        if (pcScript.isWalking == false)
        {
            anim.SetBool("doAttack", false);
            angry = false;

            if (angry == false && chill == false && returns == false)
            {
                returns = true;
            }
        }
        else if (pcScript.isWalking)
        {
            speed = 1.2f;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); //movetowards - ���� � ����-��

            if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance)// / 2)
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
    }

    void Returns()
    {
        speed = 1f;
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime); //movetowards - ���� � ����-��
    }
}
