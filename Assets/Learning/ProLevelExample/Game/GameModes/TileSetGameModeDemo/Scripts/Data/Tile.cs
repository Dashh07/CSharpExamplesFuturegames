using System.Collections.Generic;

public class TilesData {
    public List<TileReference> tileReference = new();
}

public class TilesHandler {
    private readonly TilesData tilesData;

    public TilesHandler(TilesData tilesData) {
        this.tilesData = tilesData;

    }

    public void LateTick() {
        for(int i = 0; i < tilesData.tileReference.Count; i++) {
            TileReference tile = tilesData.tileReference[i];
            tile.LateTick();
        }
    }
    public void Dispose() {
    }
}