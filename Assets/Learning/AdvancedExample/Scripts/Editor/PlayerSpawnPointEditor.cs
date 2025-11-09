using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerSpawnPoint))]
public class PlayerSpawnPointEditor : Editor {
    private void OnSceneGUI() {
        PlayerSpawnPoint spawnPoint = (PlayerSpawnPoint)target;
        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(spawnPoint.transform.position, spawnPoint.transform.rotation);
        Quaternion newRotation = Handles.RotationHandle(spawnPoint.transform.rotation, spawnPoint.transform.position);

        if(EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(spawnPoint.transform, "Move or Rotate Spawn Point");
            spawnPoint.transform.position = newPosition;
            spawnPoint.transform.rotation = newRotation;
        }
        Handles.Label(spawnPoint.transform.position + Vector3.up * 2.5f, spawnPoint.gameObject.name, EditorStyles.largeLabel);
    }
}
