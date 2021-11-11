using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfInWater : MonoBehaviour
{
    ThirdPersonMovement movementScript;
    private void Start()
    {
         movementScript = gameObject.GetComponentInParent<ThirdPersonMovement>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water" && movementScript != null)
        {
            movementScript.OnExitWater();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water" && movementScript != null)
        {
            movementScript.OnEnterWater();
        }
    }
}
