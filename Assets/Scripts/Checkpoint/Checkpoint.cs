using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject tilemapDoor1;
    public GameObject tilemapDoor2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (FindObjectOfType<CheckpointStartValues>().checkpointNumber == 0)
            {
                tilemapDoor1.SetActive(true);

                PlayerPrefs.SetFloat("leftLimit", FindObjectOfType<WatchPlayer>().leftLimit = -22f);
                PlayerPrefs.SetFloat("rightLimit", FindObjectOfType<WatchPlayer>().rightLimit = 25.37f);
                PlayerPrefs.SetFloat("downLimit", FindObjectOfType<WatchPlayer>().downLimit = -20.6f);
                PlayerPrefs.SetFloat("upLimit", FindObjectOfType<WatchPlayer>().upLimit = 0.1f);
                PlayerPrefs.SetFloat("offsetX", FindObjectOfType<WatchPlayer>().offset.x = 1.15f);
                PlayerPrefs.SetFloat("offsetY", FindObjectOfType<WatchPlayer>().offset.y = 0.5f);
                PlayerPrefs.SetFloat("dumping", FindObjectOfType<WatchPlayer>().dumping = 2f);
            }

            if (FindObjectOfType<CheckpointStartValues>().checkpointNumber == 2)
            {
                tilemapDoor2.SetActive(true);

                PlayerPrefs.SetFloat("leftLimit", FindObjectOfType<WatchPlayer>().leftLimit = -37.18f);
                PlayerPrefs.SetFloat("rightLimit", FindObjectOfType<WatchPlayer>().rightLimit = -26.19f);
                PlayerPrefs.SetFloat("downLimit", FindObjectOfType<WatchPlayer>().downLimit = -27.2f);
                PlayerPrefs.SetFloat("upLimit", FindObjectOfType<WatchPlayer>().upLimit = -5.14f);
                PlayerPrefs.SetFloat("offsetX", FindObjectOfType<WatchPlayer>().offset.x = 1.15f);
                PlayerPrefs.SetFloat("offsetY", FindObjectOfType<WatchPlayer>().offset.y = 0.5f);
                PlayerPrefs.SetFloat("dumping", FindObjectOfType<WatchPlayer>().dumping = 2f);
            }

            FindObjectOfType<CheckpointStartValues>().checkpointNumber++;

            PlayerPrefs.SetFloat("posX", FindObjectOfType<PlayerController>().transform.position.x);
            PlayerPrefs.SetFloat("posY", FindObjectOfType<PlayerController>().transform.position.y);
            PlayerPrefs.SetFloat("posZ", FindObjectOfType<PlayerController>().transform.position.z);
            PlayerPrefs.SetFloat("localPos", FindObjectOfType<PlayerController>().transform.localScale.x);

            PlayerPrefs.SetInt("souls", FindObjectOfType<PlayerController>().souls);
            PlayerPrefs.SetInt("checkpointNumber", FindObjectOfType<CheckpointStartValues>().checkpointNumber);
            PlayerPrefs.SetInt("levelNumber", FindObjectOfType<LevelCount>().levelNumber);
            PlayerPrefs.SetInt("sceneNumber", FindObjectOfType<ConditionScript>().sceneNumber);

            PlayerPrefs.SetInt("havingKey", FindObjectOfType<PlayerController>().havingKey);
            PlayerPrefs.SetInt("havingWarriorSoul", FindObjectOfType<PlayerController>().havingWarriorSoul);
            PlayerPrefs.Save();

            Destroy(gameObject);
        }
    }
}
