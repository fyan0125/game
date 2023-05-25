using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private TextMeshProUGUI speakerName,
        dialogue;
    private Image speakerIcon;

    public int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    public static bool isTalking;

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
        speakerIcon = anim.transform.Find("Image").GetComponent<Image>();
        speakerName = anim.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        dialogue = anim.transform.Find("Text").GetComponent<TextMeshProUGUI>();
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
        instance.speakerIcon.sprite = null;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        isTalking = true;
        SwitchSkills.lockSkill = true;

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
            isTalking = false;
            SwitchSkills.lockSkill = false;
            instance.anim.SetBool("isOpened", false);
            instance.currentIndex = 0;
            return;
        }
        speakerIcon.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetIcon();
        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
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
