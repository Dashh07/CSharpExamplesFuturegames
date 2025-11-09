using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public static class EditorToolsMenu {
    [MenuItem("ExampleTools/Load Scenes")]
    private static void LoadScenes() {
        var scenesToLoadInOrder = new List<string>();
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            scenesToLoadInOrder.Add(sceneName);
        }
        string path = "Assets/Scenes";
        foreach(string scene in scenesToLoadInOrder) {
            EditorSceneManager.OpenScene($"{path}/{scene}.unity", OpenSceneMode.Additive);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenesToLoadInOrder[2]));
    }
}
