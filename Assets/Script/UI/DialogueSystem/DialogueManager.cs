using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName,
        dialogue;

    public int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    public bool isTalking;

    public Animator anim;
    private Coroutine typing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Skill") && isTalking == true)
        {
            ReadNext();
        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.anim.SetBool("isOpened", true);
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.isTalking = true;

        instance.ReadNext();
    }

    public static bool EndConversation()
    {
        if (instance.currentIndex > instance.currentConvo.GetLength())
        {
            return true;
        }
        else
            return false;
    }

    public void ReadNext()
    {
        if (instance.currentIndex > instance.currentConvo.GetLength())
        {
            instance.isTalking = false;
            instance.anim.SetBool("isOpened", false);
            instance.currentIndex = 0;
            return;
        }
        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
        //dialogue.text = currentConvo.GetLineByIndex(currentIndex).dialogue;
        if (typing == null)
        {
            typing = instance.StartCoroutine(
                TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue)
            );
        }
        else
        {
            instance.StopCoroutine(typing);
            typing = null;
            typing = instance.StartCoroutine(
                TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue)
            );
        }
        currentIndex++;
    }

    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";

        bool complete = false;
        int index = 0;

        while (!complete)
        {
            dialogue.text += text[index];
            index++;
            yield return new WaitForSeconds(0.02f);
            if (index == text.Length)
            {
                complete = true;
            }
        }

        typing = null;
    }
}
