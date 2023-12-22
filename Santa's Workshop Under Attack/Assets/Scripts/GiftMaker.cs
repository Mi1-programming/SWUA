using UnityEngine;
using UnityEngine.Events;

public class GiftMaker : MonoBehaviour
{
    [SerializeField] private Building buildInput;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2Int previewResolution = new Vector2Int(128, 128);
    [SerializeField] private Object gift;
    [SerializeField] private UnityEvent<Vector3> onCreate;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 spawnVelocity;

    public void CreateGift()
    {
        Texture newPreview = SnapModel();
        GameObject newGift = Instantiate(gift, target.position, target.rotation) as GameObject;
        newGift.GetComponentInChildren<Rigidbody>().velocity = spawnVelocity;
        Gift newGiftComp = newGift.GetComponent<Gift>();
        newGiftComp.preview = newPreview;
        newGiftComp.model = buildInput.gen.Export(true);

        onCreate.Invoke(target.position);
    }
    private Texture SnapModel()
    {
        RenderTexture originalTarget = cam.targetTexture;

        RenderTexture preview = new RenderTexture(previewResolution.x, previewResolution.y, 0);
        cam.targetTexture = preview;
        cam.Render();

        cam.targetTexture = originalTarget;

        return preview;
    }
}
