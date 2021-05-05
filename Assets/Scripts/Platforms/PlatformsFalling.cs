using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsFalling : MonoBehaviour
{
    public GameObject Platform;
    private Rigidbody2D rbPlatform;
    public bool willObjectDestroy = false;
    public bool willPlayerTrigger = false;

    private void Start()
    {
        rbPlatform = Platform.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (willPlayerTrigger == true)
        {
            if (collision.tag == "Player")
            {
                Fall();

                if (willObjectDestroy == true)
                {
                    StartCoroutine(DestroyPlatform());
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (collision.tag == "Box")
            {
                Fall();

                if (willObjectDestroy == true)
                {
                    StartCoroutine(DestroyPlatform());
                    Destroy(collision.gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void Fall()
    {
        rbPlatform.isKinematic = false;
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(2f);

        Destroy(Platform);
        Destroy(gameObject);
    }
}
