using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverMeshGenerator : MonoBehaviour
{
    public float borderHeight = .5f;
    public float xBorderOffset = .2f;
    public int terrainWidth = 20;
    public float xDistanceBetweenTerrainDots = .5f;
    public float noiseHeightModifier = 1.5f;
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] upperLeftDots;
    Vector3[] upperRightDots;
    Vector3[] terrainLeftDots;
    Vector3[] terrainRightDots;
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
        terrainLeftDots = new Vector3[leftDots.Length * (terrainWidth + 1)];
        terrainRightDots = new Vector3[rightDots.Length * (terrainWidth + 1)];

        // dots for river borders
        for (int i = 0; i < upperLeftDots.Length; i++)
        {
            upperLeftDots[i] = leftDots[i] + new Vector3(-xBorderOffset, borderHeight, 0);
            upperRightDots[i] = rightDots[i] + new Vector3(xBorderOffset, borderHeight, 0);
        }

        // dots for left terrain
        for (int verticeIdx = 0, x = terrainWidth; x >= 0; x--)
        {
            for (int z = 0; z < upperLeftDots.Length; z++)
            {
                float xCoordinate = upperLeftDots[z].x - (x + 1) * xDistanceBetweenTerrainDots;
                float yCoordinate = upperLeftDots[z].y + Mathf.PerlinNoise(x * .3f, z * .3f) * noiseHeightModifier;
                float zCoordinate = upperLeftDots[z].z;

                terrainLeftDots[verticeIdx] = new Vector3(xCoordinate, yCoordinate, zCoordinate);
                verticeIdx++;
            }
        }
        // dots for right terrain
        for (int verticeIdx = 0, x = 0; x <= terrainWidth; x++)
        {
            for (int z = 0; z < upperRightDots.Length; z++)
            {
                float xCoordinate = upperRightDots[z].x + (x + 1) * xDistanceBetweenTerrainDots;
                float yCoordinate = upperRightDots[z].y + Mathf.PerlinNoise(x * .3f, z * .3f) * noiseHeightModifier;
                float zCoordinate = upperRightDots[z].z;

                terrainRightDots[verticeIdx] = new Vector3(xCoordinate, yCoordinate, zCoordinate);
                verticeIdx++;
            }
        }

        // merging arrays (I miss js)
        vertices = new Vector3[leftDots.Length * (4 + terrainWidth * 2 + 2)];
        terrainLeftDots.CopyTo(vertices, 0);
        upperLeftDots.CopyTo(vertices, terrainLeftDots.Length);
        leftDots.CopyTo(vertices, leftDots.Length + terrainLeftDots.Length);
        rightDots.CopyTo(vertices, leftDots.Length * 2 + terrainLeftDots.Length);
        upperRightDots.CopyTo(vertices, leftDots.Length * 3 + terrainLeftDots.Length);
        terrainRightDots.CopyTo(vertices, leftDots.Length * 4 + terrainLeftDots.Length);
        
        triangles = new int[(leftDots.Length - 1) * (5 + terrainWidth * 2) * 6];
        // triangles = new int[xSize * zSize * 6];
        
        int vert = 0;
        int triangleIdx = 0;
        for (int z = 0; z < (5 + terrainWidth * 2); z++)
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

    void OnDrawGizmos()
    {
        if (terrainLeftDots != null)
        {
            foreach (Vector3 vector in vertices)
            {
                Gizmos.DrawSphere(vector, .05f);
            }
        }
    }
}
