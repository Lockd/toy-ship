using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindSpawn : MonoBehaviour
{
    public GameObject Player;
    PlayerState playerState = null;
    void Start()
    {
        if (playerState == null)
        {
            playerState = Player.GetComponent<PlayerState>();
        }

        if (!playerState.canRewind && Random.Range(0f, 1f) >= 0.9f)
        {
            Debug.Log("Time Rewind spawned ");
        }
        else
        {
            Debug.Log("Time rewind buff destroyed");
            Destroy(gameObject);
        }
    }
}
