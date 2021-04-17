using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Souls : MonoBehaviour
{
    public int souls = 0;

    public TextMeshProUGUI textSouls;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Soul")
        {
            collision.transform.tag = "Untagged";
            Destroy(collision.gameObject);
            souls++;
            textSouls.text = souls.ToString();
        }
    }

}
