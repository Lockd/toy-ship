using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class AnimalBehaviour : MonoBehaviour
{
    public Animator animator;
    public PathCreator pathCreator;
    public float minDistanceToPlayer = 18f;
    public bool startDigging = false;
    public bool isSleeping = false;
    GameObject player;
    float distanceTravelled = 0f;
    ThirdPersonMovement thirdPersonMovement;
    bool isNearPlayer = false;
    List<string> runningAnimations = new List<string>{ "Fox_Run", "Rab_Run", "Rac_Run Forward" };

    void Start()
    {
        player = GameObject.Find("Player");
        thirdPersonMovement = player.GetComponent<ThirdPersonMovement>();

        if (startDigging)
        {
            animator.SetBool("startDigging", true);
        }

        if (isSleeping)
        {
            animator.SetBool("isSleeping", true);
        }
    }

    void Update()
    {
        if (player != null && thirdPersonMovement != null)
        {
            if (transform.position.z - player.transform.position.z < minDistanceToPlayer && !isNearPlayer && pathCreator != null)
            {
                animator.SetBool("isNearPlayer", true);
                isNearPlayer = true;
            }

            AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
            
            if (isNearPlayer && runningAnimations.Contains(clips[0].clip.name))
            {
                distanceTravelled += (thirdPersonMovement.speed + 3f) * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Animal_jump_collider")
        {
            animator.SetTrigger("Jump");
        }
    }
}
