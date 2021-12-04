using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RiverPathGenerator : MonoBehaviour
{
    public PathCreator pathCreator;
    public float minDeltaX = .5f;
    public float maxDeltaX = .5f;
    public float minDeltaZ = .5f;
    public float maxDeltaZ = .5f;
    public float minDistanceFromRiverCurve = 1f;
    public float maxDistanceFromRiverCurve = 3f;
    public float deltaForRiverSegments = 1f;
    public int lengthOfRiverSegment = 20;

    Vector3[] sideDots = null;
    Vector3[] dotsLeft = null;
    Vector3[] dotsRight = null;

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

    BezierPath GeneratePath(Vector2[] points, bool closedPath)
    {
        BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xz);
        return bezierPath;
    }

    void RegenerateRiver()
    {
        Vector2[] points = GenerateRiverPoints();
        BezierPath path = GeneratePath(points, false);
        pathCreator.bezierPath = path;

        sideDots = new Vector3[(int)(pathCreator.path.length / deltaForRiverSegments * 2 + 2)];
        // Those arrays can be used for creating river height
        dotsLeft = new Vector3[sideDots.Length / 2];
        dotsRight = new Vector3[sideDots.Length / 2];

        int sideDotsIdx = 0;
        for (float i = 0; i <= pathCreator.path.length; i += deltaForRiverSegments)
        {
            Vector3 direction = pathCreator.path.GetDirectionAtDistance(i);
            Vector3 pos = pathCreator.path.GetPointAtDistance(i);

            // TODO find a way to change width of the river :)
            // right side of the river
            Vector3 rotatedLine = (Quaternion.AngleAxis(90, transform.up) * direction).normalized;
            // Vector3 right = rotatedLine * Random.Range(minDistanceFromRiverCurve, maxDistanceFromRiverCurve);
            sideDots[sideDotsIdx] = pos + rotatedLine;
            sideDotsIdx++;

            // left side of the river
            Vector3 rotatedLineNegative = (Quaternion.AngleAxis(-90, transform.up) * direction).normalized;
            // Vector3 left = rotatedLineNegative * Random.Range(minDistanceFromRiverCurve, maxDistanceFromRiverCurve);
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
            float x = Random.Range(minDeltaX, maxDeltaX);
            float z = Random.Range(minDeltaZ, maxDeltaZ);
            points[i] = new Vector2(x, z) + points[i - 1];
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
