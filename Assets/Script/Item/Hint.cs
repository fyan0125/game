using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hint : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Image icon;
    public bool talkHint = true;
    public Sprite mouseIcon;
    public Sprite RIcon;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        icon = transform.GetChild(1).GetComponent<Image>();
        icon.sprite = talkHint ? RIcon : mouseIcon;
        text.text = talkHint ? "對話" : "選擇";
    }

    private void LateUpdate()
    {
        transform.LookAt(
            transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up
        );
    }
}
