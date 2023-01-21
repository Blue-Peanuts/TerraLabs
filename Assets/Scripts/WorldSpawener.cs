using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawener : MonoBehaviour
{
    void Start()
    {
        BlockPlacer blockPlacer = FindObjectOfType<BlockPlacer>(); 
        for (int i = -30; i <= 30; i++) for (int j = -30; j <= 30; j++)
        {
            Vector3 snappedPosition = new Vector3(i + 0.5f, j + 0.5f, 0);
            Instantiate(blockPlacer.GetPrefabFromBlockType(BlockType.Water), snappedPosition, Quaternion.identity);
        }
    }
}
