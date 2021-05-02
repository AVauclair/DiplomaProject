using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform objectHolder;
    public float rayDistance;
    private bool isGrabed = false;

    private void Update()
    {
        RaycastHit2D grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDistance);

        if (grabcheck.collider != null && (grabcheck.collider.tag == "Grab" || grabcheck.collider.tag == "Box"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isGrabed = !isGrabed;

                if (isGrabed == true)
                {
                    grabcheck.collider.gameObject.transform.parent = objectHolder;
                    grabcheck.collider.gameObject.transform.position = objectHolder.position;
                    grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    //grabcheck.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    grabcheck.collider.gameObject.transform.parent = null;
                    //grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    grabcheck.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;

                    if (grabcheck.collider.tag == "Box")
                    {
                        grabcheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    }
                }
            }
        }
    }
}
