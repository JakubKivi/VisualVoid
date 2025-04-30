// ColorPalette.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorPalette", menuName = "Color Palette", order = 1)]
public class ColorPalette : ScriptableObject
{
    public Color primary;
    public Color secondary;
    public Color accent;
}