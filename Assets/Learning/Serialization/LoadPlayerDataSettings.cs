using UnityEditor;
using UnityEngine;

// sometimes you want to load a scriptable objects by hand when unity starts (not in the builds, only unity editor), for example. Since
// scriptable objects are not serialized, you need to load them manually.
// this is a simple example of how to do that. You should never load scriptable objects during [InitialzeOnLoad] attribute or [RuntimeInitializeOnLoadMethod]
// because you are bypassing the normal unity serialization process.
public class LoadPlayerDataSettings : AssetPostprocessor {
    // this is automatically called when a new asset is imported
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload) {
        Debug.Log("OnPostprocessAllAssets called regardless of user input");
        AssetDatabase.LoadAssetAtPath<PlayerData>("Assets/Learning/Serialization/PlayerData.asset");
    }
}
