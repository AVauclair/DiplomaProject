using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text characterName;
    public TMP_Text lineDialog;
    public Image portrait;

    public Animator animator;

    private AudioSource audioSource;
    public AudioClip audioClip;

    private Queue<string> lines;

    public GameObject checker;

    void Start()
    {
        lines = new Queue<string>();

        audioSource = GetComponent<AudioSource>();
    }

    public void StartDialog(Dialog dialog)
    {
        checker.GetComponent<SelectDialog>().inDialog = true;

        animator.SetBool("isOpen", true);
        characterName.text = dialog.name;
        characterName.color = dialog.color;
        portrait.sprite = dialog.portrait;

        lines.Clear();
        foreach (string line in dialog.sentences)
        {
            lines.Enqueue(line);
        }
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialog();
            //return; знайте этого негодяя в лицо, из-за которого у меня часа полтора ушло на то, чтобы понять, почему диалогнамбер++ плюсует дважды
        }

        string line = lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence (string sentence)
    {
        lineDialog.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            lineDialog.text += letter;
            audioSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void EndDialog()
    {
        animator.SetBool("isOpen", false);
        checker.GetComponent<SelectDialog>().inDialog = false;
        checker.GetComponent<SelectDialog>().dialogNumber++;
    }

}
