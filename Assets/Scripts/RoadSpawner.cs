using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] StraightRoads;
    public GameObject[] StraightToLeftRoads;
    public GameObject[] LeftToStraightRoads;
    // TODO implement those road types as well?
    public int AmountOfTilesOnSpawn = 6;
    private string LastRoadType;
    private int indexOfLastTile = 1;
    private Vector3 initialRoadPosition = new Vector3(0f, 0f, 0f);
    private Vector3 positionScale = new Vector3(0f, 0f, 160f);
    Vector3[] additionsForStreetLightPositions = {
        new Vector3 (-35f, 0f, -40f),
        new Vector3 (-35f, 0f, 40f),
        new Vector3 (75F, 0f, 40f),
        new Vector3 (75f, 0f, -40f)
    };

    void Start()
    {
        while (indexOfLastTile < AmountOfTilesOnSpawn)
        {
            createNewTile();
        }
    }

    GameObject getNextRoadType()
    {
        bool shouldChangeDirection = false;
        if (Random.Range(0f, 1f) > 0.5f)
            shouldChangeDirection = true;

        switch (LastRoadType)
        {
            case "straight":
                if (shouldChangeDirection)
                {
                    LastRoadType = "left";
                    return StraightToLeftRoads[Random.Range(0, StraightToLeftRoads.Length)];
                }

                return StraightRoads[Random.Range(0, StraightRoads.Length)];

            case "left":
                // TODO add straight left roads and add chance to use them here

                LastRoadType = "straight";
                return LeftToStraightRoads[Random.Range(0, LeftToStraightRoads.Length)];
            default:
                LastRoadType = "straight";
                return StraightRoads[Random.Range(0, StraightRoads.Length)];
        }
    }

    Vector3 getPositionForNewTile()
    {
        return initialRoadPosition + positionScale * indexOfLastTile;
    }
    public void createNewTile()
    {

        GameObject road = Instantiate(getNextRoadType(), transform);
        road.transform.position = getPositionForNewTile();
        indexOfLastTile++;
    }
}
