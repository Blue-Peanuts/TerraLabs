using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 radius;
    void Start()
    {
        BlockPlacer blockPlacer = FindObjectOfType<BlockPlacer>(); 
        for (int i = -Mathf.RoundToInt(radius.x); i <= Mathf.RoundToInt(radius.x); i++) for (int j = -Mathf.RoundToInt(radius.y); j <= Mathf.RoundToInt(radius.y); j++)
        {
            Vector3 snappedPosition = new Vector3(i + 0.5f, j + 0.5f, 0);
            Instantiate(blockPlacer.GetPrefabFromBlockType(BlockType.Water), snappedPosition, Quaternion.identity);
        }
    }
}
