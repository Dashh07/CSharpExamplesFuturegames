using Template;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TileSetMenuView : IApplicationLifecycle {
    private readonly UIReference uiReference;
    private readonly SessionData sessionData;
    private readonly GameplayData gameplayData;

    private bool isActive;
    private bool isGameSessionActive;
    public TileSetGameData TileSetGameData { private get; set; }

    public TileSetMenuView(UIReference uiReference, SessionData sessionData, GameplayData gameplayData) {
        this.uiReference = uiReference;
        this.sessionData = sessionData;
        this.gameplayData = gameplayData;
    }

    public void Initialize() {
        gameplayData.quitToMainMenuEvent += EndSession;
        uiReference.menuParent.SetActive(true);
        uiReference.levelSelectionMenu.SetActive(false);

        uiReference.startButton.onClick.AddListener(() => uiReference.levelSelectionMenu.SetActive(true));
        uiReference.continueButton.onClick.AddListener(() => uiReference.menuParent.SetActive(false));
        uiReference.backButton.onClick.AddListener(() => gameplayData.shouldQuit = true);

        SetLevelSelectionMenu();
    }

    public void Tick() {
        if(Keyboard.current.escapeKey.wasPressedThisFrame) {
            ToggleMainMenu(!isActive);
        }
    }

    public void ToggleMainMenu(bool value) {
        if(isGameSessionActive) {
            uiReference.levelSelectionMenu.SetActive(false);
            uiReference.menuParent.SetActive(value);
            isActive = value;
        }
    }

    private void SetLevelSelectionMenu() {
        for(int i = 0; i < TileSetGameData.GameLevels.Length; i++) {
            var button = GameObject.Instantiate(uiReference.buttonPrefab, uiReference.levelSelectionMenu.transform).GetComponent<Button>();
            var textField = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var index = i;
            textField.text = $"Test Case {i + 1}";
            button.onClick.AddListener(() => SetLevelAndPlay(index));
        }
    }

    private void SetLevelAndPlay(int levelIndex) {
        sessionData.currenLevelIndex = levelIndex;
        gameplayData.startSessionEvent.Invoke();
        isGameSessionActive = true;
        ToggleMainMenu(false);
    }

    private void EndSession() {
        ToggleMainMenu(true);
        isGameSessionActive = false;
    }

    public void Dispose() {
        gameplayData.quitToMainMenuEvent -= EndSession;
    }
}