using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "Project/GameMode/Match3/Game Config")]
public class TileSetGameData : ScriptableObject {
    [Header("Main Settings")]
    [Tooltip("Amount of pieces to create a match")]
    [SerializeField]
    private int matchCounter = 3;

    [SerializeField]
    private LevelData[] gameLevels;

    [SerializeField]
    private List<TileReference> gameTiles;

    [Header("Other Settings")]
    [SerializeField]
    private float tileSpeed = 40f;

    [SerializeField]
    private float tileClickDestroyDelay;

    public int MatchCounter => matchCounter;
    public LevelData[] GameLevels => gameLevels;
    public List<TileReference> GameTiles => gameTiles;
    public float TileSpeed => tileSpeed;
    public float TileClickDestroyDelay => tileClickDestroyDelay;

#if UNITY_EDITOR
    void OnValidate() {
        for(int i = 0; i < gameTiles.Count; i++) {
            if(gameTiles[i] == null) {
                continue;
            }
            var tiles = gameTiles.ToList();
            tiles.Remove(gameTiles[i]);
            if(tiles.Count > 0) {
                for(int j = 0; j < tiles.Count; j++) {
                    if(tiles[j] != null && tiles[j].typeID == gameTiles[i].typeID) {
                        Debug.LogError($"Tile is a duplicate!");
                        gameTiles[i] = null;
                    }
                }
            }
        }
    }
#endif
}