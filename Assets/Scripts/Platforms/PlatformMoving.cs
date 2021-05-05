using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public GameObject platform;
    public float posX, posY;
    public float speed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);

            platform.transform.position = Vector3.MoveTowards(platform.transform.position, new Vector3(posX, posY, 0), speed*Time.deltaTime);
        }
    }
}
