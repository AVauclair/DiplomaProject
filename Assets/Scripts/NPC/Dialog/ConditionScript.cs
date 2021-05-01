using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ConditionScript : MonoBehaviour
{
    public int sceneNumber = 0;

    [Header("ScenesObjects")]
    public GameObject dialogInPrison;
    public GameObject makingNoise;
    public GameObject getFree;
    public GameObject dialogBeforePortal;
    public GameObject dialogBeforePills;

    [Header("Cutscenes")]
    public PlayableDirector takeGuardian;

    public void ConditionsChecker()
    {
        if (sceneNumber == 1)
        {
            makingNoise.SetActive(true);
        }
        if (sceneNumber == 2)
        {
            takeGuardian.Play();
            makingNoise.SetActive(false);
            dialogInPrison.SetActive(false);
            getFree.SetActive(true);
        }
        if (sceneNumber == 3)
        {
            getFree.SetActive(false);
            dialogBeforePortal.SetActive(true);
        }
        if (sceneNumber == 4)
        {
            dialogBeforePortal.SetActive(false);
            dialogBeforePills.SetActive(true);
        }
        if (sceneNumber == 5)
        {
            dialogBeforePills.SetActive(false);
        }
    }
}
