using UnityEngine;
using UnityEngine.UIElements;

public class UIToolkitHandler : MonoBehaviour {
    public UIDocument uiDocument;

    public Button ButtonKill { get; private set; }

    private void Awake() {
        ButtonKill = uiDocument.rootVisualElement.Q<Button>("button_kill");
        if (ButtonKill == null) {
            Debug.LogError("ButtonKill not found!");
        }
    }

    private void OnEnable() {
        ButtonKill.clicked += OnButtonKillClicked;
    }

    private void OnButtonKillClicked() {
        var player = FindFirstObjectByType<PlayerReference>();
        player.playerUIReference.healthBar.fillAmount = 0;
        Debug.Log("Player killed!");
    }

    private void OnDisable() {
        ButtonKill.clicked -= OnButtonKillClicked;
    }
}
