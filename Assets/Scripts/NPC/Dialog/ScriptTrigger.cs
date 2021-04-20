using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptTrigger : MonoBehaviour
{
    private bool inTrigger = false;
    public bool inDialog = false;

    [Header("DialogTriggerEvent")]
    public UnityEvent dialogEvent;

    [Header("NextLineEvent")]
    public UnityEvent nextLineEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
        }
    }

    private void Update()
    {
        if (inTrigger == true && Input.GetKeyDown(KeyCode.E) && inDialog == false)
        {
            dialogEvent.Invoke();
        }

        if (inDialog == true && Input.GetKeyDown(KeyCode.Space))
        {
            nextLineEvent.Invoke();
        }
    }
}
