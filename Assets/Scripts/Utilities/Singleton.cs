using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _applicationIsQuitting = false;

    protected bool DestroyOnLoad = true;

    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError($"[{typeof(T).Name}] Something went wrong - there should never be more than 1 singleton of type '{typeof(T)}'. Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        if(!_instance.DestroyOnLoad)
                            DontDestroyOnLoad(singletonObject);

                        Debug.Log($"[Singleton] An instance of '{typeof(T)}' was created "+ (_instance.DestroyOnLoad? "without": "with") + " DontDestroyOnLoad.");
                    }
                }

                return _instance;
            }
        }
    }

    private void OnDestroy()
    {
        _applicationIsQuitting = true;
    }

    // Optional: Clear the _applicationIsQuitting flag if needed (useful for play mode testing in the editor)
    protected virtual void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }
}
