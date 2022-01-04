using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject coinModel;

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play(0);
        coinModel.transform.gameObject.SetActive(false);
    }
}
