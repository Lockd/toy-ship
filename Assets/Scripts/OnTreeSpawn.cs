using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTreeSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randomScaleValue = Random.Range(3.2f, 5.4f);
        transform.localScale = new Vector3(randomScaleValue, randomScaleValue, randomScaleValue);
    }
}
