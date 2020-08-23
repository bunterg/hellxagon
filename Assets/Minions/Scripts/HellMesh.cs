using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HellMesh : MonoBehaviour
{
    Mesh hellMesh;
    MeshCollider meshCollider;
    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = hellMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        hellMesh.name = "Hell Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();

    }

    public void Triangulate(Cell[] cells)
    {
        hellMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }
        hellMesh.vertices = vertices.ToArray();
        hellMesh.triangles = triangles.ToArray();
        hellMesh.RecalculateNormals();
    }

    void Triangulate(Cell cell)
    {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i<6; i++)
        {
            AddTriangle(center, center + Metrics.corners[i], center + Metrics.corners[i + 1]);
        }
        meshCollider.sharedMesh = hellMesh;
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertIndex);
        triangles.Add(vertIndex + 1);
        triangles.Add(vertIndex + 2);
    }

}
