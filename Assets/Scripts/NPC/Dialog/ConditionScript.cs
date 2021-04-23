using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionScript : MonoBehaviour
{
    public int sceneNumber = 0;

    [Header("ScenesObjects")]
    public GameObject dialogInPrison;
    public GameObject makingNoise;

    public void ConditionsChecker()
    {
        if (sceneNumber == 1)
        {
            makingNoise.SetActive(true);
        }
        if (sceneNumber == 2)
        {
            dialogInPrison.SetActive(false);
        }
    }
}
