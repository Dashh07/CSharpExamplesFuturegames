using Project;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "GameModeSettings", menuName = "Project/Settings/General/GameModeSettings")]
public class GameModeSettings : ScriptableObject {
    public GameModeData[] gameModeData;
}

[Serializable]
public class GameModeData {
    public GameMode gameMode;
    public AssetReference scene;
}