using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public int xSize = 20;
    public int zSize = 20;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") == true) {
            Debug.Log("key is pressed");
            mesh.Clear();
            vertices = null;
            triangles = null;
            CreateShape();
            UpdateMesh();
        }
    }

    void CreateShape()
    {
        vertices = new Vector3[((xSize + 1) * (zSize + 1))];

        for (int verticeIdx = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * .5f;
                vertices[verticeIdx] = new Vector3(x * .3f, y, z * .3f);
                verticeIdx++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
