using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price;
    bool isPurchased = false;
    CoinsUI CoinsUI;
    
    void Start()
    {
        GameObject header = GameObject.Find("Coins Text");
        CoinsUI = header.GetComponent<CoinsUI>();
    }

    void Update()
    {

    }

    public void onPurchase()
    {
        float coinsAmount = PlayerPrefs.GetFloat("Coins");
        PlayerPrefs.SetFloat("Coins", coinsAmount - price);
        CoinsUI.updateCoinsInUI();
    }
}
