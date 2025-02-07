using UnityEngine;

// Example usage
public interface IGameService
{
    void Serve();
}

public class GameService : IGameService
{
    public void Serve() => Debug.Log("Service working!");
}