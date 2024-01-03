using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_FoodColorPalette", menuName = "Level/FoodColorPalette", order = 2)]
public class S_FoodColorPalette : ScriptableObject
{
    public Color baseColor;
    //public Color comonColor;
    public Color rareColor;
    public Color epicColor;
    //public Color legendaryColor;
}
