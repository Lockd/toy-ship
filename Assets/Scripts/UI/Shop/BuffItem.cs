using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffItem : MonoBehaviour
{
    public string buffName;
    public BuffCharacteristics[] upgradePrices;
    Button button;
    CoinsUI CoinsUI;
    int currentUpgradeLevel = 0;
    bool canBeUpgraded = true;

     void Start()
    {
        GameObject header = GameObject.Find("Coins Text");
        button = gameObject.GetComponent<Button>();
        CoinsUI = header.GetComponent<CoinsUI>();
        currentUpgradeLevel = PlayerPrefs.GetInt(buffName);

        if (currentUpgradeLevel == upgradePrices.Length - 1)
            canBeUpgraded = false;
        
        // TODO Add logic for different levels of buffs
        // TODO Add indicators for levels and instatntiate a new gameobject on purchase
    }

    public void onPurchase()
    {
        int coinsAmount = PlayerPrefs.GetInt("Coins");
        if (
            coinsAmount >= upgradePrices[currentUpgradeLevel].price &&
            canBeUpgraded
        )
        {
            PlayerPrefs.SetInt("Coins", coinsAmount - upgradePrices[currentUpgradeLevel].price);
            currentUpgradeLevel++;
            PlayerPrefs.SetInt(buffName, currentUpgradeLevel);
            CoinsUI.updateCoinsInUI();

            if (currentUpgradeLevel == upgradePrices.Length -1)
                canBeUpgraded = false;
        }
        else
        {
            // TODO display tolltip error about not enough money
            button.interactable = false;
        }
    }
}
