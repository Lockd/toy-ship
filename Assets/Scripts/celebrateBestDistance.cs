using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celebrateBestDistance : MonoBehaviour
{
    public GameObject celebrationIndicator;
    private float maxDistance;
    private Vector3 celebrationIndicatorPostition;
    private bool haveSpawnedCelebrationIndicator = false;
    void Start()
    {
        maxDistance = PlayerPrefs.GetFloat("BestDistance") * 10;
        celebrationIndicatorPostition = new Vector3 (0f, 0f, 40f);
    }

    void Update()
    {
        if (maxDistance - transform.position.z < 300f && !haveSpawnedCelebrationIndicator) {
            Instantiate(celebrationIndicator, celebrationIndicatorPostition, transform.rotation);
            haveSpawnedCelebrationIndicator = true;
        }
    }
}
