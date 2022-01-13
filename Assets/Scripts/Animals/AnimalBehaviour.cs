using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class AnimalBehaviour : MonoBehaviour
{
    public Animator animator;
    public PathCreator pathCreator;
    public float speed;
    public float minDistanceToPlayer = 18f;
    public bool startDigging = false;
    public bool isSleeping = false;
    public bool isRunning = false;
    GameObject player;
    float distanceTravelled = 0f;
    ThirdPersonMovement thirdPersonMovement;
    List<string> animationsWithMovement;

    void Start()
    {
        animationsWithMovement = new List<string>{
            "Fox_Run", "Rab_Run", "Rac_Run Forward",
            "Fox_Jump_Run", "Rab_Jump_Run", "Rac_Jump Run"
        };

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

        if (isRunning)
        {
            animator.SetTrigger("Run");
        }
    }

    void Update()
    {
        if (player != null && thirdPersonMovement != null)
        {
            if (transform.position.z - player.transform.position.z < minDistanceToPlayer && !isRunning && pathCreator != null)
            {
                animator.SetTrigger("Run");
                isRunning = true;
            }

            AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
            if (clips.Length > 0)
            {
                if (isRunning && animationsWithMovement.Contains(clips[0].clip.name))
                {
                    float movementSpeed = speed != 0 ? speed : thirdPersonMovement.speed;
                    distanceTravelled += (movementSpeed + 3f) * Time.deltaTime;
                    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jumping does not work properly when following pathCreator
        if (other.tag == "Animal_jump_collider")
        {
            animator.SetTrigger("Jump");
        }
    }
}
