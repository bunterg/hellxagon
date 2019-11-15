using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{
    public Tilemap PlayerLayer;
    public Tilemap HighlightLayer;
    public Tilemap UntargetableLayer;

    public Tile SelectedTileTemplate;
    public Tile AreaTileTemplate;

    private TileUnit selectedTile;
    private Vector3Int selectedTilePosition;

    private static Vector3Int[] neighbors =
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Vector3Int currentCell = PlayerLayer.WorldToCell(mousePos);

            if (PlayerLayer.HasTile(currentCell))
            {
                // highlight player
                HighlightLayer.ClearAllTiles();
                HighlightLayer.SetTile(currentCell, SelectedTileTemplate);
                selectedTilePosition = currentCell;
                selectedTile = PlayerLayer.GetTile(currentCell) as TileUnit;

                // get surroundings positions
                Vector3Int[] highlightPositions = selectedTile.Data.getMoveTilePositions(currentCell).Where(pos => !UntargetableLayer.HasTile(pos)).ToArray();

                var dict = new Dictionary<Vector3Int, int>()
                {
                    { currentCell, 0 }
                };
                int clusterCount = 1;
                int i = 0, opCount = 0;
                while(i < highlightPositions.Length)
                {
                    Vector3Int position = highlightPositions[i];
                    int clusterId = clusterCount;
                    if (!dict.ContainsKey(position))
                    {
                        clusterCount++;
                        dict.Add(position, clusterId);
                    }

                    // find smallest adjacent cluster id
                    clusterId = dict[position];
                    foreach (Vector3Int neighbor in neighbors)
                    {
                        var neighborKey = position + neighbor;
                        if (dict.ContainsKey(neighborKey))
                        {
                            opCount++;

                            int neighborClusterId = dict[neighborKey];
                            clusterId = Mathf.Min(clusterId, neighborClusterId);
                        }
                    }
                    // join adjacents cluster to cluster with smallest id
                    foreach (Vector3Int neighbor in neighbors)
                    {
                        var neighborKey = position + neighbor;
                        if (dict.ContainsKey(neighborKey))
                        {
                            opCount++;

                            int neighborClusterId = dict[neighborKey];
                            if (neighborClusterId > clusterId)
                                i = 0;

                            //Debug.Log($"i: {i} opCode: {opCount} key: {position}, nei: {neighborKey} clusterId: {clusterId}");
                            dict[neighborKey] = clusterId;
                        }
                    }
                    dict[position] = clusterId;
                    i++;
                }

                //foreach (var kvp in dict)
                //{
                //    Debug.Log(kvp.Key + " : " + kvp.Value);
                //}
                highlightPositions = dict.Where(kvp => kvp.Value == 0 && kvp.Key != currentCell).Select(kvp => kvp.Key).ToArray();
                Debug.Log($"from {currentCell} operations: {opCount} tiles: {highlightPositions.Length}");

                // highlight surroundings
                foreach (Vector3Int position in highlightPositions)
                {
                    HighlightLayer.SetTile(position, AreaTileTemplate);
                }
            }
            else if (HighlightLayer.HasTile(currentCell) && !PlayerLayer.HasTile(currentCell) && selectedTile != null)
            {
                HighlightLayer.ClearAllTiles();
                PlayerLayer.SetTile(selectedTilePosition, null);
                PlayerLayer.SetTile(currentCell, selectedTile);
            }
        }
    }
}
