using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellGrid : MonoBehaviour
{
    public int width = 3;
    public int height = 3;

    public Cell cellPrefab;
    Cell[] cells;

    private void Awake()
    {
        cells = new Cell[height * width];
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Create(x, z, i++);
            }
        }
    }

    void Create(int x, int z, int i)
    {
        Vector3 pos;
        pos.x = x * 10f;
        pos.y = 0f;
        pos.z = z * 10f;

        Cell cell = cells[i] = Instantiate<Cell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = pos;
    }

}
