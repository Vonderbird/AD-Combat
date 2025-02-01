using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ADC.UnitCreation
{

    public class SpawnUnitActivatorButton : MonoBehaviour
    {
        [SerializeField] private Image highlightBackground;
        [SerializeField] private Image[] highlightImages;
        [SerializeField] private TextMeshProUGUI[] highlightTexts;
        [SerializeField] private Color highlightBackgroundColor;
        [SerializeField] private Color highlightColor;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI price;

        private Color defaultBackgroundColor;
        private Color[] defaultImageColors;
        private Color[] defaultTextColors;
        private void Awake()
        {
            defaultBackgroundColor = highlightBackground.color;
            defaultImageColors = new Color[highlightImages.Length];
            for (var i = 0; i < highlightImages.Length; i++)
            {
                defaultImageColors[i] = highlightImages[i].color;
            }
            defaultTextColors = new Color[highlightImages.Length];
            for (var i = 0; i < highlightTexts.Length; i++)
            {
                defaultTextColors[i] = highlightTexts[i].color;
            }
        }

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
            //Debug.Log("Button Is Activated");
            highlightBackground.color = highlightBackgroundColor;
            foreach (var highlightImage in highlightImages)
                highlightImage.color = highlightColor;
            foreach (var highlightText in highlightTexts)
                highlightText.color = highlightColor;
        }

        public void OnDeactivateButton()
        {
            //Debug.Log("Button Is Deactivated");
            highlightBackground.color = defaultBackgroundColor;
            for (var i = 0; i < highlightImages.Length; i++)
                highlightImages[i].color = defaultImageColors[i];
            for (var i = 0; i < highlightTexts.Length; i++)
                highlightTexts[i].color = defaultTextColors[i];
        }
    }
}