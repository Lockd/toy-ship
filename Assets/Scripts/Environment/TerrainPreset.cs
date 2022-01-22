using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Terrain Color Preset", menuName = "Scriptables/Terrain Color Preset", order = 1)]
public class TerrainPreset : ScriptableObject
{
    public Gradient TerrainGradient;
}
