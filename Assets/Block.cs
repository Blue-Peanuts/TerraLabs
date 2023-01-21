using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Plains, Hill, Water
}

public class Block : MonoBehaviour
{
    [SerializeField] private BlockType type;

    public BlockType Type => type;
}
