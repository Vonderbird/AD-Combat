using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ADC
{ 
    public class UnitStatsUIOnOff : MonoBehaviour
    {
        private UIOnOffTransition onOffTransition = UIOnOffTransition.Closed;
        [SerializeField] private RectTransform uiRectTransform;
        [SerializeField] private float openPosition = 0;
        [SerializeField] private float closePosition = -250;
        [SerializeField] private float transitionSpeed = 10f;
        [SerializeField] private Sprite openIcon;
        [SerializeField] private Sprite closeIcon;
        [SerializeField] private Image btnImage;

        private Coroutine scrollX;
        private Button btn;

        private void Awake()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                if (onOffTransition is UIOnOffTransition.Closed or UIOnOffTransition.Closing)
                    OpenPanel();
                else
                    ClosePanel();
            });
        }

        public void EnableButton()
        {
            btn.interactable = true;
        }
        public void DisableButton()
        {
            btn.interactable = false;
        }


        public void OpenPanel()
        {
            if (onOffTransition is UIOnOffTransition.Opened or UIOnOffTransition.Opening) return;
            if (scrollX != null)
            {
                StopCoroutine(scrollX);
                scrollX = null;
            }

            onOffTransition = UIOnOffTransition.Opening;
            btnImage.sprite = closeIcon;
            scrollX = StartCoroutine(MoveToTargetXPosition(openPosition, UIOnOffTransition.Opened));
        }

        public void ClosePanel()
        {
            if (onOffTransition is UIOnOffTransition.Closed or UIOnOffTransition.Closing) return;
            if (scrollX != null)
            {
                StopCoroutine(scrollX);
                scrollX = null;
            }
            onOffTransition = UIOnOffTransition.Closing;
            btnImage.sprite = openIcon;
            scrollX = StartCoroutine(MoveToTargetXPosition(closePosition, UIOnOffTransition.Closed)); // Adjust -250 based on the closed position
        }

        private IEnumerator MoveToTargetXPosition(float targetX, UIOnOffTransition finalState)
        {
            Vector3 startPosition = uiRectTransform.anchoredPosition;
            Vector3 targetPosition = new Vector3(targetX, startPosition.y, startPosition.z);

            while (Mathf.Abs(uiRectTransform.anchoredPosition.x - targetX) > 0.1f)
            {
                uiRectTransform.anchoredPosition = Vector3.Lerp(
                    uiRectTransform.anchoredPosition,
                    targetPosition,
                    Time.deltaTime * transitionSpeed
                );

                yield return null;
            }

            uiRectTransform.anchoredPosition = targetPosition; 
            onOffTransition = finalState;
        }
    }

    public enum UIOnOffTransition
    {
        Opening,
        Opened,
        Closing,
        Closed
    }
}