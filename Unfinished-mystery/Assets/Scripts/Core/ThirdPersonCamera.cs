using UnityEngine;
using Unity.Cinemachine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public float horizontalSensitivity = 200f;
    public float verticalSensitivity = 150f;
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;

    [Header("References")]
    public Transform cameraFollowTarget; // The CameraFollow empty child object

    private float _xRotation = 0f;
    private float _yRotation = 0f;

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleCameraRotation();
        HandleCursorToggle();
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;

        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, minVerticalAngle, maxVerticalAngle);

        // Rotate the follow target — Cinemachine reads from this
        cameraFollowTarget.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }

    void HandleCursorToggle()
    {
        // Press Escape to unlock cursor (useful during development)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}