using UnityEngine;

// Setup in scene
public class SceneBootstrapper : MonoBehaviour
{
    void Awake()
    {
        MonoInjector.Container.RegisterSingleton<IGameService, GameService>();
        
        // Register all MonoBehaviours in scene
        foreach (var mb in FindObjectsOfType<MonoBehaviour>())
        {
            MonoInjector.ProcessInjection(mb.gameObject);
        }
    }
}