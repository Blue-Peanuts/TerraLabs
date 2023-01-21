using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public GameObject[] tilePrefabs; // the prefabs of selected tile
    public int selectedTile = 1; // which tile are you currently using

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get world position
        float xFloored = Mathf.Floor(mousePosition.x);
        float yFloored = Mathf.Floor(mousePosition.y);
        Vector3 snappedPosition = new Vector3(xFloored + 0.5f, yFloored + 0.5f, 0);

        if (Utility.IsButtonPushedOnNonUI(KeyCode.Mouse0))
        {
            GameObject nearestObject = Utility.FindNearestTaggedObject("Tiles", snappedPosition, 1f);
            if(nearestObject != null)
            {
                // destroy overlapping
                Destroy(nearestObject);
            }
            Instantiate(tilePrefabs[selectedTile], snappedPosition, Quaternion.identity);
        }
        if (Utility.IsButtonPushedOnNonUI(KeyCode.Mouse0))
        {
            GameObject nearestObject = Utility.FindNearestTaggedObject("Tiles", snappedPosition, 1f);
            if(nearestObject != null)
            {
                // destroy overlapping
                Destroy(nearestObject);
            }
        }
    }
}
