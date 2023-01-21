using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorPallet")]
public class ColorPallet : ScriptableObject
{
    [SerializeField] private Color[] colors;
}
