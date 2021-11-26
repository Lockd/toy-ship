using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public GameObject[] wheels;
    Vector3 wheelsRotationVector = new Vector3 (0f, 0f, -3f);
    public CharacterController controller;
    float carSpeed;
    bool isGoingBack = false;

    void Start() {
        carSpeed = Random.Range(50f, 70f);
    }

    void Update()
    {
        foreach (var wheel in wheels)
        {
            wheel.transform.Rotate(wheelsRotationVector);
        }
        Vector3 carDirection = isGoingBack ? new Vector3 (0f, 0f, -1f) : new Vector3 (0f, 0f, 1f);
        Vector3 movementDirection = (carDirection + Physics.gravity) * carSpeed * Time.deltaTime;
        controller.Move(movementDirection);
    }
}
