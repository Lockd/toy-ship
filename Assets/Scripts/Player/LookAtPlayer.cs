using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject text;
    bool isActive = false;
    float bestDistance;

    void Start()
    {
        bestDistance = PlayerPrefs.GetFloat("BestDistance") * 10;
        transform.position = new Vector3(0f, transform.position.y, bestDistance);
        text.SetActive(false);
    }

    void Update()
    {
        if (player != null && bestDistance != 0)
        {
            if (transform.position.z - player.transform.position.z < 100f && !isActive)
            {
                text.SetActive(true);
                isActive = true;
            }

            if (isActive)
            {
                // transform.LookAt(player.transform);

                transform.position = new Vector3(
                    player.transform.position.x,
                    transform.position.y,
                    bestDistance
                );
            }

            if (player.transform.position.z > transform.position.z)
            {
                Destroy(gameObject);
            }
        }
    }
}
