using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private MeshRenderer previewRenderer;

    [Header("DEBUG")]
    public Texture preview;
    public Mesh model;
    
    private void Start()
    {
        previewRenderer.material.SetTexture("_MainTex", preview);
    }
}
