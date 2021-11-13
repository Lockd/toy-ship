using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 20f;
    public float speedByTimeMultiplier = 1.1f;
    public float maxSpeed = 50f;
    public float smoothValueOnTurn = 40f;
    public float gravity = -9.8f;

    [SerializeField] private Slider slider;

    [HideInInspector] public int amountOfPointsInWater = 0;
    private float previousHorizontalDirection;
    private bool wasMoving = false;
    private float timeOfExit;

    public void OnExitWater()
    {
        amountOfPointsInWater -= 1;
        timeOfExit = Time.time;
    }

    public void OnEnterWater()
    {
        amountOfPointsInWater += 1;
        // Kostyl' ebanii :)
        if (amountOfPointsInWater > 4)
            amountOfPointsInWater = 4;
    }

    void Update()
    {
        float horizontal = slider.value;

        float additionalSpeed = Time.time / 60 * speedByTimeMultiplier;
        float speedOverTime = speed + additionalSpeed;

        if (
            speedOverTime <= 0f ||
            (Time.time - timeOfExit > 1f && amountOfPointsInWater < 4) ||
            amountOfPointsInWater <= 0
        ) {
            // game over here
            // Debug.Log("Game Over");
        }

        if (speedOverTime > maxSpeed)
            speedOverTime = maxSpeed;

        float horizontalDirection = previousHorizontalDirection;
        Vector3 rotationDirection;
        if (horizontal != 0)
        {
            rotationDirection = new Vector3(0f, horizontal, 0f);
            transform.Rotate(rotationDirection * speedOverTime * Time.deltaTime * smoothValueOnTurn);
            previousHorizontalDirection = horizontal;
            horizontalDirection += horizontal;
            
            wasMoving = true;
        }

        Vector3 direction = transform.forward + Physics.gravity;
        Vector3 movementDirection = direction * speedOverTime * Time.deltaTime;
        controller.Move(movementDirection);
    }
}
