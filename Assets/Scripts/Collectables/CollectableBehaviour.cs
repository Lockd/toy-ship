using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public int itemIdx = 0;
    float delayByIndex = 0.4f;
    public bool isCoin = false;
    public float magneticSpeed = 18f;
    public float magnetismDistance = 15f;
    bool shouldMagnetise = true;
    PlayerState playerState;
    Vector3 initialPosition;
    float delayForAnimation;

    void Start()
    {
        delayForAnimation = delayByIndex * itemIdx;
        initialPosition = transform.position;

        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
    }

    void Update()
    {
        if (playerState != null)
        {
            if (
                playerState.isMagnetic &&
                Vector3.Distance(transform.position, playerState.transform.position) < magnetismDistance &&
                isCoin &&
                shouldMagnetise
            ) {
                float step = magneticSpeed * Time.deltaTime;
                Vector3 targetPosition = playerState.transform.position + new Vector3 (0f, .7f, 0f);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            }
            else
            {
                float deltaY = Mathf.Sin(Time.time - delayForAnimation) / 5;
                transform.position = initialPosition + new Vector3(0f, deltaY, 0f);
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
            }

            if (Vector3.Distance(transform.position, playerState.transform.position) < 0.01f)
            {
                shouldMagnetise = false;
            }
        }
    }
}
