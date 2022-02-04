using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSelectedSkin : MonoBehaviour
{
    [HideInInspector] public bool isCurrentItemPurchased = false;
    public ShipSkin currentSkin;
    Vector3 skinPosition = new Vector3(0f, -76f, -124f);

    void Start()
    {
        if (currentSkin != null)
        {
            updateSelectedSkin(currentSkin);
        }
    }

    public void updateSelectedSkin(ShipSkin newSkin)
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        
        GameObject skin = Instantiate(newSkin.model, new Vector3(0f, 0f, 0f), transform.rotation);
        
        // Layer 5 for UI
        skin.layer = 5;
        foreach(Transform child in skin.transform)
        {
            child.gameObject.layer = 5;
        }

        skin.transform.parent = gameObject.transform;
        
        skin.AddComponent<SelectedSkinBehaviour>();
        RectTransform rectTransform = skin.AddComponent<RectTransform>();

        rectTransform.anchoredPosition = skinPosition;

        int isPurchased = PlayerPrefs.GetInt("is" + newSkin.name + "Purchased");
        if (isPurchased > 0)
        {
            isCurrentItemPurchased = true;
            Debug.Log("Select button");
        }
        else
        {
            Debug.Log("Buy button");
        }
    }
}
