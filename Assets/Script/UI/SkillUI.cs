using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillUI : MonoBehaviour
{
    [Header("技能欄位")]
    public GameObject RabbitSkillUI;
    public GameObject RabbitSkillIcon;
    public GameObject WolfSkillUI;
    public GameObject WolfSkillIcon;
    public GameObject ChickenSkillUI;
    public GameObject ChickenSkillIcon;
    public GameObject CraneSkillUI;
    public GameObject CraneSkillIcon;

    [Header("背包互動")]
    public Button rabbitbtn;
    public GameObject rabbitIconHover;
    public Button rabbitMemorybtn;
    public GameObject rabbitMemoryIcon;

    public Button wolfbtn;
    public GameObject wolfIconHover;
    public Button wolfMemorybtn;
    public GameObject wolfMemoryIcon;

    public Button foxbtn;
    public GameObject foxIconHover;
    public Button foxMemorybtn;
    public GameObject foxMemoryIcon;

    public Button chickenbtn;
    public GameObject chickenIconHover;
    public Button chickenMemorybtn;
    public GameObject chickenMemoryIcon;

    public Button cranebtn;
    public GameObject craneIconHover;
    public Button craneMemorybtn;
    public GameObject craneMemoryIcon;

    public Button deerbtn;
    public GameObject deerIconHover;
    public Button deerMemorybtn;
    public GameObject deerMemoryIcon;

    public Sprite bg;
    public Sprite bgActive;

    private Vector3 activeScale = new Vector3(1.3125f, 1.3125f, 1.3125f);
    private Vector3 normalScale = new Vector3(1.0f, 1.0f, 1.0f);

    private Vector3 RabbitUIOrgin;
    private Vector3 WolfUIOrgin;
    private Vector3 ChickenUIOrgin;
    private Vector3 CraneUIOrgin;

    private void Start()
    {
        RabbitUIOrgin = RabbitSkillUI.transform.position;
        WolfUIOrgin = WolfSkillUI.transform.position;
        ChickenUIOrgin = ChickenSkillUI.transform.position;
        CraneUIOrgin = CraneSkillUI.transform.position;
    }

    private void Update()
    {
        if (RabbitSkillUI && RabbitSkillIcon && SwitchSkills.getSkill >= 1)
        {
            RabbitSkillIcon.SetActive(true);
            RabbitSkillUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (WolfSkillUI && WolfSkillIcon && SwitchSkills.getSkill >= 2)
        {
            WolfSkillIcon.SetActive(true);
            WolfSkillUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (ChickenSkillUI && ChickenSkillIcon && SwitchSkills.getSkill >= 3)
        {
            ChickenSkillIcon.SetActive(true);
            ChickenSkillUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (SwitchSkills.getSkill == 0)
            {
                if (RabbitSkillIcon.activeSelf == false)
                {
                    RabbitSkillIcon.SetActive(true);
                }
                if (WolfSkillIcon.activeSelf == false)
                {
                    WolfSkillIcon.SetActive(true);
                }
            }
            else
            {
                RabbitSkillUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                WolfSkillUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void SkillUITransition(int currentSkill)
    {
        if (currentSkill == 1)
        {
            SetOriginPos();
            SetActiveImage(RabbitSkillUI);
            SetScale(RabbitSkillUI, WolfSkillUI, ChickenSkillUI, CraneSkillUI);
            SetMove(5, 6.6f, 3.3f, 0);
        }
        else if (currentSkill == 2)
        {
            SetActiveImage(WolfSkillUI);
            SetScale(WolfSkillUI, RabbitSkillUI, ChickenSkillUI, CraneSkillUI);
            SetMove(-5, -5, 0, 0);
        }
        else if (currentSkill == 3)
        {
            SetActiveImage(ChickenSkillUI);
            SetScale(ChickenSkillUI, RabbitSkillUI, WolfSkillUI, CraneSkillUI);
            SetMove(0, -5, -5, 0);
        }
        else if (currentSkill == 4)
        {
            SetActiveImage(CraneSkillUI);
            SetScale(CraneSkillUI, RabbitSkillUI, WolfSkillUI, ChickenSkillUI);
            SetMove(0, 0, -5, -5);
        }
        else
        {
            SetActiveImage();
            SetMove(-5, -6.6f, -3.3f, 0);
            LeanTween.scale(RabbitSkillUI, normalScale, 0.5f).setEase(LeanTweenType.easeOutBounce);
        }
    }

    /// <summary>
    /// 技能UI縮放動畫
    /// </summary>
    private void SetScale(GameObject activeUI, GameObject else1, GameObject else2, GameObject else3)
    {
        LeanTween.scale(activeUI, activeScale, 0.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(else1, normalScale, 0.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(else2, normalScale, 0.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(else3, normalScale, 0.5f).setEase(LeanTweenType.easeOutBounce);
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
    private void SetOriginPos()
    {
        RabbitSkillUI.transform.position = RabbitUIOrgin;
        WolfSkillUI.transform.position = WolfUIOrgin;
        ChickenSkillUI.transform.position = ChickenUIOrgin;
        CraneSkillUI.transform.position = CraneUIOrgin;
    }

    /// <summary>
    /// 設定技能底圖
    /// </summary>
    private void SetActiveImage(GameObject target = null)
    {
        RabbitSkillUI.GetComponent<Image>().sprite = bg;
        WolfSkillUI.GetComponent<Image>().sprite = bg;
        ChickenSkillUI.GetComponent<Image>().sprite = bg;
        CraneSkillUI.GetComponent<Image>().sprite = bg;
        if (target != null)
            target.GetComponent<Image>().sprite = bgActive;
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
}
