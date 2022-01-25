using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    bool isSoundPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isSoundPlayed)
        {
            audioSource.Play(0);
            gameObject.SetActive(false);
            isSoundPlayed = true;
        }
    }
}

