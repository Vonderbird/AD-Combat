using UnityEngine;

// MonoBehaviour-aware resolver
public class MonoInjector : MonoBehaviour
{
    private static DiContainer _container;
    private static bool _isInitialized;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (_isInitialized) return;
        
        _container = new DiContainer();
        _isInitialized = true;
        
        // Register core Unity-related types
        _container.RegisterFactory(() => new GameObject("DynamicObject"));
    }

    public static DiContainer Container => _container;

    void Awake()
    {
        if (!_isInitialized) Initialize();
        ProcessInjection(gameObject);
    }

    public static void ProcessInjection(GameObject target)
    {
        foreach (var component in target.GetComponentsInChildren<MonoBehaviour>(true))
        {
            if (component != null)
            {
                _container.InjectDependencies(component);
            }
        }
    }
}