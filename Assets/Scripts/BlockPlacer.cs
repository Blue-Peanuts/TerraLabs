using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [System.Serializable]
    private struct BlockTypePrefabPair
    {
        public BlockType Type;
        public GameObject Prefab;
    }
    
    [SerializeField] private BlockTypePrefabPair[] blockPrefabs; // the prefabs of selected tile
    public BlockType selectedBlock; // which tile are you currently using
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition); // get world position
        float xFloored = Mathf.Floor(mousePosition.x);
        float yFloored = Mathf.Floor(mousePosition.y);
        Vector3 snappedPosition = new Vector3(xFloored + 0.5f, yFloored + 0.5f, 0);

        if (Utility.IsButtonHeldOnNonUI(KeyCode.Mouse0))
        {
            GameObject nearestObject = Utility.FindNearestTaggedObject("Block", snappedPosition, 
                0.1f);
            if(nearestObject != null)
            {
                // destroy overlapping
                Destroy(nearestObject);
            }
            Instantiate(GetPrefabFromBlockType(selectedBlock), snappedPosition, Quaternion.identity);
        }
        if (Utility.IsButtonHeldOnNonUI(KeyCode.Mouse1))
        {
            GameObject nearestObject = Utility.FindNearestTaggedObject("Block", snappedPosition, 
                0.1f);
            if(nearestObject != null)
            {
                // destroy overlapping
                Destroy(nearestObject);
            }
            Instantiate(GetPrefabFromBlockType(0), snappedPosition, Quaternion.identity);
        }
    }

    GameObject GetPrefabFromBlockType(BlockType type)
    {
        foreach (BlockTypePrefabPair pair in blockPrefabs)
        {
            if (pair.Type == type)
            {
                return pair.Prefab;
            }
        }

        return null;
    }
}
