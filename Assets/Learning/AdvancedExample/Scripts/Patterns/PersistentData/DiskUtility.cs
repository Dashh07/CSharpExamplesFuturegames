using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class DiskUtility {
    private static readonly string path = Application.dataPath + "/SavedData";

    /// <summary>
    /// Save data to disk in a JSON format
    /// </summary>
    /// <param name="data"></param>
    /// <param name="filename"></param>
    public static void Save<T>(T data, string filename) {
        string json = JsonUtility.ToJson(data);
        if(!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText($"{path}/{filename}.sav", json);
    }

    /// <summary>
    /// Load data from disk in a JSON format
    /// </summary>
    /// <param name="filename"></param>
    public static T Load<T>(string filename) {
        if(!File.Exists($"{path}/{filename}.sav")) {
            Save(default(T), filename);
            return default(T);
        }
        string json;
        try {
            json = File.ReadAllText($"{path}/{filename}.sav");
            return JsonUtility.FromJson<T>(json);
        }
        catch(System.Exception e) {
            Debug.LogError(e.Message);
            return default(T);
        }
    }

    /// <summary>
    /// Save data to disk in a binary format
    /// </summary>
    /// <param name="data"></param>
    /// <param name="filename"></param>
    public static void SaveBinary<T>(T data, string filename) {
        BinaryFormatter formatter = new();
        if(!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        FileStream stream = new($"{path}/{filename}.bin", FileMode.OpenOrCreate);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Load data from disk in a binary format
    /// </summary>
    /// <param name="filename"></param>
    public static T LoadBinary<T>(string filename) {
        BinaryFormatter formatter = new();
        if(!File.Exists($"{path}/{filename}.bin")) {
            SaveBinary(default(T), filename);
            return default(T);
        }
        FileStream stream = new($"{path}/{filename}.bin", FileMode.Open);
        T data = (T)formatter.Deserialize(stream);
        stream.Close();
        return data;
    }
}
