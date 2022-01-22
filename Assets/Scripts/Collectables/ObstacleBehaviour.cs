using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    void Start()
    {
        if (Random.Range(0f, 1f) > 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
