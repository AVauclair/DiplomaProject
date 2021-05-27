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


        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
