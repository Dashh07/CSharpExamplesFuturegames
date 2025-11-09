using System;
using System.Collections;
using System.Collections.Generic;
using Template;
using UnityEngine;

public class GameplayData {
    public static Action<Cell> tileClickEvent;

    public Action<TileSetGameData, LevelData> shouldSpawnLevel;
    public Action<GridCell[,], PoolSystem> cleanLevel;
    public Action startSessionEvent;
    public Action quitToMainMenuEvent;
    public bool shouldQuit;
}

public class GamePlay : IApplicationLifecycle {
    private readonly TileClashReference tileClashReference;
    private readonly PoolSystem poolSystem;
    private readonly TileSetGameData tileSetGameData;
    private readonly GameplayData gameplayData;
    private LevelData currentLevel;
    private GridCell[,] levelGrid;
    private bool isEvaluating;

    private Cell startCell = new Cell(0, 0);
    private bool shouldMove;
    private readonly List<Cell> movesLeft = new List<Cell>();

    private bool shouldRemove;
    private bool shouldWaitForRemoval;
    private readonly List<Cell> cellsToRemove = new List<Cell>();

    public GamePlay(TileClashReference tileClashReference, PoolSystem poolSystem, TileSetGameData tileSetGameData, GameplayData gameplayData) {
        this.tileClashReference = tileClashReference;
        this.poolSystem = poolSystem;
        this.tileSetGameData = tileSetGameData;
        this.gameplayData = gameplayData;
    }
    public void Initialize() {
        if(tileSetGameData == null) {
            Debug.LogError("Missing the reference for game config");
            return;
        }

        if(tileSetGameData.GameLevels.Length == 0) {
            Debug.LogError("There are not levels configured in the game config", tileSetGameData);
        }

        gameplayData.startSessionEvent += StartGame;
        GameplayData.tileClickEvent += OnClickedTile;
    }

    public void Tick() {
        if(shouldRemove) {
            if(!shouldWaitForRemoval) {
                for(int i = 0; i < cellsToRemove.Count; i++) {
                    var gridCell = levelGrid[cellsToRemove[i].x, cellsToRemove[i].y];
                    var go = gridCell.TileReference.gameObject;
                    tileClashReference.StartCoroutine(RemoveDelayer(go, cellsToRemove[i]));
                    gridCell.SetTile(null);
                }
                shouldWaitForRemoval = true;
            }

            if(cellsToRemove.Count < 1) {
                shouldRemove = false;
                shouldWaitForRemoval = false;
            }
        }

        if(shouldMove) {
            if(movesLeft.Count > 0) {
                MoveTiles(movesLeft);
            }
            else {
                shouldMove = false;
            }
        }
    }

    private void StartGame() {
        Debug.Log("Starting game...");
        gameplayData.cleanLevel.Invoke(levelGrid, poolSystem);
        currentLevel = tileSetGameData.GameLevels[tileClashReference.sessionData.currenLevelIndex];
        levelGrid = currentLevel.GeneratedGrid;
        gameplayData.shouldSpawnLevel.Invoke(tileSetGameData, currentLevel);
    }

    private void OnClickedTile(Cell cell) {
        if(!isEvaluating) {
            isEvaluating = true;
            startCell = cell;
            tileClashReference.StartCoroutine(ExecuteQueue(new List<Cell>() { startCell }));
        }
    }

    IEnumerator ExecuteQueue(List<Cell> tilesToScore) {
        cellsToRemove.AddRange(tilesToScore);
        shouldRemove = true;
        while(shouldRemove) {
            yield return null;
        }

        MoveTiles(tilesToScore);
        while(shouldMove || shouldRemove) {
            yield return new WaitForEndOfFrame();
        }

        EvaluateMove();
    }

    IEnumerator RemoveDelayer(GameObject go, Cell cell) {
        yield return new WaitForSeconds(tileSetGameData.TileClickDestroyDelay);
        cellsToRemove.Remove(cell);
        poolSystem.ReturnItem(go);
    }

    private void EvaluateMove() {
        List<Cell> tilesToScore = new();
        List<Cell> collector = new();
        var startCellY = startCell.y;
        var gridHeight = levelGrid.GetLength(1);
        var gridWidth = levelGrid.GetLength(0);

        for(int j = startCellY; j < gridHeight; j++) {
            for(int i = 1; i < gridWidth; i++) {
                var currGridCell = levelGrid[i - 1, j];
                var adiacGridCell = levelGrid[i, j];

                if(!currGridCell.HasTile()) {
                    if(TestCollector(collector)) {
                        tilesToScore.AddRange(new List<Cell>(collector));
                    }

                    collector.Clear();
                    continue;
                }

                if(!adiacGridCell.HasTile()) {
                    if(TestCollector(collector)) {
                        tilesToScore.AddRange(new List<Cell>(collector));
                    }

                    collector.Clear();
                    continue;
                }

                if(adiacGridCell.TileReference.Equals(currGridCell.TileReference)) {
                    if(!collector.ContainsCell(currGridCell.Cell)) {
                        collector.Add(currGridCell.Cell);
                    }

                    collector.Add(adiacGridCell.Cell);
                }
                else {
                    if(TestCollector(collector)) {
                        tilesToScore.AddRange(new List<Cell>(collector));
                    }

                    collector.Clear();
                }
            }

            if(TestCollector(collector)) {
                tilesToScore.AddRange(new List<Cell>(collector));
            }

            collector.Clear();
        }

        if(tilesToScore.Count > 0) {
            tileClashReference.StartCoroutine(ExecuteQueue(tilesToScore));
        }
        else {
            isEvaluating = false;
            TestBoardEmpty();
        }
    }

    private void MoveTiles(List<Cell> gridCells) {
        gridCells.Reverse();
        var allTilesToMove = new List<Cell>();
        movesLeft.Clear();
        shouldMove = true;
        for(int i = 0; i < gridCells.Count; i++) {
            for(int j = gridCells[i].y + 1; j < levelGrid.GetLength(1); j++) {
                allTilesToMove.Add(new Cell(gridCells[i].x, j));
            }
        }

        for(int i = 0; i < allTilesToMove.Count; i++) {
            var selectedTile = allTilesToMove[i];
            var moveTo = levelGrid[selectedTile.x, selectedTile.y - 1];
            var moveFrom = levelGrid[selectedTile.x, selectedTile.y];

            var tile = moveFrom.TileReference;
            if(tile == null) {
                if(moveFrom.Cell.y + 1 < levelGrid.GetLength(1)) {
                    movesLeft.Add(new Cell(moveFrom.Cell.x, moveFrom.Cell.y + 1));
                }
                continue;
            }
            moveTo.SetTile(tile);
            moveFrom.SetTile(null);
            tile.StartMovement(moveTo.CellCenterPos, tileSetGameData.TileSpeed);
        }
    }

    // Added additional testing options. This would normally be a unit test.
    private bool TestCollector(List<Cell> cells) {
        var max = cells.Count;
        if(max < 3) {
            return false;
        }

#if UNITY_EDITOR
        string message = String.Empty;
        for(int i = 0; i < max; i++) {
            message += $"Collector at cell {levelGrid[cells[i].x, cells[i].y].Cell.ToString()} of type {levelGrid[cells[i].x, cells[i].y].TileReference.typeID}\n";
            if(!levelGrid[cells[0].x, cells[0].y].TileReference.Equals(levelGrid[cells[i].x, cells[i].y].TileReference)) {
                if(max > 3) {
                    Debug.LogError(
                        $"Collector has more than 3 pieces but contains rogue pieces. Ex. {cells[i].ToString()} with tileID {levelGrid[cells[i].x, cells[i].y].TileReference.typeID}");
                }

                Debug.LogError($"Collector failed test on {cells[i].ToString()} with rogue piece {levelGrid[cells[i].x, cells[i].y].TileReference.typeID}");
                return false;
            }

            if(i > 0 && (Mathf.Abs(cells[i].x - cells[i - 1].x) > 1 || cells[i].y != cells[i - 1].y)) {
                Debug.LogError($"Collector failed consecutive tile test on {cells[i].ToString()} with tileID {levelGrid[cells[i].x, cells[i].y].TileReference.typeID}");
                return false;
            }
        }
#endif
        return true;
    }

    private void TestBoardEmpty() {
        for(int i = 0; i < levelGrid.GetLength(0); i++) {
            for(int j = 0; j < levelGrid.GetLength(1); j++) {
                if(levelGrid[i, j].HasTile()) {
                    return;
                }
            }
        }
        gameplayData.quitToMainMenuEvent.Invoke();
    }

    public void Dispose() {
        gameplayData.startSessionEvent -= StartGame;
        GameplayData.tileClickEvent -= OnClickedTile;
    }
}