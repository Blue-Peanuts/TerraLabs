using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overseer : MonoBehaviour
{
    public static Overseer Instance;
    private void Awake()
    {
        Instance = this;
    }
}
