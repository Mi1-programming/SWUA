using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private MeshRenderer previewRenderer;

    [HideInInspector] public Texture preview;
    [HideInInspector] public Vector3Int[] model;
    
    private void Start()
    {
        previewRenderer.material.SetTexture("_MainTex", preview);
    }
}
