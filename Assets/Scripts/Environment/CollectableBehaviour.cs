using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    public float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
