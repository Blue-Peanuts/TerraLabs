using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Utility : MonoBehaviour
{
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
            // Check if the mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Mouse is over a UI element
                return false;
            }
            else
            {
                // Mouse is not over a UI element
                return true;
            }
        }
        else
        {
            // Mouse button is not clicked
            return false;
        }
    }

}
