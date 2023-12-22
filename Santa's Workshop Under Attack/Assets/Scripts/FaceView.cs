using UnityEngine;

public class FaceView : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    private void LateUpdate()
    {
        transform.forward = cam.transform.forward;
    }
}
