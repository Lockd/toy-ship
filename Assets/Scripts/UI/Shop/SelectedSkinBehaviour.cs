using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSkinBehaviour : MonoBehaviour
{
    float rotationSpeed = 60f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
