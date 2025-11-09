using Project;
using UnityEngine;

public class TileClashGameMode : IGameMode {
    private readonly ApplicationData applicationData;
    private PoolSettings poolSettings;
    private TileSetGameData tileSetGameData;

    private GamePlay gamePlay;
    private TileSetMenuView tileSetMenuView;
    private TilesHandler tilesHandler;
    private GameplayData gameplayData;
    private PoolSystem poolSystem;
    public TileClashGameMode(ApplicationData applicationData) {
        this.applicationData = applicationData;
    }

    public bool IsGameModeInitialized { get; private set; }

    public void EnterGameMode() {
        var tileClashReference = Object.FindFirstObjectByType<TileClashReference>();
        tileSetGameData = tileClashReference.tileSetGameData;
        poolSettings = tileClashReference.poolSettings;

        poolSystem = new(poolSettings) {
            TileSetGameData = tileSetGameData
        };
        poolSystem.Initialize();

        gameplayData = new GameplayData();
        var tilesData = new TilesData();
        var levelGridHandler = new LevelGridSetup(tileClashReference, poolSystem, tilesData, gameplayData);

        gamePlay = new GamePlay(tileClashReference, poolSystem, tileSetGameData, gameplayData);
        gamePlay.Initialize();

        tilesHandler = new TilesHandler(tilesData);

        tileSetMenuView = new TileSetMenuView(tileClashReference.uiReference, tileClashReference.sessionData, gameplayData) {
            TileSetGameData = tileSetGameData
        };
        tileSetMenuView.Initialize();
        tileSetMenuView.ToggleMainMenu(true);

        IsGameModeInitialized = true;
    }
    public void Tick() {
        gamePlay.Tick();
        tileSetMenuView.Tick();
        if(gameplayData.shouldQuit) {
            applicationData.ChangeApplicationState(ApplicationState.MainMenu);
        }
    }
    public void LateTick() {
        tilesHandler.LateTick();
    }

    public void Dispose() {
        gamePlay.Dispose();
        tileSetMenuView.Dispose();
        tilesHandler.Dispose();
        poolSystem.Dispose();
    }

    public void ExitGameMode() {
        IsGameModeInitialized = false;
    }
}