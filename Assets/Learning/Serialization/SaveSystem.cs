using System.IO;
using UnityEngine;

namespace Serialization.Save {
    public static class SaveSystem {
        private static string SavePath => Application.dataPath + "/save.json";

        public static void Save<T>(T data) {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
            Debug.Log($"Game saved to {SavePath}");
        }

        public static T Load<T>() {
            if(!File.Exists(SavePath)) {
                Debug.LogWarning("No save file found!");
                throw new FileNotFoundException();
            }

            string json = File.ReadAllText(SavePath);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log("Game loaded!");
            return data;
        }
    }
}
