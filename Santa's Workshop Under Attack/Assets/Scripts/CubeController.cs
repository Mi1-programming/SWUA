using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string lookControl;
    private void Update()
    {
        if (Input.GetButton(lookControl))
        {
            float h = speed * Input.GetAxis("Mouse X");
            float v = speed * Input.GetAxis("Mouse Y");

            transform.Rotate(new Vector3(v, -h, 0), Space.World);
        }
    }
}
