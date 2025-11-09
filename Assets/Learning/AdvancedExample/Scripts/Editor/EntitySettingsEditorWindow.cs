using UnityEngine;
using UnityEditor;

public class EntitySettingsEditorWindow : EditorWindow {
    private EntitySettings entitySettings;
    private SerializedObject serializedEntitySettings;
    private Vector2 scrollPosition;

    [MenuItem("ExampleTools/Entity Settings Editor")]
    public static void ShowWindow() {
        var window = GetWindow<EntitySettingsEditorWindow>("Entity Settings Editor");
    }

    private void OnEnable() {
        if(entitySettings == null) {
            CreateNewEntitySettings();
        }
        else {
            serializedEntitySettings = new SerializedObject(entitySettings);
        }
    }

    private void OnGUI() {
        EditorGUILayout.LabelField("Entity Settings Editor", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if(entitySettings == null) {
            EditorGUILayout.HelpBox("No EntitySettings instance selected or created.", MessageType.Info);
            if(GUILayout.Button("Create New Entity Settings")) {
                CreateNewEntitySettings();
            }
            return;
        }

        serializedEntitySettings.Update();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Iterate over all serialized properties and draw them, excluding "m_Script"
        SerializedProperty property = serializedEntitySettings.GetIterator();
        bool enterChildren = true;

        while(property.NextVisible(enterChildren)) {
            if(property.name == "m_Script")
                continue;

            EditorGUILayout.PropertyField(property, true);
            enterChildren = false;
        }

        EditorGUILayout.EndScrollView();
        serializedEntitySettings.ApplyModifiedProperties();

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Create New")) {
            if(EditorUtility.DisplayDialog("Create New Entity Settings", "Unsaved changes will be lost. Continue?", "Yes", "No")) {
                CreateNewEntitySettings();
            }
        }
        if(GUILayout.Button("Load Existing")) {
            LoadExistingEntitySettings();
        }

        if(GUILayout.Button("Save")) {
            SaveEntitySettings();
        }

        EditorGUILayout.EndHorizontal();
    }


    private void CreateNewEntitySettings() {
        entitySettings = CreateInstance<EntitySettings>();
        serializedEntitySettings = new SerializedObject(entitySettings);
    }

    private void LoadExistingEntitySettings() {
        string path = EditorUtility.OpenFilePanel("Select Entity Settings", "Assets", "asset");
        if(!string.IsNullOrEmpty(path)) {
            path = FileUtil.GetProjectRelativePath(path);
            entitySettings = AssetDatabase.LoadAssetAtPath<EntitySettings>(path);
            if(entitySettings != null) {
                serializedEntitySettings = new SerializedObject(entitySettings);
            }
            else {
                EditorUtility.DisplayDialog("Error", "Could not load the selected asset. Make sure it's an EntitySettings asset.", "OK");
            }
        }
    }

    private void SaveEntitySettings() {
        string path = AssetDatabase.GetAssetPath(entitySettings);
        if(string.IsNullOrEmpty(path)) {
            path = EditorUtility.SaveFilePanelInProject("Save Entity Settings", entitySettings.entityName, "asset", "Specify where to save the Entity Settings asset.");
            if(string.IsNullOrEmpty(path)) {
                return; // User canceled save
            }
            AssetDatabase.CreateAsset(entitySettings, path);
        }

        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(entitySettings);
        EditorUtility.DisplayDialog("Success", "Entity Settings saved successfully.", "OK");
    }
}
