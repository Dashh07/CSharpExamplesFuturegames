using System.Collections;

namespace Template {

    /// <summary>
    /// Add here your platforms as needed
    /// </summary>
    public enum DevicePlatform {
        Desktop,
        XR,
        SteamVR
    }

    /// <summary>
    /// Factory interface for platform selection and initialization
    /// </summary>
    public interface IPlatform {
        IEnumerator Initialize(object applicationData);
        IApplicationLifecycle InputHandler();
        void Tick();
        void Dispose();
        void OnApplicationQuit();
    }
}