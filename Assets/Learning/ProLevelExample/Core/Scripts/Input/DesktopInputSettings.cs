using UnityEngine;
using UnityEngine.InputSystem;

namespace Template {
    [CreateAssetMenu(fileName = "DesktopInputSettings", menuName = "Template/Settings/Input/DesktopInputSettings")]
    public class DesktopInputSettings : InputSettings {
        public InputAction movementAction;
        public InputAction gamepadCameraRotation;
        public InputAction increaseMovementSpeed;
        public InputAction decreaseMovementSpeed;
    }
}