using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBehaviour : MonoBehaviour
{
    private RoadSpawner roadSpawner;
    private GameObject Player = null;

    [HideInInspector] public int index;
    void Start()
    {
        if (Player == null)
            Player = GameObject.Find("Player");

        if (roadSpawner == null)
            roadSpawner = FindObjectOfType<RoadSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z - transform.position.z  > 200)
        {
            roadSpawner.createNewTile();
            Destroy(gameObject);
        }
    }
}
