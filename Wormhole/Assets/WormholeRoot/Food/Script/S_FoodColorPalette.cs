using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_FoodColorPalette", menuName = "Level/Food ColorPalette", order = 1)]
public class S_FoodColorPalette : ScriptableObject
{
    [SerializeField] public Color [] colors;
}
