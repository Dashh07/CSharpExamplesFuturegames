using Template;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project {
    [CreateAssetMenu(menuName = "Project/Settings/General/InitializerSettingsFile", fileName = "InitializerSettingsFile")]
    public class InitializerSettingsFile : ScriptableObject {
        public AssetReferenceT<DesktopPlatformSettings> desktopPlatformSettings;
        public AssetReferenceT<XRPlatformSettings> xrPlatformSettings;
        public AssetReferenceT<SteamVRPlatformSettings> steamVRPlatformSettings;
        public AssetReferenceT<BootstrapSettings> bootstrapAssetReference;
    }
}