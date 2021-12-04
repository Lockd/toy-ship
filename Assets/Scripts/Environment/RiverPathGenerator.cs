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
    public int lengthOfRiverSegment = 20;

    Vector3[] sideDots = null;

    void Start()
    {
        RegenerateRiver();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            RegenerateRiver();
        }
    }

    // VertexPath GeneratePath(Vector2[] points, bool closedPath)
    BezierPath GeneratePath(Vector2[] points, bool closedPath)
    {
        BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xz);
        // return new VertexPath(bezierPath, transform);
        return bezierPath;
    }

    void RegenerateRiver()
    {
        Vector2[] points = GenerateRiverPoints();
        BezierPath path = GeneratePath(points, false);
        pathCreator.bezierPath = path;

        sideDots = new Vector3[(int)(pathCreator.path.length / deltaForRiverSegments * 2 + 2)];
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

    Vector2[] GenerateRiverPoints()
    {
        Vector2[] points = new Vector2[lengthOfRiverSegment];
        points[0] = new Vector2(0f, 0f);
        for (int i = 1; i < lengthOfRiverSegment; i++)
        {
            points[i] = new Vector2(Random.Range(-3f, 3f), Random.Range(2f, 5f)) + points[i - 1];
        }
        return points;
    }

    void OnDrawGizmos()
    {
        if (sideDots != null)
        {
            foreach (Vector3 vector in sideDots)
            {
                Gizmos.DrawSphere(vector, .05f);
            }
        }
    }
}
