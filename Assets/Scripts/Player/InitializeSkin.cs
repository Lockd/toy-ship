using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeSkin : MonoBehaviour
{
    public ShipSkin[] allSkins;
    string activeSkin;

    void Start()
    {
        activeSkin = PlayerPrefs.GetString("activeSkin");
        if (activeSkin.Length > 0)
        {
            foreach (ShipSkin skin in allSkins)
            {
                if (skin.name == activeSkin)
                {
                    GameObject initializeSkin = Instantiate(skin.model, transform);
                    break;
                }
            }
        }
        else
        {
            GameObject initializeSkin = Instantiate(allSkins[0].model, transform);
        }
    }
}
