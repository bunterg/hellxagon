using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Stage
{
    private class Layer {
        public int sortingOrder { get; set; }
        public string name { get; set; }

        public Layer(int sortingOrder, string name)
        {
            this.sortingOrder = sortingOrder;
            this.name = name;
        }
    }

    [MenuItem("Game Dev/Create Map")]
    static void CreatePrefab()
    {
        Layer[] layers = new Layer[4]{
            new Layer(0, "LowerLayer"),
            new Layer(1, "UpperLayer"),
            new Layer(2, "SpawnLayer"),
            new Layer(3, "HighlightLayer")
        };
        GameObject map = new GameObject();
        Grid mapGrid = map.AddComponent<Grid>();
        mapGrid.cellLayout = GridLayout.CellLayout.Hexagon;
        mapGrid.cellSize = new Vector3(1.2f, 1.4f);

        //map.
        GameObject[] objectArray = layers.Select(x =>
        {
            GameObject layer = new GameObject();
            layer.name = x.name;
            Tilemap tilemap = layer.AddComponent<Tilemap>();
            tilemap.tileAnchor = Vector3.zero;
            layer.AddComponent<TilemapRenderer>();
            layer.GetComponent<TilemapRenderer>().sortingOrder = x.sortingOrder;
            layer.transform.parent = map.transform;
            layer.transform.localPosition = Vector3.zero;
            return layer;
        }).ToArray();


        // Set the path as within the Assets folder,
        // and name it as the GameObject's name with the .Prefab format
        string localPath = "Assets/Prefabs/Maps/" + map.name + ".prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAssetAndConnect(map, localPath, InteractionMode.UserAction);

        Object.DestroyImmediate(map);
    }

}