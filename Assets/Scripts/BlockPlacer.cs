using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    public void SetBlock(int type)
    {
        selectedBlock = (BlockType)type;
    }
    void Update()
    {
        if (Utility.IsButtonHeldOnNonUI(KeyCode.Mouse0))
        {
            Place(_camera.ScreenToWorldPoint(Input.mousePosition), selectedBlock);
            Place(_camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.up, selectedBlock);
            Place(_camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.down, selectedBlock);
            Place(_camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.left, selectedBlock);
            Place(_camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.right, selectedBlock);
        }
        if (Utility.IsButtonHeldOnNonUI(KeyCode.Mouse1))
        {
            Place(_camera.ScreenToWorldPoint(Input.mousePosition), 0);
            Place(_camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.up, 0);
            Place(_camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.down, 0);
            Place(_camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.left, 0);
            Place(_camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.right, 0);
        }
    }

    void Place(Vector3 mousePosition, BlockType blockType)
    {
        float xFloored = Mathf.Floor(mousePosition.x);
        float yFloored = Mathf.Floor(mousePosition.y);
        Vector3 snappedPosition = new Vector3(xFloored + 0.5f, yFloored + 0.5f, 0);
        GameObject nearestObject = Utility.FindNearestTaggedObject("Block", snappedPosition,
            
            0.1f);
        if(!nearestObject)
        {
            return;
        }
        Destroy(nearestObject);
        Instantiate(GetPrefabFromBlockType(blockType), snappedPosition, Quaternion.identity);

            
        foreach(GameObject blockObject in Utility.FindNearbyTaggedObjects(snappedPosition, 
                    2, "Block"))
        {
            foreach (Transform child in blockObject.transform)
            {
                child.GetComponent<BlockSmoother>().UpdateSmoother();
            }
        }
    }

    public GameObject GetPrefabFromBlockType(BlockType type)
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
