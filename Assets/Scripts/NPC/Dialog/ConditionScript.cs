using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ConditionScript : MonoBehaviour
{
    public int sceneNumber = 0;
    public GameObject firstCutscene;
    public GameObject tilemapDoor1;
    public GameObject tilemapDoor2;
    public GameObject prisoner;

    [Header("Equipment")]
    public Image imageSlot1;
    public Sprite keySprite;
    public Sprite soulSprite;

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
    public GameObject dialogEnd;

    [Header("Cutscenes")]
    public PlayableDirector takeGuardian;
    public PlayableDirector EndingDeath;
    public PlayableDirector EndingWin;

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
        }
        if (sceneNumber == 3)
        {
            imageSlot1.sprite = keySprite;
            FindObjectOfType<PlayerController>().havingKey = 1;

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
            imageSlot1.sprite = null;
            FindObjectOfType<PlayerController>().havingKey = 0;
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
            dialogEnd.SetActive(true);
        }
        if (sceneNumber == 10)
        {
            imageSlot1.sprite = soulSprite;
            FindObjectOfType<PlayerController>().havingWarriorSoul = 1;
            dialogEnd.SetActive(false);
        }
    }
}
