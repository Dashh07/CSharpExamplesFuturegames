using System;

public interface IItem { }

[Serializable]
public class ItemBaseStats {
    public string name = "default";
    public string type = "default";
    public int value;
    public int weight;
}
