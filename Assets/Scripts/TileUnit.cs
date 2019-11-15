using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUnit : Tile
{
    public Unit Data;

    public TileUnit(Unit data)
    {
        Data = data;
        this.sprite = Data.tile.sprite;
    }
}
