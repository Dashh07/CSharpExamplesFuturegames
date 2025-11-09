using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Examples/Inventory")]
public class Inventory : ScriptableObject {
    public List<Item> equipped = new (12);
    public List<Item> inventory = new (20);
}
