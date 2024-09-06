using RTSEngine.UI;
using RTSEngine;
using RTSEngine.Entities;
using UnityEngine;

[System.Serializable]
public struct UnitSpawnData
{
    [SerializeField] private EntityComponentTaskUIAsset taskUI;
    public EntityComponentTaskUIAsset TaskUi => taskUI;
    //[SerializeField]
    //private UnitCreationTask taskUI;

    [SerializeField]
    [EnforceType(typeof(IUnit), sameScene: false, prefabOnly: true)]
    private GameObject unitPrefab;

    public GameObject UnitPrefab => unitPrefab;
}
