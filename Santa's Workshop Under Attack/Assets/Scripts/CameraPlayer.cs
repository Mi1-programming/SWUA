using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public float Sensitivity = 3f;
    public float Speed = 15f;

    bool controlsEnabled = true;
    float rotationX = 0f;
    float rotationY = 0f;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCursorMoveState(false);
    }
    void Update()
    {
        if (controlsEnabled)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float up = Input.GetAxis("Up");
            float speedMultiplier = 1f;
            if (Input.GetKey(KeyCode.LeftControl)) speedMultiplier = 2f;

            // Moving
            Transform childTransform = transform.GetChild(0);
            Vector2 forwardXZ = new Vector2(childTransform.forward.x, childTransform.forward.z).normalized;
            Vector3 forward = new Vector3(forwardXZ.x, 0, forwardXZ.y);
            Vector2 rightXZ = new Vector2(childTransform.right.x, childTransform.right.z).normalized;
            Vector3 right = new Vector3(rightXZ.x, 0, rightXZ.y);
            rb.velocity = (forward * vertical + right * horizontal + Vector3.up * up) * Speed * speedMultiplier;

            // Rotation
            rotationY += Input.GetAxis("Mouse X") * Sensitivity;
            rotationX += Input.GetAxis("Mouse Y") * Sensitivity;
            rotationX = Mathf.Clamp(rotationX, -89f, 89f);
            transform.GetChild(0).localEulerAngles = new Vector3(-rotationX, rotationY, 0);
        }
    }

    public void SetCursorMoveState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
        controlsEnabled = !state;
    }
}
