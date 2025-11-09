using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Template {
    public class SteamVRPlatform : IPlatform {
        private readonly IPlatform platform;
        private SteamVRPlatformSettings steamVRPlatformSettings;
        private readonly AssetReference assetReference;

        public SteamVRPlatform(AssetReference assetReference) {
            this.assetReference = assetReference;
        }

        public IEnumerator Initialize(object applicationData) {
            if(platform != null) {
                yield return platform.Initialize(applicationData);
            }
            
            var handle = Addressables.LoadAssetAsync<SteamVRPlatformSettings>(assetReference);
            yield return new WaitUntil(() => handle.IsDone);
            steamVRPlatformSettings = handle.Result;
            Debug.Log($"Device Platform {steamVRPlatformSettings.devicePlatform} initialized");
        }

        public IApplicationLifecycle InputHandler() {
            var inputHandler = new SteamVRInput(steamVRPlatformSettings.inputSettings);
            inputHandler.Initialize();
            return inputHandler;
        }
        public void Tick() {
        }
        public void Dispose() { }

        public void OnApplicationQuit() { }
    }
}