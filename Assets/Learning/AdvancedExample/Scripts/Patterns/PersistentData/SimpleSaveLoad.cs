using UnityEngine;

public class SimpleSaveLoad : Singleton<SimpleSaveLoad> {
    private SimpleSaveLoad() { }
    private CurrentSavedData currentSavedData = new CurrentSavedData();

    public CurrentSavedData GetCurrentSavedData => currentSavedData;

    public void SaveData(string fileName) {
        Debug.Log("Saving data...");
        DiskUtility.Save(currentSavedData, fileName);
        //DiskUtility.SaveBinary(currentSavedData, fileName);
    }

    public CurrentSavedData LoadData(string fileName) {
        Debug.Log("Loading data...");
        currentSavedData = DiskUtility.Load<CurrentSavedData>(fileName);
        if (currentSavedData == null) {
            currentSavedData = new CurrentSavedData();
        }
        //currentSavedData = DiskUtility.LoadBinary<CurrentSavedData>(fileName);
        return currentSavedData;
    }
}
