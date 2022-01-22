using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCollectedCoins : MonoBehaviour
{
    public GameObject player;
    PlayerState playerState;
    TextMeshProUGUI text;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        playerState = player.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (text.text != playerState.collectedCoins.ToString())
        {
            text.text = playerState.collectedCoins.ToString();
        }
    }
}
