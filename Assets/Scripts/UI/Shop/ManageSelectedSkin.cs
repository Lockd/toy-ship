using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSelectedSkin : MonoBehaviour
{
    Vector3 skinPosition = new Vector3(0f, 0f, -124f);

    public void updateSelectedSkin(ShipSkin newSkin)
    {
        GameObject skin = Instantiate(newSkin.model, skinPosition, transform.rotation);
        skin.AddComponent<SelectedSkinBehaviour>();
    }
}
