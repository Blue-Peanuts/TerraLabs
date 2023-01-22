using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCopy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer target;

    private void Update()
    {
        GetComponent<SpriteRenderer>().color = target.color;
    }
}
