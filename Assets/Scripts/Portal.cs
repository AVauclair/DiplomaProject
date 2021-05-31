using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject alterPortal;
    public GameObject player;

    private bool inTrigger = false;
    public bool teleportAuto;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }

    private void Update()
    {
        if (teleportAuto == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inTrigger == true)
                {
                    player.transform.position = new Vector2(alterPortal.transform.position.x, alterPortal.transform.position.y);
                }
            }
        }
        else
        {
            if (inTrigger == true)
            {
                player.transform.position = new Vector2(alterPortal.transform.position.x, alterPortal.transform.position.y);
            }
        }
    }
}
