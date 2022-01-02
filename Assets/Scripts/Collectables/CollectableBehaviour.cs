using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public int itemIdx = 0;
    float delayByIndex = 0.4f;
    Vector3 initialPosition;
    float delayForAnimation;

    void Start()
    {
        delayForAnimation = delayByIndex * itemIdx;
        initialPosition = transform.position;
    }

    void Update()
    {
        float deltaY = Mathf.Sin(Time.time - delayForAnimation) / 5;
        transform.position = initialPosition + new Vector3(0f, deltaY, 0f);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
