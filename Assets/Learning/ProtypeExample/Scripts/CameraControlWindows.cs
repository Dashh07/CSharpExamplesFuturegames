using UnityEngine;

namespace Learning.Prototype {
    public class CameraControlWindows : MonoBehaviour, ICameraControl {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float lookSpeed = 2f;
        [SerializeField] private Camera cameraRef;

        public Camera CameraRefReference => cameraRef;
        public float MoveSpeed => moveSpeed;
        public float LookSpeed => lookSpeed;
    }
}
