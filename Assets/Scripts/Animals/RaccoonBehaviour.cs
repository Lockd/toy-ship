using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonBehaviour : MonoBehaviour
{
    public Animator animator;
    public bool startDigging = false;
    public bool isSleeping = false;
    void Start()
    {
        if (startDigging)
        {
            animator.SetBool("startDigging", true);
        }
        if (isSleeping)
        {
            animator.SetBool("isSleeping", true);
        }
    }

    // TODO add Update method and run / jump behaviour if needed

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Animal_jump_collider")
        {
            animator.SetTrigger("Jump");
        }
    }
}
