
using RTSEngine.Game;
using RTSEngine.UI;

namespace RTSEngine.Utilities
{
    public interface IPoolableObject : IMonoBehaviour
    {
        string Code { get; }

        void Init(IGameManager gameMgr);
    }


    public abstract class HoverHealthBarBase: PoolableObject
    {
        //public abstract void OnSpawn(HoverHealthBarSpawnInput input);
    }
}
