using UnityEngine;

public class GrabMeshRenderers : MonoBehaviour
{

    private MeshRenderer[] _meshRenderers;
    public MeshRenderer[] MeshRenderers => _meshRenderers ??= GetComponentsInChildren<MeshRenderer>();
}