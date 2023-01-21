using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public GameObject[] tilePrefabs; // the prefabs of selected tile
    public int selectedTile = 0; // which tile are you currently using

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get world position
            float Xfloored = Mathf.Floor(mousePosition.x);
            float Yfloored = Mathf.Floor(mousePosition.y);
            Vector3 snappedPosition = new Vector3(Xfloored + 0.5f, Yfloored + 0.5f, 0);

            GameObject[] Prefabs = GameObject.FindGameObjectsWithTag("Tiles");
            for (int i=0; i<Prefabs.Length; ++i)
            {
                if (Prefabs[i].transform.position == snappedPosition) Destroy(Prefabs[i]);
            }
        }
    }
}
