using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverMeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void GenerateMesh(Vector3[] leftDots, Vector3[] rightDots)
    {
        // merging two arrays (I miss js)
        vertices = new Vector3[leftDots.Length * 2];
        leftDots.CopyTo(vertices, 0);
        rightDots.CopyTo(vertices, leftDots.Length);
        
        triangles = new int[(leftDots.Length - 1) * 6];

        for (int idx = 0, dotsIdx = 0; idx < triangles.Length; idx += 6)
        {
            triangles[idx] = leftDots.Length + dotsIdx;
            triangles[idx + 1] = dotsIdx;
            triangles[idx + 2] = dotsIdx + 1;
            triangles[idx + 3] = leftDots.Length + dotsIdx;
            triangles[idx + 4] = dotsIdx + 1;
            triangles[idx + 5] = leftDots.Length + dotsIdx + 1;
            dotsIdx++;
        }

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
