using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public float Sensitivity = 3f;
    public float Speed = 15f;

    float rotationX = 0f;
    float rotationY = 0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float up = Input.GetAxis("Up");
        float speedMultiplier = 1f;
        if (Input.GetKey(KeyCode.LeftControl)) speedMultiplier = 2f;
        print(speedMultiplier);

        // Rotation
        rotationY += Input.GetAxis("Mouse X") * Sensitivity;
        rotationX += Input.GetAxis("Mouse Y") * Sensitivity;
        rotationX = Mathf.Clamp(rotationX, -89f, 89f);
        transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

        // Moving
        Vector2 forwardXZ = new Vector2(transform.forward.x, transform.forward.z).normalized;
        Vector3 forward = new Vector3(forwardXZ.x, 0, forwardXZ.y);
        Vector2 rightXZ = new Vector2(transform.right.x, transform.right.z).normalized;
        Vector3 right = new Vector3(rightXZ.x, 0, rightXZ.y);
        rb.velocity = (forward * vertical + right * horizontal + Vector3.up * up) * Speed * speedMultiplier;
    }
}
