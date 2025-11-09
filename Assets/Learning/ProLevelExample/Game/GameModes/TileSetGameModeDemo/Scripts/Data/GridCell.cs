using System;
using UnityEngine;

public class GridCell {
    private TileReference occupyingTileReference;
    private readonly Cell cell;

    public Cell Cell => cell;
    public Vector3 CellCenterPos { get; }
    public TileReference TileReference => occupyingTileReference;

    public GridCell(Cell cell, Vector2 cellCenterPosition) {
        this.cell = cell;
        CellCenterPos = cellCenterPosition;
    }

    public bool HasTile() {
        return TileReference != null;
    }

    public void SetTile(TileReference tileReference) {
        occupyingTileReference = tileReference;
        if(tileReference != null) {
            tileReference.GridCell = cell;
        }
    }
}

[Serializable]
public struct Cell {
    public int x;
    public int y;

    public Cell(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public override string ToString() {
        return $"[{x}, {y}]";
    }

    public override bool Equals(object obj) {
        if((obj == null) || GetType() != obj.GetType()) {
            return false;
        }
        var cell = (Cell)obj;
        return cell.x == x && cell.y == y;
    }

    public override int GetHashCode() {
        unchecked {
            return (x * 397) ^ y;
        }
    }
}