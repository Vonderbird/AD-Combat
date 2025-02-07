using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Inject] private IGameService _service;

    void Start()
    {
        _service.Serve();
    }
}