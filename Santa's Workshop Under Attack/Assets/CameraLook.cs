using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string lookControl;

    private void Update()
    {
        if (Input.GetButton(lookControl))
        {
            float h = speed * Input.GetAxis("Mouse X");
            float v = speed * Input.GetAxis("Mouse Y");

            transform.rotation *= Quaternion.Euler(0, h, -v);
        }    
    }
}
