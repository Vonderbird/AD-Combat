using UnityEngine;

public class GrabSkinnedMeshes : MonoBehaviour
{
    private SkinnedMeshRenderer[] _skinnedMeshes;
    public SkinnedMeshRenderer[] SkinnedMeshes => _skinnedMeshes ??= GetComponentsInChildren<SkinnedMeshRenderer>();
}