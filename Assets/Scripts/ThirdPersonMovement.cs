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
    public float rotationAcceleration = 0.3f;
    public float gravity = -9.8f;

    [SerializeField] private Slider slider;

    [HideInInspector] public int amountOfPointsInWater = 0;

    private float initialRotationAcceleration;
    private float previousHorizontalDirection;
    private bool wasRotatingRight = false;
    private bool wasMoving = false;
    private float timeOfExit;
    
    private void Start()
    {
        initialRotationAcceleration = rotationAcceleration;
    }

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

    // Update is called once per frame
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

        if (horizontal > 0)
            wasRotatingRight = true;

        if (horizontal < 0)
            wasRotatingRight = false;

        float horizontalDirection = previousHorizontalDirection;
        Vector3 rotationDirection;
        if (horizontal != 0)
        {
            rotationDirection = new Vector3(0f, horizontal, 0f);
            transform.Rotate(rotationDirection * speedOverTime * Time.deltaTime * smoothValueOnTurn);
            previousHorizontalDirection = horizontal;
            horizontalDirection += horizontal;
            rotationAcceleration = initialRotationAcceleration;
            wasMoving = true;

        } else if (rotationAcceleration > 0 && wasMoving) {
            float rotationWithDirection = rotationAcceleration;

            if (!wasRotatingRight)
                rotationWithDirection = -rotationAcceleration;

            rotationDirection = new Vector3(0f, rotationWithDirection, 0f).normalized;
            transform.Rotate(rotationDirection * speedOverTime * Time.deltaTime * smoothValueOnTurn);
            rotationAcceleration -= 0.008f;
        }

        Vector3 direction = transform.forward + Physics.gravity;
        Vector3 movementDirection = direction * speedOverTime * Time.deltaTime;
        controller.Move(movementDirection);
    }
}
