using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageSelectedSkin : MonoBehaviour
{
    [HideInInspector] public bool isCurrentItemPurchased = false;
    public ShipSkin currentSkin;
    Vector3 skinPosition = new Vector3(0f, -76f, -124f);
    public Button buyButton;
    public Button selectButton;
    public GameObject selectedIndicator;

    GameObject instantiatedSkin;

    void Start()
    {
        if (currentSkin != null)
        {
            updateSelectedSkin(currentSkin);
        }
    }

    public void updateSelectedSkin(ShipSkin newSkin)
    {
        if (instantiatedSkin != null)
        {
            GameObject.Destroy(instantiatedSkin);
        }

        foreach (GameObject child in transform)
        {
            child.SetActive(false);
        }
        
        currentSkin = newSkin;
        instantiatedSkin = Instantiate(newSkin.model, new Vector3(0f, 0f, 0f), transform.rotation);

        // Layer 5 for UI
        instantiatedSkin.layer = 5;
        foreach (Transform child in instantiatedSkin.transform)
        {
            child.gameObject.layer = 5;
        }

        instantiatedSkin.transform.parent = gameObject.transform;

        instantiatedSkin.AddComponent<SelectedSkinBehaviour>();
        RectTransform rectTransform = instantiatedSkin.AddComponent<RectTransform>();

        rectTransform.anchoredPosition = skinPosition;

        string activeSkin = PlayerPrefs.GetString("activeSkin");
        if (activeSkin == newSkin.name)
        {
            selectedIndicator.SetActive(true);
        }
        else
        {
            int isPurchased = PlayerPrefs.GetInt("is" + newSkin.name + "Purchased");
            if (isPurchased > 0)
            {
                isCurrentItemPurchased = true;
                selectButton.gameObject.SetActive(true);
            }
            else
            {
                buyButton.gameObject.SetActive(true);
                int coinsAmount = PlayerPrefs.GetInt("Coins");
                if (coinsAmount < newSkin.price)
                {
                    buyButton.interactable = false;
                }
            }
        }
    }

    public void buySkin()
    {
        int coinsAmount = PlayerPrefs.GetInt("Coins");
        if (coinsAmount > currentSkin.price)
        {
            PlayerPrefs.SetInt("Coins", coinsAmount - currentSkin.price);
            PlayerPrefs.SetInt("is" + currentSkin.name + "Purchased", 1);
        }
    }

    public void selectSkin()
    {
        PlayerPrefs.SetString("activeSkin", currentSkin.name);
    }
}
