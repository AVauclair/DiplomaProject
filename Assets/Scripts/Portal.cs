using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject alterPortal;

    private bool inTrigger = false;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inTrigger == true)
            {
                transform.position = new Vector2(alterPortal.transform.position.x, alterPortal.transform.position.y);
            }
        }
    }
}
