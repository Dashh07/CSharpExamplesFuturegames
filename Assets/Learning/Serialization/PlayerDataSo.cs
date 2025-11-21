using System;
using UnityEditor;
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
            GUILayout.Space(10);
            if(GUILayout.Button("Save SO Asset")) {
                UnityEditor.AssetDatabase.SaveAssets(); //this saves the ScriptableObject asset
            }
            GUILayout.Space(20);
            if(GUILayout.Button("Save Data")) {
                SaveSystem.Save(playerDataSo.playerData); // this saves the PlayerData in a file
            }
            GUILayout.Space(10);
            if(GUILayout.Button("Load Data")) {
                playerDataSo.playerData = SaveSystem.Load();
            }
            if(GUILayout.Button("Load")) {
                try {
                    playerDataSo.playerData = SaveSystem.Load<PlayerData>();
                }
                catch(Exception e) {
                    Debug.Log(e);
                    playerDataSo.playerData = new PlayerData();
                }

                EnemyData enemyData = SaveSystem.Load<EnemyData>();
            }
            if(GUILayout.Button("Save SO asset only")) {
                AssetDatabase.SaveAssets();
            }
        }
#endif
    }
}
