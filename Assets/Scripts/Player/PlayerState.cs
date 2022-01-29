using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    List<PointInTime> playerPositions;
    public float rewindDuration = 3f;
    public CharacterController controller;
    [HideInInspector] public bool isRewinding = false;
    [HideInInspector] public bool canRewind = false;
    [HideInInspector] public bool isMagnetic = false;
    public float magnetismDuration = 30f;
    float timeToEndMagnetism;
    ThirdPersonMovement movement;
    [HideInInspector] public int collectedCoins = 0;
    public GameObject buffContainerGameObject;
    BuffContainer buffContainer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Time Rewind Buff")
        {
            canRewind = true;
            buffContainer.addBuff(-1, "Rewind");
            Destroy(other.gameObject);
        }

        if (other.tag == "Collectable")
        {
            collectedCoins++;
        }

        if (other.tag == "Magnet")
        {
            isMagnetic = true;
            timeToEndMagnetism = Time.time + magnetismDuration;
            buffContainer.addBuff(timeToEndMagnetism, "Magnet");
            Destroy(other.gameObject);
        }
    }

    void Start()
    {
        playerPositions = new List<PointInTime>();
        movement = transform.GetComponent<ThirdPersonMovement>();
        buffContainer = buffContainerGameObject.GetComponent<BuffContainer>();
    }

    void Awake()
    {
        collectedCoins = 0;
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            RecordPlayerPosition();
        }
        if (isMagnetic && timeToEndMagnetism != 0 && Time.time > timeToEndMagnetism)
        {
            isMagnetic = false;
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
