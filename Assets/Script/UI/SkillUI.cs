using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillUI : MonoBehaviour
{
    public static GameObject compoundLock;

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

    private GameObject level1Hover;
    private GameObject level2Hover;
    private GameObject level3Hover;
    private GameObject level4Hover;
    private GameObject level5Hover;
    private GameObject level6Hover;
    private GameObject level7Hover;
    private GameObject Map;

    [HideInInspector]
    public GameObject deerIcon;
    private GameObject deerIconHover;
    private GameObject deerMemoryIcon;

    public Sprite bg;
    public Sprite bgActive;

    private Vector3 activeScale = new Vector3(1.3125f, 1.3125f, 1.3125f);
    private Vector3 normalScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Color unGetColor = new Color(1f, 1f, 1f, 1f);
    private Color lockColor = new Color(1f, 1f, 1f, 0.5f);
    private Color unlockColor = new Color(1, 1, 1, 1);
    private Vector3 RabbitUIOrgin;
    private Vector3 WolfUIOrgin;
    private Vector3 ChickenUIOrgin;
    private Vector3 CraneUIOrgin;

    private void Start()
    {
        Map = GameObject.Find("Map");
        GetGameObject();
        RabbitUIOrgin = RabbitSkillUI.transform.position;
        WolfUIOrgin = WolfSkillUI.transform.position;
        ChickenUIOrgin = ChickenSkillUI.transform.position;
        CraneUIOrgin = CraneSkillUI.transform.position;
        compoundLock = GameObject
            .Find("ObjectToNextLevel")
            .transform.Find("Canvas/Package/Compound/lock")
            .gameObject;
    }

    private void Update()
    {
        RabbitSkillUI.GetComponent<Image>().color = unGetColor;
        WolfSkillUI.GetComponent<Image>().color = unGetColor;
        ChickenSkillUI.GetComponent<Image>().color = unGetColor;
        CraneSkillUI.GetComponent<Image>().color = unGetColor;
        RabbitSkillIcon.GetComponent<Image>().color = unGetColor;
        WolfSkillIcon.GetComponent<Image>().color = unGetColor;
        ChickenSkillIcon.GetComponent<Image>().color = unGetColor;
        CraneSkillIcon.GetComponent<Image>().color = unGetColor;

        if (SwitchSkills.getSkill == 0 || SwitchSkills.lockSkill || SwitchSkills.lockByMount)
        {
            RabbitSkillUI.GetComponent<Image>().color = lockColor;
            WolfSkillUI.GetComponent<Image>().color = lockColor;
            ChickenSkillUI.GetComponent<Image>().color = lockColor;
            CraneSkillUI.GetComponent<Image>().color = lockColor;
            RabbitSkillIcon.GetComponent<Image>().color = lockColor;
            WolfSkillIcon.GetComponent<Image>().color = lockColor;
            ChickenSkillIcon.GetComponent<Image>().color = lockColor;
            CraneSkillIcon.GetComponent<Image>().color = lockColor;
        }
        else
        {
            if (RabbitSkillUI && RabbitSkillIcon && SwitchSkills.getSkill >= 1)
            {
                RabbitSkillIcon.SetActive(true);
                RabbitSkillIcon.GetComponent<Image>().color = unlockColor;
                RabbitSkillUI.GetComponent<Image>().color = unlockColor;
            }

            if (WolfSkillUI && WolfSkillIcon && SwitchSkills.getSkill >= 2)
            {
                WolfSkillIcon.SetActive(true);
                WolfSkillIcon.GetComponent<Image>().color = unlockColor;
                WolfSkillUI.GetComponent<Image>().color = unlockColor;
            }

            if (ChickenSkillUI && ChickenSkillIcon && SwitchSkills.getSkill >= 3)
            {
                ChickenSkillIcon.SetActive(true);
                ChickenSkillIcon.GetComponent<Image>().color = unlockColor;
                ChickenSkillUI.GetComponent<Image>().color = unlockColor;
            }

            if (CraneSkillUI && CraneSkillIcon && SwitchSkills.getSkill >= 4)
            {
                CraneSkillIcon.SetActive(true);
                CraneSkillIcon.GetComponent<Image>().color = unlockColor;
                CraneSkillUI.GetComponent<Image>().color = unlockColor;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex >= 4)
        {
            compoundLock.SetActive(false);
        }
    }

    public void SkillUITransition(int currentSkill)
    {
        if (currentSkill == 1)
        {
            SetOrigin();
            SetActiveSkill(RabbitSkillUI);
            SetMove(5, 12f, 6f, 0);
        }
        else if (currentSkill == 2)
        {
            SetActiveSkill(WolfSkillUI);
            SetMove(-5, -7, 4, 0);
        }
        else if (currentSkill == 3)
        {
            SetActiveSkill(ChickenSkillUI);
            SetMove(0, -14, -14, 0);
        }
        else if (currentSkill == 4)
        {
            SetActiveSkill(CraneSkillUI);
            SetMove(0, -2, -17f, -16);
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
                SetPackage(
                    rabbitbtn,
                    rabbitIconHover,
                    rabbitMemorybtn,
                    rabbitMemoryIcon,
                    level1Hover
                );
                break;
            case 2:
                SetPackage(wolfbtn, wolfIconHover, wolfMemorybtn, wolfMemoryIcon, level2Hover);
                break;
            case 3:
                SetPackage(foxbtn, foxIconHover, foxMemorybtn, foxMemoryIcon, level3Hover);
                break;
            case 4:
                SetPackage(
                    chickenbtn,
                    chickenIconHover,
                    chickenMemorybtn,
                    chickenMemoryIcon,
                    level4Hover
                );
                break;
            case 5:
                SetPackage(cranebtn, craneIconHover, craneMemorybtn, craneMemoryIcon, level5Hover);
                break;
            case 6:
                SetPackage(deerbtn, deerIconHover, deerMemorybtn, deerMemoryIcon, level6Hover);
                break;
        }
    }

    private void SetPackage(
        Button animalBtn,
        GameObject animalIconHover,
        Button memoryBtn,
        GameObject memoryIcon,
        GameObject mapLevelHover
    )
    {
        animalBtn.interactable = true;
        animalIconHover.SetActive(false);
        memoryBtn.interactable = true;
        memoryIcon.SetActive(true);
        mapLevelHover.SetActive(false);
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

        level1Hover = GameObject.Find("level1").transform.GetChild(1).gameObject;
        level2Hover = GameObject.Find("level2").transform.GetChild(1).gameObject;
        level3Hover = GameObject.Find("level3").transform.GetChild(1).gameObject;
        level4Hover = GameObject.Find("level4").transform.GetChild(1).gameObject;
        level5Hover = GameObject.Find("level5").transform.GetChild(1).gameObject;
        level6Hover = GameObject.Find("level6").transform.GetChild(1).gameObject;
        level7Hover = GameObject.Find("level7").transform.GetChild(1).gameObject;
        Map.SetActive(false);
    }
}
