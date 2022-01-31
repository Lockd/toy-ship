using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int price;
    Button button;
    bool isPurchased = false;
    CoinsUI CoinsUI;
    string GameObjectStoreName;

    void Start()
    {
        GameObjectStoreName = "is" + gameObject.name + "Purchased";
        GameObject header = GameObject.Find("Coins Text");

        button = gameObject.GetComponent<Button>();

        CoinsUI = header.GetComponent<CoinsUI>();
        int wasPurchased = PlayerPrefs.GetInt(GameObjectStoreName);
        if (wasPurchased > 0)
        {
            isPurchased = true;
            button.interactable = false;
        }
    }

    public void onPurchase()
    {
        float coinsAmount = PlayerPrefs.GetFloat("Coins");
        if (coinsAmount >= price && !isPurchased)
        {
            PlayerPrefs.SetFloat("Coins", coinsAmount - price);
            isPurchased = true;
            button.interactable = false;
            PlayerPrefs.SetInt(GameObjectStoreName, 1);
            CoinsUI.updateCoinsInUI();
        }
        else
        {
            // TODO display tolltip error about not enough money
        }
    }
}
