using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FireBehaviour : MonoBehaviour
{
    public GameObject fireContainer;
    public GameObject _light;
    ParticleSystem fire;
    LightingManager lightingManager;
    // bool isFireLighted = true;

    void Start()
    {
        fire = fireContainer.GetComponent<ParticleSystem>();
        lightingManager = FindObjectOfType<LightingManager>();
        fire.Play();
    }

    void Update()
    {
        // if (lightingManager != null && fire != null)
        // {
        //     if (
        //         (lightingManager.TimeOfDay < 8f || lightingManager.TimeOfDay > 19f) &&
        //         !isFireLighted
        //     )
        //     {
        //         fire.Play();
        //         isFireLighted = true;
        //         _light.SetActive(true);
        //     }

        //     if (
        //         (lightingManager.TimeOfDay >= 8f && lightingManager.TimeOfDay <= 19f) &&
        //         isFireLighted
        //     )
        //     {
        //         fire.Stop();
        //         isFireLighted = false;
        //         _light.SetActive(false);
        //     }
        // }
    }
}
