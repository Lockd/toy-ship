using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    List<PointInTime> playerPositions;
    public float rewindDuration = 3f;
    public Camera thirdPersonCamera;
    [HideInInspector] public bool isRewinding = false;
    
    // TODO manipulate this variable when picking up time rewind thingy :)
    [HideInInspector] public bool canRewind = true;

    void Start()
    {
        playerPositions = new List<PointInTime>();
    }

    void Update()
    {
        if (isRewinding) {
            Rewind();
        } else {
            RecordPlayerPosition();
        }
    }

    void Rewind() {
        if (playerPositions.Count > 0) {
            PointInTime pointInTime = playerPositions[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            playerPositions.RemoveAt(0);
        } else {
            StopRewind();
        }
    }

    void RecordPlayerPosition() {
        if (playerPositions.Count > Mathf.Round(rewindDuration / Time.fixedDeltaTime))
		{
			playerPositions.RemoveAt(playerPositions.Count - 1);
		}

		playerPositions.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    void StopRewind() {
        isRewinding = false;
    }

    public void StartRewind() {
        isRewinding = true;
        canRewind = false;
    }
}
