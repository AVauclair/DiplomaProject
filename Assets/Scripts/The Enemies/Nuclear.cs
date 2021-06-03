using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public bool angry = false;

    Transform player;
    public float enemyTriggerDistance;

    public Object explosion;

    private AudioSource audioSource;
    public AudioClip whispering;
    public AudioClip explosive;

    private bool clipIsPlayed = false;
    public int hp = 2;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        explosion = Resources.Load("Explosion");

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        if (FindObjectOfType<PlayerController>().punchToCheck == true && catchPlayer == true)
        {
            FindObjectOfType<PlayerController>().punchToCheck = false;
            hp -= 1;
        }

        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance)
        {
            if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance)// / 2)
            {
                anim.SetBool("isRaised", true);
                if (clipIsPlayed == false)
                {
                    audioSource.PlayOneShot(whispering);
                    clipIsPlayed = true;

                    StartCoroutine(Explosion());
                }
            }
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(explosive);
        GameObject explosionRef = (GameObject)Instantiate(explosion);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        DamageToPlayer();

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void DamageToPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance * 3 && Vector2.Distance(transform.position, player.position) > enemyTriggerDistance * 2)
        {
            FindObjectOfType<PlayerController>().hp -= Random.Range(1, 10);
            FindObjectOfType<PlayerController>().hpValue.text = FindObjectOfType<PlayerController>().hp.ToString();
            CalculateInertia(1000);
        }
        else if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance * 2 && Vector2.Distance(transform.position, player.position) > enemyTriggerDistance)
        {
            FindObjectOfType<PlayerController>().hp -= Random.Range(10, 20);
            FindObjectOfType<PlayerController>().hpValue.text = FindObjectOfType<PlayerController>().hp.ToString();
            CalculateInertia(3000);
        }
        else if (Vector2.Distance(transform.position, player.position) < enemyTriggerDistance)
        {
            FindObjectOfType<PlayerController>().hp -= Random.Range(20, 30);
            FindObjectOfType<PlayerController>().hpValue.text = FindObjectOfType<PlayerController>().hp.ToString();
            CalculateInertia(5000);
        }
    }

    private void CalculateInertia(int power)
    {
        if (transform.position.x > player.position.x)
        {
            FindObjectOfType<PlayerController>().rb.AddForce(Vector2.left * power);
        }
        else
        {
            FindObjectOfType<PlayerController>().rb.AddForce(Vector2.right * power);
        }
    }

    bool catchPlayer = false;
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
