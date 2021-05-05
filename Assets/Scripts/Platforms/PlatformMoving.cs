using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public GameObject platform;
    public float posX, posY;
    public float speed;
    private bool isTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTrigger = true;
        }
    }

    private void Update()
    {
        if (isTrigger == true)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, new Vector3(posX, posY, platform.transform.position.z), speed * Time.deltaTime);

            if (platform.transform.position.y == posY)
            {
                isTrigger = false;
            }
        }
    }
}
