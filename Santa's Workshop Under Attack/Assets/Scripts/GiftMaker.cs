using UnityEngine;
using UnityEngine.Events;

public class GiftMaker : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2Int previewResolution = new Vector2Int(128, 128);
    [SerializeField] private Object gift;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private Vector3 spawnVelocity;
    [SerializeField] private UnityEvent<Vector3> onCreate;

    public void CreateGift()
    {
        Texture newPreview = SnapModel();
        GameObject newGift = Instantiate(gift, spawnPos, Quaternion.identity) as GameObject;
        newGift.GetComponentInChildren<Rigidbody>().velocity = spawnVelocity;
        Gift newGiftComp = newGift.GetComponent<Gift>();
        newGiftComp.preview = newPreview;

        onCreate.Invoke(spawnPos);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnPos, 1.0f);
        Gizmos.DrawLine(spawnPos, spawnPos + spawnVelocity);
    }
}
