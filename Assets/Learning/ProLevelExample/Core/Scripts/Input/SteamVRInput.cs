namespace Template {
    public class SteamVRInput : IApplicationLifecycle {
        private readonly SteamVRInputSettings inputSettings;
        public SteamVRInput(InputSettings inputSettings) {
            this.inputSettings = (SteamVRInputSettings)inputSettings;
        }
        public void Initialize() { }
        public void Tick() { }
        public void Dispose() {
        }
    }
}