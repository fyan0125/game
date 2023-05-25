using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeAnimal : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI Name,
        Skill,
        Habbit,
        Story;
    public AnimalDiscription Rabbit,
        Wolf,
        Fox,
        Chicken,
        Crane,
        Deer,
        Crow;
    private attendantManager AttendantManager;

    private void Start()
    {
        AttendantManager = GameObject.Find("GameManager").GetComponent<attendantManager>();
        image.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        image.color = new Color(1, 1, 1, 1);
        if (AttendantManager.rabbitArea.activeSelf)
        {
            Change(Rabbit);
        }
        else if (AttendantManager.wolfArea.activeSelf)
        {
            Change(Wolf);
        }
        else if (AttendantManager.foxArea.activeSelf)
        {
            Change(Fox);
        }
        else if (AttendantManager.chickenArea.activeSelf)
        {
            Change(Chicken);
        }
        else if (AttendantManager.craneArea.activeSelf)
        {
            Change(Crane);
        }
        else if (AttendantManager.deerArea.activeSelf)
        {
            Change(Deer);
        }
        else if (AttendantManager.crowArea.activeSelf)
        {
            Change(Crow);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0);
        }
    }

    private void Change(AnimalDiscription animal)
    {
        image.sprite = animal.Image;
        Name.text = animal.Name;
        Skill.text = animal.Skill;
        Habbit.text = animal.Habbit;
        Story.text = animal.Story;
    }
}
