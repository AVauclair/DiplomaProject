using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Grab" || collision.gameObject.tag == "Box")
        {
            GetComponentInParent<PlayerController>().ground = true;
            FindObjectOfType<PlayerController>().jumpCount = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Grab" || collision.gameObject.tag == "Box")
        {
            GetComponentInParent<PlayerController>().ground = false;
        }
    }
}
