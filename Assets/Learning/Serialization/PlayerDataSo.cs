using UnityEngine;

namespace Serialization.Save {
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Serialization/PlayerData")]
    public class PlayerDataSo : ScriptableObject {
        public PlayerData playerData;
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PlayerDataSo))]
    public class PlayerDataSoCustomEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            PlayerDataSo playerDataSo = (PlayerDataSo)target;
            if(GUILayout.Button("Save")) {
                SaveSystem.Save(playerDataSo.playerData);
            }
        }
    }
#endif
}
