using System.IO;
using UnityEngine;

namespace Serialization.Save {
    public static class SaveSystem {
        private static string SavePath => Application.dataPath + "/save.json";

        public static void Save(PlayerData data) {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
            Debug.Log($"Game saved to {SavePath}");
        }

        public static PlayerData Load() {
            if(!File.Exists(SavePath)) {
                Debug.LogWarning("No save file found!");
                return null;
            }

            string json = File.ReadAllText(SavePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Game loaded!");
            return data;
        }
    }
}
