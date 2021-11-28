using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameOverScreen gameOverScreen;
    public CalculateTravelledDistance calculateTravelledDistance;
    public float speed = 20f;
    public float maxSpeed = 50f;
    public float smoothValueOnTurn = 40f;
    public int amountOfPointsInWater = 0;
    public Joystick joystick;

    private float timeOfExit;
    private bool haveEnteredWater = false;
    private bool isOnGameOverScreen = false;
    private int collectedCoins = 0;

    public void OnExitWater()
    {
        amountOfPointsInWater -= 1;
        timeOfExit = Time.time;
    }

    public void OnEnterWater()
    {
        amountOfPointsInWater += 1;
        haveEnteredWater = true;
    }
    void Awake()
    {
        haveEnteredWater = false;
        amountOfPointsInWater = 0;
        isOnGameOverScreen = false;
        collectedCoins = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            OnGameOver();
        }

        if (other.tag == "Collectable") {
            collectedCoins++;
            Destroy(other.gameObject);
        }
    }
    public void OnGameOver() {
        isOnGameOverScreen = true;
        calculateTravelledDistance.onGameOver();
        gameOverScreen.onDisplayChange(true, calculateTravelledDistance.maxDistance, collectedCoins);
    }

    void Update()
    {
        float horizontal = joystick.Horizontal * 0.3f;

        float additionalSpeed = transform.position.z / 10000 * 20f;
        float speedOverTime = speed + additionalSpeed;

        if (
            (speedOverTime <= 0f || amountOfPointsInWater <= 0) &&
            (!isOnGameOverScreen && haveEnteredWater)
        ) {
            OnGameOver();
        }

        if (speedOverTime > maxSpeed)
            speedOverTime = maxSpeed;

        if (!isOnGameOverScreen){

            if (horizontal != 0) {
                Vector3 rotationDirection = new Vector3(0f, horizontal, 0f);
                transform.Rotate(rotationDirection * speedOverTime * Time.deltaTime * smoothValueOnTurn);
            }

            Vector3 direction = transform.forward + Physics.gravity;
            Vector3 movementDirection = direction * speedOverTime * Time.deltaTime;
            controller.Move(movementDirection);
        }
    }
}
