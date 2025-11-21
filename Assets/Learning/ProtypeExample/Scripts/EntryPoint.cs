using Learning.Prototype;
using Learning.ProtypeExample.Scripts;
using UnityEngine;

namespace Learning.ProtypeExample {
    public class EntryPoint : MonoBehaviour {
        [SerializeField] private CameraControlWindows cameraControlWindowsPrefab;
        [SerializeField] private CameraControlAndroid cameraControlAndroidPrefab;

        private CameraHandler cameraHandler;
        private EntryPoint entryPoint;

        private void Awake() {
#if UNITY_ANDROID
            CameraControlAndroid cameraControlAndroid = Instantiate(cameraControlAndroidPrefab);
            cameraHandler = new CameraHandler(cameraControlAndroid);
#else
            CameraControlWindows cameraControlWindows = Instantiate(cameraControlWindowsPrefab);
            cameraHandler = new CameraHandler(cameraControlWindows);
#endif
        }

        private void Update() {
            cameraHandler?.Tick();
        }
    }
}
