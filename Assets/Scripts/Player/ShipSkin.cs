using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Ship Skin", menuName = "Scriptables/Ship Skin", order = 2)]
public class ShipSkin : ScriptableObject
{
    public string skinName;
    public GameObject model;
    public int price = 20;
}
