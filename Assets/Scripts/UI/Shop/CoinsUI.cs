using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    float coins;
    void Start()
    {
        updateCoinsInUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCoinsInUI()
    {
        coins = PlayerPrefs.GetFloat("Coins");
        coinsText.text = "Coins: " + coins;
    }
}