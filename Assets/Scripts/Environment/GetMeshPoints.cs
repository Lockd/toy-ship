using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GetMeshPoints : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> groundPoints;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verticies = mesh.vertices;

        groundPoints = new List<Vector3>();

        for (var i = 0; i < verticies.Length; i++)
        {
            Debug.Log(verticies[i]);
            if (verticies[i].y > -0.1f)
            {
                groundPoints.Add(verticies[i]);
            }
        }

        Debug.Log(groundPoints.Count);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 planePoint in groundPoints)
        {
            Gizmos.DrawSphere(planePoint, .05f);
        }
    }
}
