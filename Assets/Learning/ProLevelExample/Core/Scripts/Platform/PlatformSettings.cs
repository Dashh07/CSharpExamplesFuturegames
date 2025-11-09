using UnityEngine;

namespace Template {
    public abstract class PlatformSettings : ScriptableObject {
        public DevicePlatform devicePlatform;
        public InputSettings inputSettings;
    }
}