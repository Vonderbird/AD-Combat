using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ADC.UnitCreation
{

    public class SpawnUnitActivatorButton : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI price;


        public void SetIconAndText(string title, Sprite icon, float price)
        {
            SpawnUnitActivated = new UnityEvent();
            this.icon.sprite = icon;
            this.title.text = title;
            this.price.text = $"{price:F0}";
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
}