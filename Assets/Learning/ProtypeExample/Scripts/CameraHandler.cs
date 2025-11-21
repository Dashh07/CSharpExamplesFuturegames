using UnityEngine;

namespace Learning.Prototype {
public class CameraHandler {
        private readonly ICameraControl cameraControl;
        private float yaw = 0f;
        private float pitch = 0f;

        public CameraHandler(ICameraControl cameraControl) {
            this.cameraControl = cameraControl;
        }

        public void Tick() {
            // Camera movement

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = (cameraControl.CameraRefReference.transform.right * h) + (cameraControl.CameraRefReference.transform.forward * v);
            cameraControl.CameraRefReference.transform.position += move * cameraControl.MoveSpeed * Time.deltaTime;

            // Camera rotation
            if(Input.GetMouseButton(1)) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                yaw += cameraControl.LookSpeed * Input.GetAxis("Mouse X");
                pitch -= cameraControl.LookSpeed * Input.GetAxis("Mouse Y");
                pitch = Mathf.Clamp(pitch, -80f, 80f);
                cameraControl.CameraRefReference.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            }
            else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
