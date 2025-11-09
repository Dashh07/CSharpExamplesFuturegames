using System.Collections.Generic;
using UnityEngine;

public interface IWeapon {
    public string ID { get; set; }
    public List<IAttachment> Attachments { get; }
    public void AssembleWeapon(string id);

    public void TickWeapon() { }
}

public class Sniper : IWeapon {
    public string ID { get; set; }
    public List<IAttachment> Attachments { get; } = new();
    public void AssembleWeapon(string id) {
        ID = id;
        Debug.Log($"{id} Assembled");
    }
}

public class SMG : IWeapon {
    public string ID { get; set; }
    public List<IAttachment> Attachments { get; } = new();
    public void AssembleWeapon(string id) {
        ID = id;
        Debug.Log($"{id} Assembled");
    }
}
