using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject firstPortal;
    public GameObject secondPortal;

    private bool inTrigger = false;
    private bool isFirst = false;
    private bool isSecond = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "firstPortal")
        {
            isFirst = true;
        }

        else if (collision.tag == "secondPortal")
        {
            isSecond = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isFirst = false;
        isSecond = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isFirst == true)
            {
                transform.position = new Vector2(secondPortal.transform.position.x, secondPortal.transform.position.y);
            }
            else if (isSecond == true)
            {
                transform.position = new Vector2(firstPortal.transform.position.x, firstPortal.transform.position.y);
            }
        }
    }
}
