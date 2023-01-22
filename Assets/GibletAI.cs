using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class GibletAI : MonoBehaviour
{
    private const int ViewRange = 4;

    private Genetics Genetics => GetComponent<GibletBlueprint>().genetics;
    private Vector3 _wanderDirection;
    private Rigidbody2D _rigidbody2D;

    private float Speed => 1;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RandomWander());
        StartCoroutine(Bite());
        StartCoroutine(Exhaust());
    }

    IEnumerator Exhaust()
    {
        while (true)
        {
            GetComponent<Energy>().Give(1, Overseer.Instance.GetComponent<Energy>());
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator RandomWander()
    {
        while (true)
        {
            _wanderDirection += (Vector3)Random.insideUnitCircle * 0.5f;
            _wanderDirection = _wanderDirection.normalized;
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }
    }
    IEnumerator Bite()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
            List<GameObject> tempGiblets = Utility.FindNearbyTaggedObjects(transform.position, 1, "Giblet");

            foreach (var gib in tempGiblets)
            {
                Color gibColor = gib.GetComponent<SpriteRenderer>().color;
                Color myColor = GetComponent<SpriteRenderer>().color;
                float colorDelta = 0;

                colorDelta = Mathf.Max(Mathf.Abs(gibColor.r - myColor.r), Mathf.Abs(gibColor.g - myColor.g), Mathf.Abs(gibColor.b - myColor.b));

                if (colorDelta > SpeciesColorDelta)
                {
                    GetComponent<Energy>().Drain((int)Mathf.Max(0, 10 * GetComponent<GibletBlueprint>().genetics.aggressiveness.Value + 2), gib.GetComponent<Energy>());
                    break;
                }
            }
            
            
        }
    }

    private const float SpeciesColorDelta = 0.3f;

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = DecideDirection() * Speed + _wanderDirection * 0.25f;
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
            Color gibColor = gib.GetComponent<SpriteRenderer>().color;
            Color myColor = GetComponent<SpriteRenderer>().color;
            float colorDelta = 0;
            colorDelta = Mathf.Max(Mathf.Abs(gibColor.r - myColor.r), Mathf.Abs(gibColor.g - myColor.g), Mathf.Abs(gibColor.b - myColor.b));
            
            if(colorDelta > SpeciesColorDelta)
                enemies.Add(gib);
        }

        foreach (var obj in foods)
        {
            finalDirection += ((ViewRange - GetWeightedDistance(obj)) * ((obj.transform.position - transform.position).normalized) * (-(Genetics.aggressiveness.Value - 0.3f)/1.3f));
        }
        foreach (var obj in enemies)
        {
            finalDirection += ((ViewRange - GetWeightedDistance(obj)) * ((obj.transform.position - transform.position).normalized) * (Genetics.aggressiveness.Value + 0.1f)/1.3f);
        }

        return finalDirection.magnitude > 1 ? finalDirection.normalized : finalDirection;
    }

    float GetWeightedDistance(GameObject obj)
    {
        if (Utility.GetTaggedGameObjectsWithinRaycast2D("Block", gameObject, obj).Count == 0)
            return 0;
        
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
