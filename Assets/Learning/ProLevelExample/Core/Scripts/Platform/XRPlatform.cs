using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Template {
    public class XRPlatform : IPlatform {
        private readonly IPlatform platform;
        private XRPlatformSettings xrPlatformSettings;
        private readonly AssetReference assetReference;

        public XRPlatform(AssetReference assetReference) {
            this.assetReference = assetReference;
        }

        public IEnumerator Initialize(object applicationData) {
            if(platform != null) {
                yield return platform.Initialize(applicationData);
            }
            
            var handle = Addressables.LoadAssetAsync<XRPlatformSettings>(assetReference);
            yield return new WaitUntil(() => handle.IsDone);
            xrPlatformSettings = handle.Result;
            Debug.Log($"Device Platform {xrPlatformSettings.devicePlatform} initialized");
        }

        public IApplicationLifecycle InputHandler() {
            var inputHandler = new XRInput(xrPlatformSettings.inputSettings);
            inputHandler.Initialize();
            return inputHandler;
        }
        public void Tick() { }
        public void Dispose() { }

        public void OnApplicationQuit() { }
    }
}