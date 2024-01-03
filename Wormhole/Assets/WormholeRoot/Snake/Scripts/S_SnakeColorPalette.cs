using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_SnakeColorPalette", menuName = "Snake/ColorPalette", order = 1)]
public class S_SnakeColorPalette : ScriptableObject
{
    public Color minColor;
    public Color middleColor;
    public Color maxColor;
}
