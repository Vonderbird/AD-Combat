using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpawnUnitActivatorButton : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;


    public void SetIconAndText(string text, Sprite icon)
    {
        SpawnUnitActivated = new UnityEvent();
        this.icon.sprite = icon;
        this.text.text = text;
    }

    public UnityEvent SpawnUnitActivated;

    public void OnClicked()
    {
        SpawnUnitActivated?.Invoke();
    }

    public void OnActivateButton()
    {
        Debug.Log("Button Is Activated");
        background.color = new Color(115.0f / 255.0f, 0.0f / 255.0f, 25.0f / 255.0f, 255.0f / 255.0f);
    }

    public void OnDeactivateButton()
    {
        background.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}