using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject StraightRoad;
    public GameObject StraightToLeftRoad;
    public GameObject LeftToStraightRoad;
    public GameObject LeftRoad;
    public int AmountOfTilesOnSpawn = 6;
    string LastRoadType;
    int indexOfLastTile = 1;
    Vector3 initialRoadPosition = new Vector3(0f, 0f, 0f);
    Vector3 positionScale = new Vector3(0f, 0f, 160f);

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
                    return StraightToLeftRoad;
                }

                return StraightRoad;

            case "left":
                if (shouldChangeDirection)
                {
                    LastRoadType = "straight";
                    return LeftToStraightRoad;
                }

                return LeftRoad;

            default:
                LastRoadType = "straight";
                return StraightRoad;
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
