using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ConditionScript : MonoBehaviour
{
    public int sceneNumber = 0;
    public GameObject firstCutscene;
    public GameObject tilemapDoor1;
    public GameObject tilemapDoor2;
    public GameObject prisoner;

    [Header("ScenesObjects")]
    public GameObject dialogInPrison;
    public GameObject makingNoise;
    public GameObject getFree;
    public GameObject dialogBeforePortal;
    public GameObject dialogBeforePills;
    public GameObject dialogBeforeWood;
    public GameObject dialogBeforeVox;
    public GameObject dialogWithVox;
    public GameObject dialogBeforeEnd;

    [Header("Cutscenes")]
    public PlayableDirector takeGuardian;

    public void ConditionsChecker()
    {
        if (sceneNumber > 0)
        {
            Destroy(firstCutscene);
        }
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
            dialogBeforeWood.SetActive(true);
        }
        if (sceneNumber == 6)
        {
            dialogBeforeWood.SetActive(false);
            dialogBeforeVox.SetActive(true);
        }
        if (sceneNumber == 7)
        {
            tilemapDoor2.SetActive(false);
            dialogBeforeVox.SetActive(false);
            dialogWithVox.SetActive(true);
        }
        if (sceneNumber == 8)
        {
            Destroy(prisoner);
            tilemapDoor1.SetActive(false);
            dialogWithVox.SetActive(false);
            dialogBeforeEnd.SetActive(true);
        }
        if (sceneNumber == 9)
        {
            dialogBeforeEnd.SetActive(false);
        }
    }
}
