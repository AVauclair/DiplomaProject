using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptTrigger : MonoBehaviour
{

    [Header("DialogTriggerEvent")]
    public UnityEvent dialogEvent;

    [Header("NextLineEvent")]
    public UnityEvent nextLineEvent;
}
