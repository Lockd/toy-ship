using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public GameObject[] wheels;
    Vector3 wheelsRotationVector = new Vector3 (0f, 0f, -3f);
    public CharacterController controller;
    float carSpeed;
    public bool isGoingBack = false;
    GameObject Player = null;

    void Start() {
        carSpeed = Random.Range(50f, 70f);

        if (Player == null)
            Player = GameObject.Find("Player");
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

        if (
            Player.transform.position.z - transform.position.z > 200f ||
            transform.position.z - Player.transform.position.z > 800f
        ) {
            Destroy(gameObject);
        }
    }
}
