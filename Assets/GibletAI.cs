using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class GibletAI : MonoBehaviour
{
    private const int ViewRange = 6;

    private Genetics Genetics => GetComponent<GibletBlueprint>().genetics;
    private Vector3 _wanderDirection;
    private Rigidbody2D _rigidbody2D;

    private float Speed => 1;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        _rigidbody2D.velocity = DecideDirection() * Speed;
    }
    Vector3 DecideDirection()
    {
        Vector3 finalDirection = new Vector3();
        List<GameObject> foods = Utility.FindNearbyTaggedObjects(transform.position, ViewRange, "Food");
        List<GameObject> blocks = Utility.FindNearbyTaggedObjects(transform.position, ViewRange, "Block");
        List<GameObject> tempGiblets = Utility.FindNearbyTaggedObjects(transform.position, ViewRange, "Giblet");
        List<GameObject> enemies = new List<GameObject>();

        foreach (var gib in tempGiblets)
        {
            if(gib.GetComponent<GibletBlueprint>().genetics.species != Genetics.species)
                enemies.Add(gib);
        }

        foreach (var obj in foods)
        {
            finalDirection += ((ViewRange - GetWeightedDistance(obj)) * ((obj.transform.position - transform.position).normalized) * (-Genetics.aggressiveness.Value));
        }
        foreach (var obj in enemies)
        {
            finalDirection += ((ViewRange - GetWeightedDistance(obj)) * ((obj.transform.position - transform.position).normalized) * (Genetics.aggressiveness.Value));
        }

        return finalDirection;
    }

    float GetWeightedDistance(GameObject obj)
    {
        float waterAdapt = (1 - Genetics.adaptability.Value) / 2;
        float groundAdapt = (1 - Mathf.Abs(Genetics.adaptability.Value));
        float hillAdapt = (Genetics.adaptability.Value + 1) / 2;

        float weightedDistance = 0;

        foreach (var block in Utility.GetTaggedGameObjectsWithinRaycast2D("Block",gameObject, obj))
        {
            BlockType blockType = block.GetComponent<Block>().Type;
            if (blockType == BlockType.Plains)
                weightedDistance += (1f - groundAdapt);
            if (blockType == BlockType.Water)
                weightedDistance += (1f - waterAdapt);
            if (blockType == BlockType.Hill)
                weightedDistance += (1f - hillAdapt);
        }
        return weightedDistance / Utility.GetTaggedGameObjectsWithinRaycast2D("Block",gameObject, obj).Count * Vector3.Distance(obj.transform.position, transform.position);
    }

    
}
