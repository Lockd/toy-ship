using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[ExecuteAlways]
public class RiverPathGenerator : MonoBehaviour
{
    public PathCreator pathCreator;
    public float minDeltaX = .5f;
    public float maxDeltaX = .5f;
    public float minDeltaZ = .5f;
    public float maxDeltaZ = .5f;
    // next two variables will be used for manipulating river width
    // public float minDistanceFromRiverCurve = 1f;
    // public float maxDistanceFromRiverCurve = 3f;
    public float deltaForRiverSegments = 1f;
    public int lengthOfRiverSegment = 20;
    public RiverMeshGenerator riverMeshGenerator;
    Vector3[] dotsLeft = null;
    Vector3[] dotsRight = null;

    void Start()
    {
        RegenerateRiver(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            RegenerateRiver(true);
        }
    }

    BezierPath GeneratePath(Vector2[] points, bool closedPath)
    {
        BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xz);
        return bezierPath;
    }

    void RegenerateRiver(bool isInitial)
    {
        Vector2[] points = GenerateRiverPoints(isInitial);
        BezierPath path = GeneratePath(points, false);
        pathCreator.bezierPath = path;

        int dotsLength = (int)(pathCreator.path.length / deltaForRiverSegments * 2 + 2) / 2;
        dotsLeft = new Vector3[dotsLength];
        dotsRight = new Vector3[dotsLength];

        int dotsIdx = 0;
        for (float i = 0; i <= pathCreator.path.length; i += deltaForRiverSegments)
        {
            Vector3 direction = pathCreator.path.GetDirectionAtDistance(i);
            Vector3 pos = pathCreator.path.GetPointAtDistance(i);

            // TODO find a way to change width of the river :)
            // right side of the river
            Vector3 rotatedLine = (Quaternion.AngleAxis(90, transform.up) * direction).normalized;
            dotsRight[dotsIdx] = pos + rotatedLine;

            // left side of the river
            Vector3 rotatedLineNegative = (Quaternion.AngleAxis(-90, transform.up) * direction).normalized;
            dotsLeft[dotsIdx] = pos + rotatedLineNegative;

            dotsIdx++;
        }

        riverMeshGenerator.GenerateMesh(dotsLeft, dotsRight);
    }

    Vector2[] GenerateRiverPoints(bool isInitial)
    {
        Vector2[] points = new Vector2[lengthOfRiverSegment];
        points[0] = new Vector2(0f, 0f);

        int i = 1;
        if (isInitial)
        {
            points[1] = new Vector2(0, Random.Range(minDeltaZ, maxDeltaZ));
            i = 2;
        }

        while (i < lengthOfRiverSegment)
        {
            float x = Random.Range(minDeltaX, maxDeltaX);
            float z = Random.Range(minDeltaZ, maxDeltaZ);
            points[i] = new Vector2(x, z) + points[i - 1];
            i++;
        }
        return points;
    }

    void OnDrawGizmos()
    {
        if (dotsLeft != null && dotsRight != null)
        {
            foreach (Vector3 vector in dotsLeft)
            {
                Gizmos.DrawSphere(vector, .05f);
            }
            foreach (Vector3 vector in dotsRight)
            {
                Gizmos.DrawSphere(vector, .05f);
            }
        }
    }
}
