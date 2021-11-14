using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateTravelledDistance : MonoBehaviour
{
    public GameObject Player;
    [HideInInspector] public float maxDistance = 0;
    private Text textComponent;

    void Start() {
        textComponent = GetComponent<UnityEngine.UI.Text>();
    }
    
    void Awake() {
        gameObject.SetActive(true);

    }

    public void onGameOver() {
        gameObject.SetActive(false);
    }

    void Update()
    {
        float travelledDistance = Mathf.Round(Player.transform.position.z / 10);

        if (travelledDistance >= maxDistance) {
            textComponent.text = travelledDistance.ToString() + " meters";
            maxDistance = travelledDistance;
        }
    }
}
