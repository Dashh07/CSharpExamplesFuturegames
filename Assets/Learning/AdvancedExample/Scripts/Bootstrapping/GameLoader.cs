using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Project bootstrapper Monobehaviour
/// </summary>
public class GameLoader : MonoBehaviour {
    private List<string> scenesToLoadInOrder = new();
    private static bool isStarting = false;

    // start can also function as a coroutine
    private IEnumerator Start() {
        // we check if another instance of the game loader is already starting the game and cancel the coroutine if so
        if(isStarting) {
            yield break;
        }

        isStarting = true;
        // We load all scenes in the build settings in order
        for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            scenesToLoadInOrder.Add(sceneName);
        }

        foreach(string scene in scenesToLoadInOrder) {
            yield return StartCoroutine(LoadSceneAdditiveIfNotLoaded(scene));
        }

        // We set the last scene specified in the array as the active scene by convention
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenesToLoadInOrder[^1]));

        // initialize the game once all the scenes are loaded
        GameController controller = FindFirstObjectByType<GameController>();
        controller.Initialize();
    }

    private IEnumerator LoadSceneAdditiveIfNotLoaded(string sceneName) {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        if(!scene.isLoaded) {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Debug.Log($"Scene '{sceneName}' was not loaded and has now been loaded additively.");
        }
        else {
            Debug.Log($"Scene '{sceneName}' is already loaded.");
        }
    }
}
