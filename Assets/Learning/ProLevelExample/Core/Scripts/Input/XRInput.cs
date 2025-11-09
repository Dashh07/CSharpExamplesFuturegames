namespace Template {
    public class XRInput : IApplicationLifecycle {
        private readonly XRInputSettings inputSettings;
        public XRInput(InputSettings inputSettings) {
            this.inputSettings = (XRInputSettings)inputSettings;
        }
        public void Tick() { }
        public void Dispose() {
        }
        public void Initialize() { }
    }
}