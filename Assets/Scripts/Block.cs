using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{ 
    Water, Plains, Hill, Bush
}

public class Block : MonoBehaviour
{
    [SerializeField] private BlockType type;
    public BlockType Type => type;
}
