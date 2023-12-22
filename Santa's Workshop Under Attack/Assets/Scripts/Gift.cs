using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private MeshRenderer previewRenderer;

    [Header("DEBUG")] // these must be public because the collider is in a bad place and the AI has to detect for the gift...
    public Texture preview;
    public Mesh model;
    public float condition;
    public Weapon weapon;
    
    private void Start()
    {
        previewRenderer.material.SetTexture("_MainTex", preview);
    }
}
