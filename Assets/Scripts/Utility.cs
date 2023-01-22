using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Utility : MonoBehaviour
{
    public static List<GameObject> GetTaggedGameObjectsWithinRaycast2D(string tag, GameObject startObject, GameObject endObject)
    {
        // Create a list to store the tagged GameObjects that are within the raycast
        List<GameObject> objectsWithinRaycast = new List<GameObject>();

        // Get the starting position and ending position of the raycast
        Vector2 startPos = startObject.transform.position;
        Vector2 endPos = endObject.transform.position;

        // Perform the 2D raycast
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPos, endPos - startPos, Vector3.Distance(endPos, startPos));

        // Iterate through the hits to find the tagged GameObjects that are within the raycast
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag(tag))
            {
                objectsWithinRaycast.Add(hitObject);
            }
        }

        // Return the list of tagged GameObjects that are within the raycast
        return objectsWithinRaycast;
    }
    
    public static GameObject FindNearestTaggedObject(string tag, Vector2 position, float maxDistance)
    {

        // Get a list of all GameObjects with the matching tag within the max distance
        List<GameObject> objects = FindNearbyTaggedObjects(position, maxDistance, tag);

        // If no objects were found, return null
        if (objects.Count == 0)
        {
            return null;
        }

        // Assume the first object in the list is the nearest
        GameObject nearestObject = objects[0];
        float nearestDistance = Vector2.Distance(nearestObject.transform.position, position);

        // Iterate through the rest of the objects to find the nearest one
        for (int i = 1; i < objects.Count; i++)
        {
            float distance = Vector2.Distance(objects[i].transform.position, position);

            if (distance < nearestDistance)
            {
                nearestObject = objects[i];
                nearestDistance = distance;
            }
        }

        // Return the nearest object
        return nearestObject;
    }
    
    public static List<GameObject> FindNearbyTaggedObjects(Vector2 position, float distance, string tag)
    {
        // Use Physics2D.OverlapCircleAll to get all colliders within distance of position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, distance);

        // Create list to store game objects with matching tag
        var gameObjects = new List<GameObject>();

        // Iterate through colliders
        foreach (Collider2D collider in colliders)
        {
            // Check if collider's game object has the tag we're looking for
            if (collider.gameObject.CompareTag(tag))
            {
                // Add game object to list
                gameObjects.Add(collider.gameObject);
            }
        }

        // Return list of game objects
        return gameObjects;
    }
    
    public static bool IsButtonPushedOnNonUI(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            // Return the reverse of if the eventSystem exists, or that your mouse is over an object
            // This should ensure you don't run over a UI and can test without an event system in play
            return !(EventSystem.current && EventSystem.current.IsPointerOverGameObject());
        }
        // Mouse button is not clicked
        return false;
    }
    public static bool IsButtonHeldOnNonUI(KeyCode keyCode)
    {
        if (Input.GetKey(keyCode))
        {
            // Return the reverse of if the eventSystem exists, or that your mouse is over an object
            // This should ensure you don't run over a UI and can test without an event system in play
            return !(EventSystem.current && EventSystem.current.IsPointerOverGameObject());
        }
        // Mouse button is not clicked
        return false;
    }

    public static Block GetBlock()
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get world position
        float xFloored = Mathf.Floor(mousePosition.x);
        float yFloored = Mathf.Floor(mousePosition.y);
        Vector3 snappedPosition = new Vector3(xFloored + 0.5f, yFloored + 0.5f, 0);
        GameObject blockObject = Utility.FindNearestTaggedObject("Block", snappedPosition, 0.1f);

        if (!blockObject)
            return null;
        
        return blockObject.GetComponent<Block>();
    }
}
