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
    public PlayerState playerState;

    float timeOfExit;
    bool haveEnteredWater = false;
    bool isOnGameOverScreen = false;
    
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            OnGameOver();
        }
    }
    public void OnGameOver()
    {
        if (playerState.canRewind)
        {
            playerState.StartRewind();
        }
        else
        {
            isOnGameOverScreen = true;
            calculateTravelledDistance.onGameOver();
            gameOverScreen.onDisplayChange(true, calculateTravelledDistance.maxDistance, playerState.collectedCoins);
        }
    }

    void Update()
    {
        float horizontal = joystick.Horizontal * 0.3f;

        float additionalSpeed = transform.position.z / 10000 * 20f;
        float speedOverTime = speed + additionalSpeed;

        if (
            (speedOverTime <= 0f || amountOfPointsInWater <= 0) &&
            (!isOnGameOverScreen && haveEnteredWater && !playerState.isRewinding)
        )
        {
            OnGameOver();
        }

        if (speedOverTime > maxSpeed)
            speedOverTime = maxSpeed;

        if (!isOnGameOverScreen && !playerState.isRewinding)
        {

            if (horizontal != 0)
            {
                Vector3 rotationDirection = new Vector3(0f, horizontal, 0f);
                transform.Rotate(rotationDirection * speedOverTime * Time.deltaTime * smoothValueOnTurn);
            }

            // Vector3 direction = -transform.forward + Physics.gravity;
            Vector3 direction = transform.forward + Physics.gravity;
            Vector3 movementDirection = direction * speedOverTime * Time.deltaTime;
            controller.Move(movementDirection);
        }
    }
}
