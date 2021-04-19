using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Souls : MonoBehaviour
{
    public int souls = 0;

    public TextMeshProUGUI textSouls;

    private GameObject player;
    private AudioSource audioSource;
    public AudioClip audioClip;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Soul")
        {
            audioSource.PlayOneShot(audioClip);
            collision.transform.tag = "Untagged";
            Destroy(collision.gameObject);
            souls++;
            textSouls.text = souls.ToString();
        }
    }

}
