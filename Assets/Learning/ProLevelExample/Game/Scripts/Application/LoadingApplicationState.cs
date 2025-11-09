namespace Project {
    /// <summary>
    /// This is a state which would normally consist of a load screen scene or anything similar
    /// </summary>
    public class LoadingApplicationState : IApplicationState {
        public bool IsApplicationStateInitialized { get; set; }

        public void EnterApplicationState() {
        }
        public ApplicationState Tick() {
            return ApplicationState.Loading;
        }
        public void LateTick() { }
        public void ExitApplicationState() {
        }
        public void Dispose() {
        }

    }
}