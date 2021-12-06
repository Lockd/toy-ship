using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverMeshGenerator : MonoBehaviour
{
    public float borderHeight = .5f;
    public float xBorderOffset = .2f;
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] upperLeftDots;
    Vector3[] upperRightDots;
    int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void GenerateMesh(Vector3[] leftDots, Vector3[] rightDots)
    {
        upperLeftDots = new Vector3[leftDots.Length];
        upperRightDots = new Vector3[rightDots.Length];
        for (int i = 0; i < upperLeftDots.Length; i++)
        {
            upperLeftDots[i] = leftDots[i] + new Vector3(-xBorderOffset, borderHeight, 0);
            upperRightDots[i] = rightDots[i] + new Vector3(xBorderOffset, borderHeight, 0);
        }
        // merging two arrays (I miss js)
        vertices = new Vector3[leftDots.Length * 4];
        upperLeftDots.CopyTo(vertices, 0);
        leftDots.CopyTo(vertices, leftDots.Length);
        rightDots.CopyTo(vertices, leftDots.Length * 2);
        upperRightDots.CopyTo(vertices, leftDots.Length * 3);

        triangles = new int[(leftDots.Length - 1) * 6 * 3];

        int vert = 0;
        int triangleIdx = 0;
        for (int z = 0; z < 3; z++)
        {
            for (int x = 0; x < leftDots.Length - 1; x++)
            {
                triangles[triangleIdx] = vert + leftDots.Length;
                triangles[triangleIdx + 1] = vert;
                triangles[triangleIdx + 2] = vert + 1;
                triangles[triangleIdx + 3] = vert + leftDots.Length;
                triangles[triangleIdx + 4] = vert + 1;
                triangles[triangleIdx + 5] = vert + leftDots.Length + 1;

                vert++;
                triangleIdx += 6;
            }
            vert++;
        }

        UpdateMesh();
    }

    void UpdateMesh()
    {
        if (mesh != null)
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }
}
