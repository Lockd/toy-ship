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
    public int riverLength = 20;
    public RiverMeshGenerator riverMeshGenerator;
    public GameObject player;
    Vector3[] dotsLeft = null;
    Vector3[] dotsRight = null;
    Vector3[] prevLeftDots = null;
    List<Vector2> riverPoints;

    void Start()
    {
        riverPoints = new List<Vector2>();
        GenerateRiver(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GenerateRiver(true);
        }
        if (
            Input.GetKeyDown(KeyCode.E) ||
            player.transform.position.z - riverPoints[0].y > 30
        )
        {
            GenerateRiver(false);
        }
    }

    BezierPath GeneratePath(Vector2[] points, bool closedPath)
    {
        BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xz);
        return bezierPath;
    }

    void GenerateRiver(bool isInitial)
    {
        GenerateRiverPoints(isInitial);
        Vector2[] riverPointsArray = riverPoints.ToArray();
        BezierPath path = GeneratePath(riverPointsArray, false);
        pathCreator.bezierPath = path;

        int dotsLength = (int)(pathCreator.path.length / deltaForRiverSegments * 2 + 2) / 2;
        prevLeftDots = dotsLeft;
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
            dotsRight[dotsIdx] = pos + rotatedLine * 2;

            // left side of the river
            Vector3 rotatedLineNegative = (Quaternion.AngleAxis(-90, transform.up) * direction).normalized;
            dotsLeft[dotsIdx] = pos + rotatedLineNegative;

            dotsIdx++;
        }

        riverMeshGenerator.GenerateMesh(dotsLeft, dotsRight, isInitial);
    }

    void GenerateRiverPoints(bool isInitial)
    {
        if (isInitial)
        {
            riverPoints.Add(new Vector2(0f, 0f));
            riverPoints.Add(new Vector2(0, Random.Range(minDeltaZ, maxDeltaZ)));

            int i = 2;
            while (i < riverLength)
            {
                float x = Random.Range(minDeltaX, maxDeltaX);
                float z = Random.Range(minDeltaZ, maxDeltaZ);
                riverPoints.Add(new Vector2(x, z) + riverPoints[i - 1]);
                i++;
            }
        }
        else
        {
            riverPoints.RemoveAt(0);
            float x = Random.Range(minDeltaX, maxDeltaX);
            float z = Random.Range(minDeltaZ, maxDeltaZ);
            Vector2 lastPoint = new Vector2(x, z) + riverPoints[riverPoints.Count - 1];
            riverPoints.Add(lastPoint);
        }
    }
}
