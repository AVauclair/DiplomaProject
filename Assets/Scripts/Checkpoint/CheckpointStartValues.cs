using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointStartValues : MonoBehaviour
{
    public int checkpointNumber = 0;
    public float posX;
    public float posY;
    public float posZ;

    public GameObject[] checkpoints;

    public void CheckpointCheck()
    {
        checkpointNumber = PlayerPrefs.GetInt("checkpointNumber");

        if (checkpointNumber == 1)
        {
            checkpoints[0].SetActive(false);
        }
        else if (checkpointNumber == 2)
        {
            checkpoints[1].SetActive(false);
        }
    }

    public void CheckpointStart()
    {
        posX = PlayerPrefs.GetFloat("posX");
        posY = PlayerPrefs.GetFloat("posY");
        posZ = PlayerPrefs.GetFloat("posZ");
        FindObjectOfType<PlayerController>().souls = PlayerPrefs.GetInt("souls");
        FindObjectOfType<PlayerController>().levelNumber = PlayerPrefs.GetInt("levelNumber");

        if (checkpointNumber == 0)
        {
            FindObjectOfType<PlayerController>().transform.position = new Vector3(-5.71f * 2, -0.77f * 2, 0f);

        }
        else
        {
            FindObjectOfType<PlayerController>().transform.position = new Vector3(posX + 0.5f, posY, 0f);
            FindObjectOfType<WatchPlayer>().leftLimit = PlayerPrefs.GetFloat("leftLimit");
            FindObjectOfType<WatchPlayer>().rightLimit = PlayerPrefs.GetFloat("rightLimit");
            FindObjectOfType<WatchPlayer>().downLimit = PlayerPrefs.GetFloat("downLimit");
            FindObjectOfType<WatchPlayer>().upLimit = PlayerPrefs.GetFloat("upLimit");
            FindObjectOfType<WatchPlayer>().offset.x = PlayerPrefs.GetFloat("offsetX");
            FindObjectOfType<WatchPlayer>().offset.y = PlayerPrefs.GetFloat("offsetY");
            FindObjectOfType<WatchPlayer>().dumping = PlayerPrefs.GetFloat("dumping");
        }
    }
}
