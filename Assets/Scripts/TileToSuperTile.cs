using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TileToSuperTile
{

    [MenuItem("Assets/Game Dev/Transform Tile")]
    static void CreatePrefab()
    {
        SuperTile superTile = new SuperTile();
        Texture2D texture = (Texture2D)Selection.activeObject;
        superTile.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        string path = "Assets/Resources/Sprites/tool/" + Selection.activeObject.name + ".asset";
        path = AssetDatabase.GenerateUniqueAssetPath(path);
        AssetDatabase.CreateAsset(superTile, path);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = superTile;
    }

    [MenuItem("Assets/Game Dev/Transform Tile", true)]
    static bool ValidateLogSelectedTransformName()
    {
        // Return false if no transform is selected.
        return Selection.activeObject != null && Selection.activeObject is Texture2D;
    }

}