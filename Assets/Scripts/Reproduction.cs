using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction : MonoBehaviour
{
    int Least2Born = 90;
    int BornConsumption = 40;
    public GameObject gibletPrefab;
    private Energy _energy;
    //public float MaxAllowedColorDistinction = 0.8f;
    //public float MaxAllowedGeneticsDistinction = 0.5f;

    private void Awake()
    {
        _energy = GetComponent<Energy>();
    }

    void Update()
    {
        if (_energy.energyLevel >= Least2Born) //
        {
            GiveBirth();
        }
    }

    void GiveBirth()
    {
        Genetics Genetics = GetComponent<GibletBlueprint>().genetics;
        Genetics.Mutate();
    
        GameObject egg = Instantiate(Overseer.Instance.gibletPrefab, transform.position, Quaternion.identity);
        _energy.Give(BornConsumption, egg.GetComponent<Energy>());
        egg.GetComponent<GibletBlueprint>().genetics = Genetics;
    }
}
