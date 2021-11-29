using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] StraightRoads;
    public GameObject[] StraightToLeftRoads;
    public GameObject[] LeftToStraightRoads;
    // TODO implement those road types as well?
    public GameObject[] LeftStraightRoads;
    public GameObject streetLight;
    public int AmountOfTilesOnSpawn = 6;
    public GameObject[] Trees;
    public GameObject carSpawnerBackwards;

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

        // adding street lights
        for (int i = 0; i < 4; i++) {
            GameObject instanciatedStreetLight = Instantiate(streetLight, transform);
            instanciatedStreetLight.transform.parent = road.transform;
            instanciatedStreetLight.transform.position = road.transform.position + additionsForStreetLightPositions[i];
            if (i >= 2) {
                instanciatedStreetLight.transform.Rotate(new Vector3 (0f, 180f, 0f));
            }
        }

        int amountOfTreesToSpawn = (int)Random.Range(3f, 5f);
        int amountOfTreesOnTheLefSide = amountOfTreesToSpawn / 2;
        
        for (int counter = 1; counter <= amountOfTreesToSpawn; counter++) {
            GameObject treeToInstanciate = Trees[Random.Range(0, Trees.Length)];
            GameObject tree = Instantiate(treeToInstanciate, transform);
            tree.transform.parent = road.transform;

            Vector3 TreePosition;
            float offset;
            if (counter <= amountOfTreesOnTheLefSide)
            {
                TreePosition = new Vector3(Random.Range(78f, 81f), 1.25f, 0f);
                offset = positionScale.z / amountOfTreesOnTheLefSide;
                TreePosition.z = offset * counter + Random.Range(-1f, 1f) - (positionScale.z / 2);
            }
            else
            {
                TreePosition = new Vector3(Random.Range(-35f, -42f), 1.25f, 0f);
                offset = positionScale.z / (amountOfTreesToSpawn - amountOfTreesOnTheLefSide);
                TreePosition.z = offset * (amountOfTreesToSpawn - counter) + Random.Range(-1f, 1f) - (positionScale.z / 2 );
            }

            tree.transform.position = road.transform.position + TreePosition;
        }

        // moving game object of car spawner when adding new tile
        carSpawnerBackwards.transform.position += positionScale;
    }
}
