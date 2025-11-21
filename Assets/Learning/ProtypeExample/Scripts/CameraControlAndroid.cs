using Learning.Prototype;
using UnityEngine;

namespace Learning.ProtypeExample.Scripts {
    public class CameraControlAndroid:MonoBehaviour, ICameraControl {

        [field: SerializeField]
        public Camera CameraRefReference { get; private set; }

        [field: SerializeField]
        public float MoveSpeed { get; private set; }

        [field: SerializeField]
        public float LookSpeed { get; private set; }
    }
}
