using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Genetics
{
    public string species;
    public Gene redColor;
    public Gene greenColor;
    public Gene blueColor;
    public Gene adaptability;
    public Gene aggressiveness;

    public void Randomize()
    {
        redColor.Randomize();
        greenColor.Randomize();
        blueColor.Randomize();
        aggressiveness.Randomize();
        adaptability.Randomize();
        species = RandomNameGenerator.NameGenerator(adaptability.Value);
    }

    public void Mutate()
    {
        redColor.Mutate();
        greenColor.Mutate();
        blueColor.Mutate();
        adaptability.Mutate();
        aggressiveness.Mutate();
    }
}
