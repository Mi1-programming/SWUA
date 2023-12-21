using UnityEngine;

public class Blur : MonoBehaviour
{
    [SerializeField] private Shader shader;
    public float amount = 0.01f;
    public float apply = 1.0f;
    private Material newMat;

    public void SetApply(float newApply)
    {
        apply = newApply;
    }
    private void Awake()
    {
        newMat = new Material(shader);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        newMat.SetFloat("_Amount", amount);
        newMat.SetFloat("_Apply", apply);
        Graphics.Blit(source, destination, newMat);
    }
}
