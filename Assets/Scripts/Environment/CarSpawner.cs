using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] cars;
    public float minSpawnCooldown = 7f;
    public float maxSpawnCooldown = 11f;
    public bool isBackwardsSpawner = false;
    float timeOfLastSpawn = 0f;
    float currentSpawnCooldown = 0f;


    void Update()
    {
        float chanceOfSpawn = Random.Range(0f, 1f);        

        if (chanceOfSpawn > 0.8f && Time.time - timeOfLastSpawn > currentSpawnCooldown)
        {
            GameObject car = Instantiate(cars[Random.Range(0, cars.Length)], transform);

            if (isBackwardsSpawner)
                car.GetComponent<CarMovement>().isGoingBack = true;

            timeOfLastSpawn = Time.time;
            currentSpawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);
        }
    }
}
