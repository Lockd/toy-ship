using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindSpawn : MonoBehaviour
{
    public GameObject Player;
    PlayerState playerState = null;
    bool prevCanRewind = false;
    void Start()
    {
        if (playerState == null)
        {
            playerState = Player.GetComponent<PlayerState>();
        }

        if (!playerState.canRewind && Random.Range(0f, 1f) < 0.75f)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Destroying buff if player picks one up
        if (playerState != null)
        {
            if (!prevCanRewind && playerState.canRewind)
                Destroy(gameObject);
        }
        prevCanRewind = playerState.canRewind;
    }
}
