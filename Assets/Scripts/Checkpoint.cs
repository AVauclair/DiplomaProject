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
            PlayerPrefs.SetFloat("rightLimit", FindObjectOfType<WatchPlayer>().rightLimit *= 5);
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
