using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialog
{
    public string name;
    public Sprite portrait;
    public Color color;

    [TextArea(3, 10)]
    public string[] sentences;
}
