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
    public int amountOfSegmentsBetweenTwoDots = 4;
    public RiverMeshGenerator riverMeshGenerator;
    public GameObject player;
    List<Vector3> dotsLeft = null;
    List<Vector3> dotsRight = null;
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

    BezierPath GeneratePath(Vector2[] points)
    {
        BezierPath bezierPath = new BezierPath(points, false, PathSpace.xz);
        return bezierPath;
    }

    void GenerateRiver(bool isInitial)
    {
        GenerateRiverPoints(isInitial);
        Vector2[] riverPointsArray = riverPoints.ToArray();
        BezierPath path = GeneratePath(riverPointsArray);
        pathCreator.bezierPath = path;

        dotsLeft = new List<Vector3>();
        dotsRight = new List<Vector3>();

        for (int idx = 0; idx < riverPointsArray.Length - 1; idx++)
        {
            float currentDistance = pathCreator.path.GetClosestDistanceAlongPath(
                new Vector3(riverPointsArray[idx].x, 0f, riverPointsArray[idx].y)
            );
            float nextDistance = pathCreator.path.GetClosestDistanceAlongPath(
                new Vector3(riverPointsArray[idx + 1].x, 0f, riverPointsArray[idx + 1].y)
            );
            float delta = (nextDistance - currentDistance) / amountOfSegmentsBetweenTwoDots;
            for (int i = 0; i < amountOfSegmentsBetweenTwoDots; i++)
            {
                float distance = currentDistance + i * delta;
                Vector3 direction = pathCreator.path.GetDirectionAtDistance(distance);
                Vector3 pos = pathCreator.path.GetPointAtDistance(distance);

                // right side of the river
                Vector3 rotatedLine = (Quaternion.AngleAxis(90, transform.up) * direction).normalized;
                dotsRight.Add(pos + rotatedLine * 2);

                // left side of the river
                Vector3 rotatedLineNegative = (Quaternion.AngleAxis(-90, transform.up) * direction).normalized;
                dotsLeft.Add(pos + rotatedLineNegative);
            }
        }

        riverMeshGenerator.GenerateMesh(dotsLeft.ToArray(), dotsRight.ToArray(), isInitial, amountOfSegmentsBetweenTwoDots);
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
