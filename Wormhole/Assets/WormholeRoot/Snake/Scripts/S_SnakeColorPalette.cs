using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Snake_ColorPalette", menuName = "Snake/Color Palette", order = 3)]
public class S_SnakeColorPalette : ScriptableObject
{
    public Color minColor;
    public Color middleColor;
    public Color maxColor;
}
