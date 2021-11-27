using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBehaviour : MonoBehaviour
{
    RoadSpawner roadSpawner;
    GameObject Player = null;
    GameObject carSpawnerForward;

    [HideInInspector] public int index;
    void Start()
    {
        if (Player == null)
            Player = GameObject.Find("Player");

        if (roadSpawner == null)
            roadSpawner = FindObjectOfType<RoadSpawner>();

        if (carSpawnerForward == null)
            carSpawnerForward = GameObject.Find("Car Spawner Forward");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z - transform.position.z  > 200)
        {
            roadSpawner.createNewTile();
            Destroy(gameObject);
            carSpawnerForward.transform.position += new Vector3(0f, 0f, 160f);
        }
    }
}
