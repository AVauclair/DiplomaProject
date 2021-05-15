using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCount : MonoBehaviour
{
    public int levelNumber = 1;

    public void Start()
    {
        levelNumber = PlayerPrefs.GetInt("levelNumber");

        if (levelNumber == 0)
        {
            levelNumber++;
        }
    }
}
