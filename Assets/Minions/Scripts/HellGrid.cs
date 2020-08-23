using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellGrid : MonoBehaviour
{
    public int width = 4;
    public int height = 4;

    public Cell cellPrefab;
    Cell[] cells;
    public Text cellLabel;
    Canvas gridCanvas;
    HellMesh hellMesh;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hellMesh = GetComponentInChildren<HellMesh>();
        cells = new Cell[height * width];
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Create(x, z, i++);
            }
        }
    }

    void Start()
    {
        hellMesh.Triangulate(cells);
    }

    void Create(int x, int z, int i)
    {
        Vector3 pos;
        pos.x = (x + z * 0.5f - z / 2) * (Metrics.ir*2f);
        pos.y = 0f;
        pos.z = z * (Metrics.or * 1.5f);

        Cell cell = cells[i] = Instantiate<Cell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = pos;
        cell.coordinates = Coordinates.FromOffsetCoordinates(x, z);

        Text label = Instantiate<Text>(cellLabel);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(pos.x, pos.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }

    //luego moveremos este codigo a otro lado
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    void TouchCell (Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        Coordinates coordinates = Coordinates.FromPosition(position);
        Debug.Log("touched at " + coordinates.ToString());
    }
}
