using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolSystem {
    private readonly PoolSettings poolSettings;
    public PoolSystem(PoolSettings poolSettings) {
        this.poolSettings = poolSettings;

    }
    private List<TileReference> tilePrefabs;
    private readonly List<GameObject> pool = new List<GameObject>();
    private readonly List<string> tileTypeId = new List<string>();

    public TileSetGameData TileSetGameData { private get; set; }

    public void Initialize() {
        if(TileSetGameData == null) {
            return;
        }
        tilePrefabs = TileSetGameData.GameTiles;
        InitializePool();
    }

    public GameObject GetPoolItem(string typeID, Transform parent, float scale, bool asActive = true) {
        var obj = GetPoolTile(typeID);
        obj.transform.SetParent(parent);
        obj.transform.localScale *= scale;
        obj.SetActive(asActive);
        return obj;
    }

    /// <summary>
    /// Returns the tile to the pool
    /// </summary>
    /// <param name="tileObj"></param>
    public void ReturnItem(GameObject tileObj) {
        tileObj.SetActive(false);
        tileObj.transform.localPosition = Vector3.zero;
        tileObj.transform.localScale = GetScale(tileObj.GetComponent<TileReference>());

        var tile = tileObj.GetComponent<TileReference>();
        if(tile != null) {
            tile.GridCell = new Cell(0, 0);
            tile.IsMoving = false;
        }
    }

    private void InitializePool() {
        for(int i = 0; i < tilePrefabs.Count; i++) {
            for(int j = 0; j < poolSettings.minAmountEach; j++) {
                AddObjectToPool(tilePrefabs[i].gameObject);
            }
        }
    }

    private GameObject GetPoolTile(string typeID) {
        GameObject tileObject = null;
        for(int i = 0; i < tileTypeId.Count; i++) {
            if(typeID == tileTypeId[i]) {
                if(!pool[i].activeSelf) {
                    return pool[i];
                }
                tileObject = pool[i];
            }
        }
        return AddObjectToPool(tileObject);
    }

    private GameObject AddObjectToPool(GameObject tileObject) {
        var obj = GameObject.Instantiate(tileObject);
        obj.SetActive(false);
        var tile = obj.GetComponent<TileReference>();
        if(tile != null) {
            tile.transform.localScale = GetScale(tile);
            pool.Add(obj);
            tileTypeId.Add(tile.typeID);
        }
        return obj;
    }

    private Vector3 GetScale(TileReference tileReference) {
        for(int i = 0; i < TileSetGameData.GameTiles.Count; i++) {
            if(tileReference.typeID == TileSetGameData.GameTiles[i].typeID) {
                return TileSetGameData.GameTiles[i].transform.localScale;
            }
        }
        return Vector3.one;
    }

    public void Dispose() {
        for(int i = 0; i < pool.Count; i++) {
            Object.Destroy(pool[i]);
        }
        pool.Clear();
    }
}