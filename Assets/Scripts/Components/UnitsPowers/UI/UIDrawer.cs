using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ADC
{
    public class UIDrawer : MonoBehaviour
    {
        [SerializeField] private RectTransform statesDrawer;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private Vector2 closedPosition;
        [SerializeField] private Vector2 openPosition;
        // Start is called before the first frame update
        bool isOpening;
        bool isClosing;
        private Coroutine drawerTransition;

        public UnityEvent BeginClosing;
        public UnityEvent Closed;
        public UnityEvent BeginOpening;
        public UnityEvent Opened;

        public void OnActivate()
        {
            if (isOpening) return;

            if (isClosing)
            {
                StopTransition();
                isClosing = false;
            }
            BeginOpening?.Invoke();
            StartCoroutine(OpenDrawer());

        }

        public void OnDeactivate()
        {
            if (isClosing) return;
            StartCoroutine(CloseDrawer());

            if (isOpening)
            {
                StopTransition();
                isOpening = false;
            }
            BeginClosing?.Invoke();
        }

        private void StopTransition()
        {
            if (drawerTransition != null)
                StopCoroutine(drawerTransition);
        }

        private IEnumerator OpenDrawer()
        {
            isOpening = true;
            var elapsedTime = 0f;
            var startPosition = statesDrawer.anchoredPosition;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                statesDrawer.anchoredPosition = Vector2.Lerp(startPosition, openPosition, elapsedTime / animationDuration);
                yield return null;
            }

            statesDrawer.anchoredPosition = openPosition;
            isOpening = false;
            Opened?.Invoke();
        }

        private IEnumerator CloseDrawer()
        {
            isClosing = true;
            var elapsedTime = 0f;
            var startPosition = statesDrawer.anchoredPosition;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                statesDrawer.anchoredPosition = Vector2.Lerp(startPosition, closedPosition, elapsedTime / animationDuration);
                yield return null;
            }

            statesDrawer.anchoredPosition = closedPosition;
            isClosing = false;
            Closed?.Invoke();
        }
    }
}
