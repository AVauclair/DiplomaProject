using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDialog : MonoBehaviour
{
    public GameObject[] dialogsObjects;

    public bool inTrigger = false;
    public bool inDialog = false;
    public bool willRepeat = false;
    public int repeat = 0;

    public int dialogNumber = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = true;
            FindObjectOfType<DialogManager>().checker = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
            FindObjectOfType<DialogManager>().checker = null;
        }
    }

    void Update()
    {
        DialogPicker();
        DialogNextLine();
    }

    private void DialogPicker()
    {
        if (inTrigger == true && Input.GetKeyDown(KeyCode.E) && inDialog == false)
        {
            try
            {
                for (int i = -1; i < dialogNumber; i++)
                {
                    dialogsObjects[dialogNumber].GetComponent<ScriptTrigger>().dialogEvent.Invoke();
                }
            }
            catch { }
        }
    }

    private void DialogNextLine()
    {
        if (inDialog == true && Input.GetKeyDown(KeyCode.Space))
        {
            try
            {
                for (int i = -1; i < dialogNumber; i++)
                {
                    dialogsObjects[dialogNumber].GetComponent<ScriptTrigger>().nextLineEvent.Invoke();
                }
            }
            catch { }
        }
    }
}
