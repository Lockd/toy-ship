using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celebrateBestDistance : MonoBehaviour
{
    public ParticleSystem confetti1;
    public ParticleSystem confetti2;
    float bestDistance;
    bool isCelebrated = false;

    void Start()
    {
        bestDistance = PlayerPrefs.GetFloat("BestDistance") * 10;
        Debug.Log("Best distance " + bestDistance);
        confetti1.Stop();
        confetti2.Stop();
    }

    void Update()
    {
        if (
            transform.position.z > bestDistance &&
            !isCelebrated &&
            bestDistance > 0
        ) {
            isCelebrated = true;
            confetti1.Play();
            confetti2.Play();
        }
    }
}
