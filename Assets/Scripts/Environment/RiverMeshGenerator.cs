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
    public int xSize = 40;
    public int zSize = 40;
    Mesh mesh;
    List<Vector3> vertices;
    List<Vector3> upperLeftDots;
    List<Vector3> upperRightDots;
    List<Vector3> terrainLeftDots;
    List<Vector3> terrainRightDots;
    int[] triangles;
    MeshCollider meshCollider;
    int iterationsCounter = 0;
    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void GenerateMesh(Vector3[] leftDots, Vector3[] rightDots, bool isInitial, int indexIncrement)
    {
        upperLeftDots = new List<Vector3>();
        upperRightDots = new List<Vector3>();
        terrainLeftDots = new List<Vector3>();
        terrainRightDots = new List<Vector3>();

        for (int i = 0; i < leftDots.Length; i++)
        {
            upperLeftDots.Add(leftDots[i] + new Vector3(-xBorderOffset, borderHeight, 0));
            upperRightDots.Add(rightDots[i] + new Vector3(xBorderOffset, borderHeight, 0));
        }

        // dots for left terrain
        for (int x = terrainWidth; x >= 0; x--)
        {
            for (int z = 0; z < upperLeftDots.Count; z++)
            {
                Vector3 point = upperLeftDots[z];
                float xCoordinate = point.x - (x + 1) * xDistanceBetweenTerrainDots;
                float yCoordinate = point.y + Mathf.PerlinNoise(x * .3f, (z + iterationsCounter) * .3f) * noiseHeightModifier;
                float zCoordinate = point.z;

                terrainLeftDots.Add(new Vector3(xCoordinate, yCoordinate, zCoordinate));
            }
        }
        // dots for right terrain
        for (int x = 0; x <= terrainWidth; x++)
        {
            for (int z = 0; z < upperRightDots.Count; z++)
            {
                float xCoordinate = upperRightDots[z].x + (x + 1) * xDistanceBetweenTerrainDots;
                float yCoordinate = upperRightDots[z].y + Mathf.PerlinNoise(x * .3f, (z + iterationsCounter) * .3f) * noiseHeightModifier;
                float zCoordinate = upperRightDots[z].z;

                terrainRightDots.Add(new Vector3(xCoordinate, yCoordinate, zCoordinate));
            }
        }

        iterationsCounter += indexIncrement;

        vertices = new List<Vector3>();
        vertices.AddRange(terrainLeftDots);
        vertices.AddRange(upperLeftDots);
        vertices.AddRange(leftDots);
        vertices.AddRange(rightDots);
        vertices.AddRange(upperRightDots);
        vertices.AddRange(terrainRightDots);

        triangles = new int[(leftDots.Length - 1) * (5 + terrainWidth * 2) * 6];

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
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            meshCollider.sharedMesh = mesh;
        }
    }

    // use this for debugging purposes
    void OnDrawGizmos()
    {
        if (vertices != null)
        {
            foreach (Vector3 vector in vertices)
            {
                Gizmos.DrawSphere(vector, .05f);
            }
        }
    }
}
