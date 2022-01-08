using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FoxBehaviour : MonoBehaviour
{
    public Animator animator;
    public PathCreator pathCreator;
    public float minDistanceToPlayer = 5f;
    GameObject player;
    float distanceTravelled = 0f;
    ThirdPersonMovement thirdPersonMovement;
    bool isNearPlayer = false;

    void Start()
    {
        player = GameObject.Find("Player");
        thirdPersonMovement = player.GetComponent<ThirdPersonMovement>();
    }

    void Update()
    {
        if (player != null && thirdPersonMovement != null)
        {
            Debug.Log(player.transform.position.z);
            Debug.Log(transform.position.z);
            if (transform.position.z - player.transform.position.z < minDistanceToPlayer && !isNearPlayer)
            {
                animator.SetBool("isNearPlayer", true);
                isNearPlayer = true;
            }

            if (isNearPlayer)
            {
                distanceTravelled += (thirdPersonMovement.speed + 3f) * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            }
        }
    }
}
