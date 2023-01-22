using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class GibletAI : MonoBehaviour
{
    private const int ViewRange = 5;

    private Genetics Genetics => GetComponent<GibletBlueprint>().genetics;
    private Vector3 _wanderDirection;
    private Rigidbody2D _rigidbody2D;

    private float Speed => 2;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RandomWander());
        StartCoroutine(Bite());
        StartCoroutine(Exhaust());
        StartCoroutine(ChangeTarget());
    }

    IEnumerator Exhaust()
    {
        while (true)
        {
            GetComponent<Energy>().Give((int)(2 * (2f - AdaptMul())), Overseer.Instance.GetComponent<Energy>());
            yield return new WaitForSeconds(2);
        }
    }
    IEnumerator RandomWander()
    {
        while (true)
        {
            _wanderDirection += (Vector3)Random.insideUnitCircle * 0.5f;
            _wanderDirection = _wanderDirection.normalized;
            yield return new WaitForSeconds(Random.Range(0f, 3));
        }
    }

    IEnumerator ChangeTarget()
    {
        
        while (true)
        {
            _rigidbody2D.velocity = DecideDirection() * (Speed * AdaptMul()) + _wanderDirection * 0.1f;
            yield return new WaitForSeconds(Random.Range(0f, 0.1f));
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
                    GetComponent<Energy>().Drain((int)Mathf.Max(0, 3 * GetComponent<GibletBlueprint>().genetics.aggressiveness.Value + 2), gib.GetComponent<Energy>());
                    
                    if(gib)
                        Overseer.Instance.GetComponent<Energy>().Drain((int)Mathf.Max(0, GetComponent<GibletBlueprint>().genetics.aggressiveness.Value + 1), gib.GetComponent<Energy>());
                    break;
                }
            }
            
            
        }
    }

    private const float SpeciesColorDelta = 0.15f;

    private float AdaptMul()
    {
        GameObject nearestBlock = Utility.FindNearestTaggedObject("Block", transform.position, 1);
        if (nearestBlock)
        {
            float adaptMul = 1;
            float waterAdapt = (1 - Genetics.adaptability.Value) / 2;
            float groundAdapt = (1 - Mathf.Abs(Genetics.adaptability.Value));
            float hillAdapt = (Genetics.adaptability.Value + 1) / 2;
            BlockType blockType = nearestBlock.GetComponent<Block>().Type;
            if (blockType == BlockType.Plains)
                adaptMul = (groundAdapt);
            if (blockType == BlockType.Water)
                adaptMul = (waterAdapt);
            if (blockType == BlockType.Hill || blockType == BlockType.Bush)
                adaptMul = (hillAdapt);
            return adaptMul * adaptMul * adaptMul;
        }

        return 1;
    }
    
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().rotation = Mathf.Rad2Deg * Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) - 90;

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
        
        if((Genetics.aggressiveness.Value < 0.3f))
            foreach (var obj in foods)
            {
                Vector3 temp = ((ViewRange - GetWeightedDistance(obj)) * ((obj.transform.position - transform.position).normalized) * (-(Genetics.aggressiveness.Value - 0.3f)/1.3f));
                if (finalDirection.magnitude < temp.magnitude)
                    finalDirection = temp;
            }
        //if((Genetics.aggressiveness.Value > -0.1f))
            foreach (var obj in enemies)
            {
                Vector3 temp = ((ViewRange - GetWeightedDistance(obj)) * ((obj.transform.position - transform.position).normalized) * (Genetics.aggressiveness.Value + 0.1f)/1.3f);
                if (finalDirection.magnitude < temp.magnitude)
                    finalDirection = temp;
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
            if (blockType == BlockType.Hill || blockType == BlockType.Bush)
                weightedDistance += (1f - hillAdapt);
        }
        return weightedDistance / Utility.GetTaggedGameObjectsWithinRaycast2D("Block",gameObject, obj).Count * Vector3.Distance(obj.transform.position, transform.position) +  Vector3.Distance(obj.transform.position, transform.position) * 0.1f;
    }

    
}
