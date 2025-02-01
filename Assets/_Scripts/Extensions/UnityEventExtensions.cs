using UnityEngine.Events;
using UnityEngine;

namespace ADC.Extensions
{
    public static class UnityEventExtensions
    {
        public static void AddListenerOnce(this UnityEvent unityEvent, UnityAction unityAction)
        {
            for (int index = 0; index < unityEvent.GetPersistentEventCount(); index++)
            {
                Object curEventObj = unityEvent.GetPersistentTarget(index);
                string curEventName = unityEvent.GetPersistentMethodName(index);
                Debug.Log("curEventName: " + curEventName + ", unityAction: " + unityAction.Method.Name);
                if ((Object)unityAction.Target == curEventObj)
                {
                    Debug.LogError("Event is already added: " + curEventName);
                    return;
                }
            }

            unityEvent.AddListener(unityAction);
        }

        public static void AddListenerOnce<T>(this UnityEvent<T> unityEvent, UnityAction<T> unityAction)
        {
            for (int index = 0; index < unityEvent.GetPersistentEventCount(); index++)
            {
                Object curEventObj = unityEvent.GetPersistentTarget(index);
                string curEventName = unityEvent.GetPersistentMethodName(index);
                Debug.Log("curEventName: " + curEventName + ", unityAction: " + unityAction.Method.Name);
                if ((Object)unityAction.Target == curEventObj)
                {
                    Debug.LogError("Event is already added: " + curEventName);
                    return;
                }
            }

            unityEvent.AddListener(unityAction);
        }
    }
}