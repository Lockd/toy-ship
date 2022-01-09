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
