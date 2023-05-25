using UnityEngine;


[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/Speaker")]
public class Speaker : ScriptableObject
{
    [SerializeField] private string speakerName;
    [SerializeField] private Sprite Icon;

    public string GetName()
    {
        return speakerName;
    }

    public Sprite GetIcon(){
        return Icon;
    }
}