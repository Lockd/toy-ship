using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoadTile : MonoBehaviour
{
    public GameObject[] roadTypes;
    public bool isFirstTile = false;
    
    void Start()
    {
        if (!isFirstTile) {
            GameObject roadToInstanciate = roadTypes[Random.Range(0, roadTypes.Length)];
            GameObject road = Instantiate(roadToInstanciate, transform);
            road.transform.position += new Vector3 (50.3f, 0f, 0f);
        }
    }
}
