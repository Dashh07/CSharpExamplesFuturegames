using UnityEngine;

namespace Serialization.Save {

    public class GameController : MonoBehaviour {
        private PlayerData currentData;

        private void Start() {
            // Example: load data at start
            PlayerData playerData = new() {
                playerName = "Hero",
                level = 1,
                health = 100f
            };

            currentData = SaveSystem.Load() ?? playerData;
        }

        private void Update() {
            // Example: save when pressing S
            if(Input.GetKeyDown(KeyCode.S)) {
                Save();
            }
        }
        public void Save() {
            currentData.level++;
            SaveSystem.Save(currentData);
        }
    }
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(GameController))]
    public class GameControllerEditor : UnityEditor.Editor {
        private GameController gameController;
        private void OnEnable() {
            gameController = (GameController)target;
        }
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            UnityEditor.EditorGUILayout.HelpBox("Save data to disk when pressing S or press this button.", UnityEditor.MessageType.Info);
            GUILayout.Space(10);
            if(GUILayout.Button("Save Data")) {
                gameController.Save();
            }
        }
    }
#endif
}
