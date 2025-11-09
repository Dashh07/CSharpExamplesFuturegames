namespace Template {
    /// <summary>
    /// Base class used by the platform factory to select the current platform and the input
    /// </summary>
    public class PlatformSelector {
        public readonly DevicePlatform devicePlatform;
        private InputMode inputMode;

        public PlatformSelector(DevicePlatform devicePlatform, InputMode inputMode) {
            this.devicePlatform = devicePlatform;
            this.inputMode = inputMode;
        }

        public void SetInputMode(InputMode newInputMode) {
            inputMode = newInputMode;
        }

        public static InputMode GetPlatformDefaultInputMode() {
#if UNITY_ANDROID
            return InputMode.XR;
#elif STEAMWORKS
            return InputMode.SteamVR;
#else
            return InputMode.Desktop;
#endif
        }

        public static DevicePlatform GetDevicePlatform() {
#if UNITY_ANDROID
            return DevicePlatform.XR;
#elif STEAMWORKS
            return DevicePlatform.SteamVR;
#else
            return DevicePlatform.Desktop;
#endif
        }
    }
}