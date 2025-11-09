using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CurrentSavedData {
    public CurrentPlayerData currentPlayerData = new();
    public LevelData levelData = new();
}

[Serializable]
public class LevelData {
    public string timeAndDate;
}

[Serializable]
public class CurrentPlayerData {
    public List<float> position = new(){0,0,0};
    public CurrentPlayerSettings entitySettings = new();
}

[Serializable]
public class CurrentPlayerSettings {
    public string name;
    public int health;
    public int mana;
    public int strength;
    public int dexterity;
    public int intelligence;
    public int vitality;
    public int luck;

    public List<IItem> startInventory = new();
    public List<IItem> startEquipment = new();
}
