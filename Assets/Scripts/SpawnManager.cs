using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    public Tilemap Tilemap;
    public Unit[] Units;
    public Tile[] SpawnTiles;

    // Start is called before the first frame update
    void Start()
    {
        if (Units.Length != SpawnTiles.Length)
            Debug.LogError("Tiles wrong size");

        for (int i = 0; i < SpawnTiles.Length; i++)
        {
            Tilemap.SwapTile(SpawnTiles[i], new TileUnit(Units[i]));
        }
    }
}
