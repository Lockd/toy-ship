using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    void Start()
    {
        if (Random.Range(0f, 1f) >= .5f)
        {
            Destroy(gameObject);
        }
    }
}
