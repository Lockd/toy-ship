using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RiverPathGenerator : MonoBehaviour
{
    public PathCreator pathCreator;
    public float minDeltaX = .5f;
    // public float minDeltaY;
    public float minDeltaZ = .5f;
    public float deltaForRiverSegments = 1f;
    public float minDistanceFromRiverCurve = 1;

    Vector3[] sideDots = null;

    // VertexPath GeneratePath(Vector2[] points, bool closedPath)
    BezierPath GeneratePath(Vector2[] points, bool closedPath)
    {
        BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xz);
        // return new VertexPath(bezierPath, transform);
        return bezierPath;
    }

    void Start()
    {
        Vector2[] points = new Vector2[3];
        points[0] = new Vector2(0f, 0f);
        points[1] = new Vector2(0f, 1f);
        points[2] = new Vector2 (7f, 6f);
        BezierPath path = GeneratePath(points, false);
        pathCreator.bezierPath = path;

        sideDots = new Vector3[(int)(pathCreator.path.length / deltaForRiverSegments * 2)];
        int sideDotsIdx = 0;
        for (float i = 0; i <= pathCreator.path.length; i += deltaForRiverSegments)
        {
            Vector3 direction = pathCreator.path.GetDirectionAtDistance(i);
            Vector3 pos = pathCreator.path.GetPointAtDistance(i);
            Vector3 rotatedLine = (Quaternion.AngleAxis(90, transform.up) * direction).normalized;
            sideDots[sideDotsIdx] = pos + rotatedLine;
            sideDotsIdx++;
            Vector3 rotatedLineNegative = (Quaternion.AngleAxis(-90, transform.up) * direction).normalized;
            sideDots[sideDotsIdx] = pos + rotatedLineNegative;
            sideDotsIdx++;
        }
    }

    void OnDrawGizmos()
    {
        if (sideDots != null)
        {
            foreach (Vector3 vector in sideDots)
            {
                Gizmos.DrawSphere(vector, .01f);
            }
        }
    }
}
