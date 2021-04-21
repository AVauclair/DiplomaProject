using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDialog : MonoBehaviour
{
    [Header("DialogObjects")]
    public GameObject dialog1;
    public GameObject dialog2;
    public GameObject dialog3;
    public GameObject dialog4;
    public GameObject dialog5;


    public bool inTrigger = false;
    public bool inDialog = false;
    public int dialogNumber = 1;

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
                switch (dialogNumber)
                {
                    case 1:
                        dialog1.GetComponent<ScriptTrigger>().dialogEvent.Invoke();
                        break;
                    case 2:
                        dialog2.GetComponent<ScriptTrigger>().dialogEvent.Invoke();
                        break;
                    case 3:
                        dialog3.GetComponent<ScriptTrigger>().dialogEvent.Invoke();
                        break;
                    case 4:
                        dialog4.GetComponent<ScriptTrigger>().dialogEvent.Invoke();
                        break;
                    case 5:
                        dialog5.GetComponent<ScriptTrigger>().dialogEvent.Invoke();
                        break;
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
                switch (dialogNumber)
                {
                    case 1:
                        dialog1.GetComponent<ScriptTrigger>().nextLineEvent.Invoke();
                        break;
                    case 2:
                        dialog2.GetComponent<ScriptTrigger>().nextLineEvent.Invoke();
                        break;
                    case 3:
                        dialog3.GetComponent<ScriptTrigger>().nextLineEvent.Invoke();
                        break;
                    case 4:
                        dialog4.GetComponent<ScriptTrigger>().nextLineEvent.Invoke();
                        break;
                    case 5:
                        dialog5.GetComponent<ScriptTrigger>().nextLineEvent.Invoke();
                        break;
                }
            }
            catch { }
        }
    }
}
