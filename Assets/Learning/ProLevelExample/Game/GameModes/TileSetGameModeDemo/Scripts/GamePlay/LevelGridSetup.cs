using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class LevelGridSetup {
    private readonly TileClashReference tileClashReference;
    private readonly PoolSystem poolSystem;
    private readonly TilesData tilesData;
    private readonly GameplayData gameplayData;
    private GridCell[,] currentGameGrid;
    private GameObject background;

    public LevelGridSetup(TileClashReference tileClashReference, PoolSystem poolSystem, TilesData tilesData, GameplayData gameplayData) {
        this.tileClashReference = tileClashReference;
        this.poolSystem = poolSystem;
        this.tilesData = tilesData;
        gameplayData.shouldSpawnLevel += SpawnLevel;
        gameplayData.cleanLevel += CleanLevel;

    }

    private void SpawnLevel(TileSetGameData tileSetGameData, LevelData levelData) {
        tilesData.tileReference = new List<TileReference>();
        currentGameGrid = levelData.GeneratedGrid;

        if(background == null) {
            background = Object.Instantiate(tileClashReference.backgroundPrefab, tileClashReference.boardTransform);
        }
        var pos = background.transform.position;
        background.transform.position = new Vector3(pos.x, pos.y, 2f);
        background.transform.localScale = levelData.BackgroundScale;

        for(int i = 0; i < currentGameGrid.GetLength(1); i++) {
            SpawnTilesLine(i, tileSetGameData, levelData);
        }
    }

    public void CleanLevel(GridCell[,] gameGrid, PoolSystem poolSystem) {
        if(gameGrid == null) {
            gameGrid = new GridCell[0, 0];
        }
        for(int i = 0; i < gameGrid.GetLength(0); i++) {
            for(int j = 0; j < gameGrid.GetLength(1); j++) {
                var gridCell = gameGrid[i, j];
                if(gridCell.HasTile()) {
                    gridCell.TileReference.IsMoving = false;
                    poolSystem.ReturnItem(gridCell.TileReference.gameObject);
                    gameGrid[i, j].SetTile(null);
                }
            }
        }
    }

    private void SpawnTilesLine(int gridLineIndex, TileSetGameData tileSetGameData, LevelData levelData) {
        var gridWidth = currentGameGrid.GetLength(0);
        TileReference[] spawnedTiles = new TileReference[gridWidth];

        for(int i = 0; i < gridWidth; i++) {
            List<string> previousIDs = new List<string>();
            if(i >= tileSetGameData.MatchCounter - 1) {
                for(int j = 1; j < tileSetGameData.MatchCounter; j++) {
                    previousIDs.Add(spawnedTiles[i - j].typeID);
                }
            }
            var currentCell = currentGameGrid[i, gridLineIndex];
            if(!currentCell.HasTile()) {
                spawnedTiles[i] = SpawnTile(tileSetGameData, levelData, currentCell, previousIDs);
                spawnedTiles[i].gameObject.SetActive(true);
            }
            previousIDs.Clear();
        }
    }

    private TileReference SpawnTile(TileSetGameData tileSetGameData, LevelData levelData, GridCell currentCell, List<string> previousIDs = null) {
        var tiles = tileSetGameData.GameTiles;
        var tileToSpawn = GetRandomTile(tiles, previousIDs);
        var spawnedTileObject = poolSystem.GetPoolItem(tileToSpawn.typeID, tileClashReference.boardTransform, levelData.TileScaleMultiplier);
        spawnedTileObject.transform.localPosition = currentCell.CellCenterPos;
        var spawnedTile = spawnedTileObject.GetComponent<TileReference>();
        currentCell.SetTile(spawnedTile);
        tilesData.tileReference.Add(spawnedTile);
        return spawnedTile;
    }

    // It will avoid creating sets of more than two same type consecutive pieces
    private TileReference GetRandomTile(List<TileReference> gameTiles, List<string> previousIDs) {
        var tileIndex = Random.Range(0, gameTiles.Count);
        var tile = gameTiles[tileIndex];

        if(previousIDs != null) {
            int counter = 0;
            for(int i = 0; i < previousIDs.Count - 1; i++) {
                if(previousIDs[i] != null) {
                    if(tile.typeID != previousIDs[i]) {
                        break;
                    }
                    counter++;
                }
            }
            if(counter == previousIDs.Count - 1) {
                tile = GetRandomTile(gameTiles, previousIDs);
            }
        }
        return tile;
    }
}