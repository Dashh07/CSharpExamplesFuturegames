using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour {
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;

    public Camera CurrentCamera { get; set; }

    private float yaw = 0f;
    private float pitch = 0f;

    private void Awake() {
        CurrentCamera = GetComponent<Camera>();
    }

    private void Update() {
        // Camera movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * h) + (transform.forward * v);
        transform.position += move * moveSpeed * Time.deltaTime;

        // Camera rotation
        if(Input.GetMouseButton(1)) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            yaw += lookSpeed * Input.GetAxis("Mouse X");
            pitch -= lookSpeed * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -80f, 80f);
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
