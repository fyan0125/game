using UnityEngine;

[CreateAssetMenu(menuName = "AnimalDiscription")]
public class AnimalDiscription : ScriptableObject
{
    public Sprite[] Image;
    public string Name;
    public string Skill;

    [TextArea(4, 4)]
    public string Habbit;

    [TextArea(4, 4)]
    public string Story;
}
