using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteAlways]
public class PaintTerrain : MonoBehaviour
{
    Mesh mesh;
    public TerrainPreset terrainPreset;
    Color[] colors;
    float minTerrainHeight;
    float maxTerrainHeight;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        Vector3[] verticies = mesh.vertices;

        colors = new Color[mesh.vertices.Length];

        for (var i = 0; i < verticies.Length; i++)
        {
            float y = verticies[i].y;

            if (y > maxTerrainHeight)
                maxTerrainHeight = y;

            if (y < minTerrainHeight)
                minTerrainHeight = y;
        }

        for (var i = 0; i < verticies.Length; i++)
        {
            float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, verticies[i].y);
            colors[i] = terrainPreset.TerrainGradient.Evaluate(height); 
        }

        mesh.colors = colors;
    }
}
