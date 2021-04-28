using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject tilemapDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<PlayerController>().checkpointNumber == 0)
        {
            tilemapDoor.SetActive(true);

            PlayerPrefs.SetFloat("leftLimit", FindObjectOfType<WatchPlayer>().leftLimit = -6.27f);
            PlayerPrefs.SetFloat("rightLimit", FindObjectOfType<WatchPlayer>().rightLimit = 11.5f);
            PlayerPrefs.SetFloat("downLimit", FindObjectOfType<WatchPlayer>().downLimit = -27.2f);
            PlayerPrefs.SetFloat("upLimit", FindObjectOfType<WatchPlayer>().upLimit = 0.1f);
            PlayerPrefs.SetFloat("offsetX", FindObjectOfType<WatchPlayer>().offset.x = 1.15f);
            PlayerPrefs.SetFloat("offsetY", FindObjectOfType<WatchPlayer>().offset.y = 0.5f);
            PlayerPrefs.SetFloat("dumping", FindObjectOfType<WatchPlayer>().dumping = 2f);
        }

        FindObjectOfType<PlayerController>().checkpointNumber++;


        if (collision.tag == "Player")
        {
            PlayerPrefs.SetFloat("posX", FindObjectOfType<PlayerController>().transform.position.x);
            PlayerPrefs.SetFloat("posY", FindObjectOfType<PlayerController>().transform.position.y);
            PlayerPrefs.SetFloat("posZ", FindObjectOfType<PlayerController>().transform.position.z);

            PlayerPrefs.SetInt("souls", FindObjectOfType<PlayerController>().souls);
            PlayerPrefs.SetInt("checkpointNumber", FindObjectOfType<PlayerController>().checkpointNumber);
            PlayerPrefs.SetInt("levelNumber", FindObjectOfType<PlayerController>().levelNumber);
            PlayerPrefs.Save();

            Destroy(gameObject);
        }
    }
}
