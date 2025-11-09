using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Examples/Item")]
public class Item : ScriptableObject, IItem {
    public ItemBaseStats baseStats = new();
}
