using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public GameObject platform;
    public float posX, posY;
    public float speed;
    public bool isTrigger;//

    public float startX, startY;//
    public bool isStartPosSaved = false;//

    public bool freezeX;
    public bool freezeY;

    public bool gettingEnd = false;//

    public bool needsTrigger;

    public float curPosX, curPosY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTrigger = true;
        }
    }

    private void Update()
    {
        if (needsTrigger == true)
        {
            if (isTrigger == true)
            {
                PlatformLogic();
            }
        }
        else
        {
            PlatformLogic();
        }
    }

    private void PlatformLogic()
    {
        curPosX = platform.transform.position.x;
        curPosY = platform.transform.position.y;

        if (isStartPosSaved == false)
        {
            startX = platform.transform.position.x;
            startY = platform.transform.position.y;
            isStartPosSaved = true;
        }

        if (gettingEnd == false)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, new Vector3(posX, posY, platform.transform.position.z), speed * Time.deltaTime);

            if (freezeX == false && freezeY == false)
            {
                if (platform.transform.position.x == posX || platform.transform.position.y == posY)
                {
                    gettingEnd = true;
                }
            }
        }
        else if (gettingEnd == true)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, new Vector3(startX, startY, platform.transform.position.z), speed * Time.deltaTime);

            if (freezeX == false && freezeY == false)
            {
                if (platform.transform.position.x == startX && platform.transform.position.y == startY)
                {
                    gettingEnd = false;
                }
            }
        }

        if (freezeX == true)
        {
            if (platform.transform.position.y == posY)
            {
                isTrigger = false;
            }
        }
        else if (freezeY == true)
        {
            if (platform.transform.position.x == posX)
            {
                isTrigger = false;
            }
        }
    }
}
