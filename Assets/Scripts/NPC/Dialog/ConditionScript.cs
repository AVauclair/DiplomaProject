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
    public GameObject tilemapDoor3;
    public GameObject prisoner;

    [Header("Equipment")]
    public Image imageSlot1;
    public Sprite keySprite;
    public Sprite soulSprite;

    [Header("ScenesObjects")]
    public GameObject dialogInPrison;
    public GameObject makingNoise;
    public GameObject getFree;
    public GameObject dialogBeforePills;
    public GameObject dialogBeforeWood;
    public GameObject dialogBeforeVox;
    public GameObject dialogWithVox;
    public GameObject dialogBeforeEnd;
    public GameObject dialogEnd;

    [Header("SceneObjectsHub1")]
    public GameObject dialogBeforeEndH1;

    [Header("SceneObjectsL2")]
    public GameObject dialogStartPushL2;
    public GameObject dialogStartJumpL2;
    public GameObject dialogMaze;

    [Header("SceneObjectsHub2")]
    public GameObject dialogWithTrader;
    public GameObject GetAbility;
    public GameObject dialogWithVoxH2;

    [Header("SceneObjectsL3")]
    public GameObject tilemapDoorL3;
    public GameObject dialogWithKnightL3;
    public GameObject ClosingDoorTrigger;
    public GameObject musicObject;
    public AudioClip bossFightSong;
    public AudioClip defaultSong;

    [Header("Cutscenes")]
    public PlayableDirector takeGuardian;
    public PlayableDirector EndingDeath;
    public PlayableDirector EndingWin;
    public PlayableDirector endH2;
    public PlayableDirector L3;

    public void ConditionsChecker()
    {
        if (sceneNumber > 0)
        {
            Destroy(firstCutscene);
        }
        if (sceneNumber == 1)
        {
            makingNoise.SetActive(true);
            PlayerPrefs.SetInt("maxHP", 100);
            PlayerPrefs.SetInt("maxJumpValue", 1);
        }
        if (sceneNumber == 2)
        {
            takeGuardian.Play();
            StartCoroutine(FindObjectOfType<PlayerController>().CutScene2());

            makingNoise.SetActive(false);
            dialogInPrison.SetActive(false);
            getFree.SetActive(true);
        }
        if (sceneNumber == 3)
        {
            imageSlot1.sprite = keySprite;
            FindObjectOfType<PlayerController>().havingKey = 1;

            getFree.SetActive(false);
            if (PlayerPrefs.GetInt("checkpointNumber") != 1)
            {
                tilemapDoor1.SetActive(false);
            }
            dialogBeforePills.SetActive(true);
        }
        if (sceneNumber == 4)
        {
            dialogBeforePills.SetActive(false);
            dialogBeforeWood.SetActive(true);
        }
        if (sceneNumber == 5)
        {
            dialogBeforeWood.SetActive(false);
            dialogBeforeVox.SetActive(true);
        }
        if (sceneNumber == 6)
        {
            imageSlot1.sprite = null;
            FindObjectOfType<PlayerController>().havingKey = 0;
            tilemapDoor3.SetActive(false);

            dialogBeforeVox.SetActive(false);
            dialogWithVox.SetActive(true);
        }
        if (sceneNumber == 7)
        {
            Destroy(prisoner);
            tilemapDoor2.SetActive(false);

            dialogWithVox.SetActive(false);
            dialogBeforeEnd.SetActive(true);
        }
        if (sceneNumber == 8)
        {
            dialogBeforeEnd.SetActive(false);
            dialogEnd.SetActive(true);
        }
        if (sceneNumber == 9)
        {
            imageSlot1.sprite = soulSprite;
            FindObjectOfType<PlayerController>().havingWarriorSoul = 1;
            dialogEnd.SetActive(false);
        }
        if (sceneNumber == 10)
        {
            //nothing, that's ok (10 == finishing 1st level)
        }
        if (sceneNumber == 11)
        {
            Destroy(dialogBeforeEndH1);
        }
        if (sceneNumber == 12)
        {
            //nothing, that's ok (12 == finishing 1st hub)
        }
        if (sceneNumber == 13)
        {
            dialogStartPushL2.SetActive(false);
            dialogStartJumpL2.SetActive(false);
        }
        if (sceneNumber == 14)
        {
            dialogMaze.SetActive(false);
        }
        if (sceneNumber == 15)
        {
            //nothing, that's ok (15 == finishing 2nd level)
        }
        if (sceneNumber == 16)
        {
            dialogWithTrader.SetActive(false);
            GetAbility.SetActive(true);
        }

        if (sceneNumber == 17)
        {
            GetAbility.SetActive(false);
            dialogWithVoxH2.SetActive(true);
            FindObjectOfType<PlayerController>().maxJumpValue = 2;
            FindObjectOfType<PlayerController>().pushImpulse = 1000;
        }
        if (sceneNumber == 18)
        {
            endH2.Play();
            StartCoroutine(FindObjectOfType<PlayerController>().CutScene5());
        }
        if (sceneNumber == 19)
        {
            //nothing, that's ok (19 == finishing 2nd hub)
        }
        if (sceneNumber == 20)
        {
            dialogWithKnightL3.SetActive(false);
            tilemapDoorL3.SetActive(true);
            musicObject.GetComponent<AudioSource>().Stop();
            musicObject.GetComponent<AudioSource>().PlayOneShot(bossFightSong);
            L3.Play();
            StartCoroutine(FindObjectOfType<PlayerController>().CutScene6());
            FindObjectOfType<Knight>().hpObject.SetActive(true);
            sceneNumber++;
            PlayerPrefs.SetInt("sceneNumber", FindObjectOfType<ConditionScript>().sceneNumber);
            PlayerPrefs.Save();
        }
        if (sceneNumber == 21)
        {
            ClosingDoorTrigger.SetActive(true);
        }
    }
}
