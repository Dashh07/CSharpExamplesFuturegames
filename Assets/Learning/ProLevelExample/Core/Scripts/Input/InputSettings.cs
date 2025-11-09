using UnityEngine;
using UnityEngine.InputSystem;

namespace Template {
    public abstract class InputSettings : ScriptableObject {
        public InputMode inputMode;
        public InputAction debugMenu;
    }
}