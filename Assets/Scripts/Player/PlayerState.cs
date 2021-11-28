using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    List<PointInTime> playerPositions;
    public float rewindDuration = 3f;
    public CharacterController controller;
    [HideInInspector] public bool isRewinding = false;

    // TODO manipulate this variable when picking up time rewind thingy :)
    [HideInInspector] public bool canRewind = true;
    ThirdPersonMovement movement;

    void Start()
    {
        playerPositions = new List<PointInTime>();
        movement = transform.GetComponent<ThirdPersonMovement>();
    }

    void Update()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            RecordPlayerPosition();
        }
    }

    void Rewind()
    {
        if (playerPositions.Count > 0)
        {
            PointInTime pointInTime = playerPositions[0];
            Vector3 offset = pointInTime.position - transform.position;

            if (offset.magnitude > .1f)
            {

                float additionalSpeed = transform.position.z / 10000 * 20f;
                float speedOverTime = movement.speed + additionalSpeed;

                if (speedOverTime > movement.maxSpeed)
                {
                    speedOverTime = movement.maxSpeed;
                }

                offset = offset.normalized * movement.speed;
                controller.Move(offset * Time.deltaTime);
            }
            transform.rotation = pointInTime.rotation;
            playerPositions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void RecordPlayerPosition()
    {
        if (playerPositions.Count > Mathf.Round(rewindDuration / Time.fixedDeltaTime))
        {
            playerPositions.RemoveAt(playerPositions.Count - 1);
        }

        playerPositions.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    void StopRewind()
    {
        isRewinding = false;
        Time.timeScale = 1f;
    }

    public void StartRewind()
    {
        isRewinding = true;
        canRewind = false;
        Time.timeScale = 0.7f;
    }
}
