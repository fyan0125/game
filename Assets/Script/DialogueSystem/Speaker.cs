using UnityEngine;


[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/Speaker")]
public class Speaker : ScriptableObject
{
    [SerializeField] private string speakerName;

    public string GetName()
    {
        return speakerName;
    }
}