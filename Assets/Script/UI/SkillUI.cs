using System;
using UnityEngine.UI;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    [Header("技能欄位")]
    public GameObject RabbitSkillUI;
    public GameObject WolfSkillUI;
    public GameObject ChickenSkillUI;
    public GameObject CraneSkillUI;

    [Header("背包互動")]
    public Button rabbitbtn;
    public Button rabbitMemorybtn;
    public Button wolfbtn;
    public Button wolfMemorybtn;
    public Button foxbtn;
    public Button foxMemorybtn;
    public Button chickenbtn;
    public Button chickenMemorybtn;
    public Button cranebtn;
    public Button craneMemorybtn;
    public Button deerbtn;
    public Button deerMemorybtn;

    private GameObject RabbitSkillIcon;
    private GameObject WolfSkillIcon;
    private GameObject ChickenSkillIcon;
    private GameObject CraneSkillIcon;
    private GameObject rabbitIconHover;
    private GameObject rabbitMemoryIcon;
    private GameObject wolfIconHover;
    private GameObject wolfMemoryIcon;
    private GameObject foxIconHover;
    private GameObject foxMemoryIcon;
    private GameObject chickenIconHover;
    private GameObject chickenMemoryIcon;
    private GameObject craneIconHover;
    private GameObject craneMemoryIcon;

    [HideInInspector]
    public GameObject deerIcon;
    private GameObject deerIconHover;
    private GameObject deerMemoryIcon;

    public Sprite bg;
    public Sprite bgActive;

    private Vector3 activeScale = new Vector3(1.3125f, 1.3125f, 1.3125f);
    private Vector3 normalScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Color lockColor = new Color(0.8f, 0.8f, 0.8f, 1);
    private Color unlockColor = new Color(1, 1, 1, 1);
    private Vector3 RabbitUIOrgin;
    private Vector3 WolfUIOrgin;
    private Vector3 ChickenUIOrgin;
    private Vector3 CraneUIOrgin;

    private void Start()
    {
        GetGameObject();
        RabbitUIOrgin = RabbitSkillUI.transform.position;
        WolfUIOrgin = WolfSkillUI.transform.position;
        ChickenUIOrgin = ChickenSkillUI.transform.position;
        CraneUIOrgin = CraneSkillUI.transform.position;
    }

    private void Update()
    {
        if (SwitchSkills.getSkill == 0 || SwitchSkills.lockSkill || SwitchSkills.lockByMount)
        {
            RabbitSkillUI.GetComponent<Image>().color = lockColor;
            WolfSkillUI.GetComponent<Image>().color = lockColor;
            ChickenSkillUI.GetComponent<Image>().color = lockColor;
            CraneSkillUI.GetComponent<Image>().color = lockColor;
        }
        else
        {
            if (RabbitSkillUI && RabbitSkillIcon && SwitchSkills.getSkill >= 1)
            {
                RabbitSkillIcon.SetActive(true);
                RabbitSkillUI.GetComponent<Image>().color = unlockColor;
            }

            if (WolfSkillUI && WolfSkillIcon && SwitchSkills.getSkill >= 2)
            {
                WolfSkillIcon.SetActive(true);
                WolfSkillUI.GetComponent<Image>().color = unlockColor;
            }

            if (ChickenSkillUI && ChickenSkillIcon && SwitchSkills.getSkill >= 3)
            {
                ChickenSkillIcon.SetActive(true);
                ChickenSkillUI.GetComponent<Image>().color = unlockColor;
            }

            if (CraneSkillUI && CraneSkillIcon && SwitchSkills.getSkill >= 4)
            {
                CraneSkillIcon.SetActive(true);
                CraneSkillUI.GetComponent<Image>().color = unlockColor;
            }
        }
    }

    public void SkillUITransition(int currentSkill)
    {
        if (currentSkill == 1)
        {
            SetOrigin();
            SetActiveSkill(RabbitSkillUI);
            SetMove(5, 6.6f, 3.3f, 0);
        }
        else if (currentSkill == 2)
        {
            SetActiveSkill(WolfSkillUI);
            SetMove(-5, -5, 0, 0);
        }
        else if (currentSkill == 3)
        {
            SetActiveSkill(ChickenSkillUI);
            SetMove(0, -5, -5, 0);
        }
        else if (currentSkill == 4)
        {
            SetActiveSkill(CraneSkillUI);
            SetMove(0, 0, -5, -5);
        }
        else
        {
            SetActiveSkill();
            SetOrigin();
        }
    }

    /// <summary>
    /// 技能UI縮放動畫
    /// </summary>
    private void SetActiveSkill(GameObject activeUI = null)
    {
        GameObject[] AllUI = { RabbitSkillUI, WolfSkillUI, ChickenSkillUI, CraneSkillUI };
        foreach (GameObject item in AllUI)
        {
            item.GetComponent<Image>().sprite = bg;
        }

        if (activeUI != null)
        {
            LeanTween.scale(activeUI, activeScale, 0.5f).setEase(LeanTweenType.easeOutBounce);
            activeUI.GetComponent<Image>().sprite = bgActive;

            AllUI = Array.FindAll(AllUI, u => u != activeUI);
            foreach (GameObject item in AllUI)
            {
                LeanTween.scale(item, normalScale, 0.5f).setEase(LeanTweenType.easeOutBounce);
            }
        }
    }

    /// <summary>
    /// 技能UI移動動畫
    /// </summary>
    private void SetMove(float dis1, float dis2, float dis3, float dis4, float speed = 0.5f)
    {
        LeanTween
            .moveX(RabbitSkillUI, RabbitSkillUI.transform.position.x + dis1, speed)
            .setEase(LeanTweenType.easeOutBounce);
        LeanTween
            .moveX(WolfSkillUI, WolfSkillUI.transform.position.x + dis2, speed)
            .setEase(LeanTweenType.easeOutBounce);
        LeanTween
            .moveX(ChickenSkillUI, ChickenSkillUI.transform.position.x + dis3, speed)
            .setEase(LeanTweenType.easeOutBounce);
        LeanTween
            .moveX(CraneSkillUI, CraneSkillUI.transform.position.x + dis4, speed)
            .setEase(LeanTweenType.easeOutBounce);
    }

    /// <summary>
    /// 歸位
    /// </summary>
    private void SetOrigin()
    {
        LeanTween.scale(RabbitSkillUI, normalScale, 0f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(WolfSkillUI, normalScale, 0f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(ChickenSkillUI, normalScale, 0f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(CraneSkillUI, normalScale, 0f).setEase(LeanTweenType.easeOutBounce);
        RabbitSkillUI.transform.position = RabbitUIOrgin;
        WolfSkillUI.transform.position = WolfUIOrgin;
        ChickenSkillUI.transform.position = ChickenUIOrgin;
        CraneSkillUI.transform.position = CraneUIOrgin;
    }

    public void ClearLevel(int level)
    {
        switch (level)
        {
            case 1:
                SetPackage(rabbitbtn, rabbitIconHover, rabbitMemorybtn, rabbitMemoryIcon);
                break;
            case 2:
                SetPackage(wolfbtn, wolfIconHover, wolfMemorybtn, wolfMemoryIcon);
                break;
            case 3:
                SetPackage(foxbtn, foxIconHover, foxMemorybtn, foxMemoryIcon);
                break;
            case 4:
                SetPackage(chickenbtn, chickenIconHover, chickenMemorybtn, chickenMemoryIcon);
                break;
            case 5:
                SetPackage(cranebtn, craneIconHover, craneMemorybtn, craneMemoryIcon);
                break;
            case 6:
                SetPackage(deerbtn, deerIconHover, deerMemorybtn, deerMemoryIcon);
                break;
        }
    }

    private void SetPackage(
        Button animalBtn,
        GameObject animalIconHover,
        Button memoryBtn,
        GameObject memoryIcon
    )
    {
        animalBtn.interactable = true;
        animalIconHover.SetActive(false);
        memoryBtn.interactable = true;
        memoryIcon.SetActive(true);
    }

    private void GetGameObject()
    {
        RabbitSkillIcon = RabbitSkillUI.transform.GetChild(0).gameObject;
        WolfSkillIcon = WolfSkillUI.transform.GetChild(0).gameObject;
        ChickenSkillIcon = ChickenSkillUI.transform.GetChild(0).gameObject;
        CraneSkillIcon = CraneSkillUI.transform.GetChild(0).gameObject;

        rabbitIconHover = rabbitbtn.transform.GetChild(1).gameObject;
        rabbitMemoryIcon = rabbitMemorybtn.transform.GetChild(0).gameObject;
        wolfIconHover = wolfbtn.transform.GetChild(1).gameObject;
        wolfMemoryIcon = wolfMemorybtn.transform.GetChild(0).gameObject;
        foxIconHover = foxbtn.transform.GetChild(1).gameObject;
        foxMemoryIcon = foxMemorybtn.transform.GetChild(0).gameObject;
        chickenIconHover = chickenbtn.transform.GetChild(1).gameObject;
        chickenMemoryIcon = chickenMemorybtn.transform.GetChild(0).gameObject;
        craneIconHover = cranebtn.transform.GetChild(1).gameObject;
        craneMemoryIcon = craneMemorybtn.transform.GetChild(0).gameObject;
        deerIcon = deerbtn.transform.GetChild(0).gameObject;
        deerIconHover = deerbtn.transform.GetChild(1).gameObject;
        deerMemoryIcon = deerMemorybtn.transform.GetChild(0).gameObject;
    }
}
