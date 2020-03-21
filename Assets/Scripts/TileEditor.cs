using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;

// Tagging a class with the EditorTool attribute and no target type registers a global tool. Global tools are valid for any selection, and are accessible through the top left toolbar in the editor.
[EditorTool("Tile Editor", typeof(GridSelection))]
class TileEditor : EditorTool
{
    // Serialize this value to set a default value in the Inspector.
    [SerializeField]
    Texture2D m_ToolIcon;

    GUIContent m_IconContent;

    void OnEnable()
    {
        m_IconContent = new GUIContent()
        {
            image = m_ToolIcon,
            text = "Tile Editor",
            tooltip = "Tile Editor"
        };
    }

    public override GUIContent toolbarIcon
    {
        get { return m_IconContent; }
    }

    // This is called for each window that your tool is active in. Put the functionality of your tool here.
    public override void OnToolGUI(EditorWindow window)
    {
        EditorGUI.BeginChangeCheck();

        Tilemap layer = GridSelection.target.GetComponent<Tilemap>();
        Tile tile = layer.GetTile(GridSelection.position.position) as Tile;
        Debug.Log(tile);
        Selection.activeObject = tile;
        Vector3 position = Tools.handlePosition;

        using (new Handles.DrawingScope(Color.green))
        {
            position = Handles.Slider(position, Vector3.right);
        }

        if (EditorGUI.EndChangeCheck())
        {
            Vector3 delta = position - Tools.handlePosition;

            Undo.RecordObjects(Selection.transforms, "Move Platform");

            foreach (var transform in Selection.transforms)
                transform.position += delta;
        }
    }
}