using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    void Start()
    {
        float amountOfCoins = transform.childCount;
        
        if (Random.Range(0f, 1f) < (amountOfCoins / 10)) {
            Destroy(transform.gameObject);
        }
    }
}
