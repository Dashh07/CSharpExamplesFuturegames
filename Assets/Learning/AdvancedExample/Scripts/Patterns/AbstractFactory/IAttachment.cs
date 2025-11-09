using UnityEngine;

public interface IAttachment {
    public string ID { get; set; }
    public void AssembleAttachment(string id);
}

public class Silencer : IAttachment {
    public string ID { get; set; }
    public void AssembleAttachment(string id) {
        ID = id;
        Debug.Log($"Silencer with id {ID} Assembled");
    }
}


public class Scope : IAttachment {
    public string ID { get; set; }
    public void AssembleAttachment(string id) {
        ID = id;
        Debug.Log($"Scope with id {ID} Assembled");
    }
}
