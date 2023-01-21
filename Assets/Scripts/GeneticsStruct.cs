using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Genetics
{
    public readonly Gene redColor;
    public readonly Gene greenColor;
    public readonly Gene blueColor;
    public readonly Gene Adaptability;
    public readonly Gene Aggressiveness;

    public void Randomize()
    {
        redColor.Randomize();
        greenColor.Randomize();
        blueColor.Randomize();
        Adaptability.Randomize();
        Aggressiveness.Randomize();
    }

    public void Mutate()
    {
        redColor.Mutate();
        greenColor.Mutate();
        blueColor.Mutate();
        Adaptability.Mutate();
        Aggressiveness.Mutate();
    }
}
