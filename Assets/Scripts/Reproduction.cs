using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction : MonoBehaviour
{
    public int Least2Born = 90;
    public int BornConsumption = 40;
    public GameObject gibletPrefab;
    public GameObject eggPrefab;
    private Energy _energy;
    //public float MaxAllowedColorDistinction = 0.8f;
    //public float MaxAllowedGeneticsDistinction = 0.5f;

    private void Awake()
    {
        _energy = GetComponent<Energy>();
    }

    void Update()
    {
        float xFloored = Mathf.Floor(transform.position.x);
        float yFloored = Mathf.Floor(transform.position.y);
        //float distance = Vector3.Distance(transform.position, Egg.transform.position); // need to detect whether there are things in range, more than one eggs
        if (_energy.energyLevel >= Least2Born) //
        {
            LayEgg();
        }
        //if () { //detectes whether there are eggs nearby

            /* Genetics Genetics = Giblet.GetComponent<Genetics>();
            Genetics EggGene = Egg.GetComponent<Genetics>();
            bool[] Similarity = {(abs(Genetics.redColor.Value - EggGene.redColor.Value) <= MaxAllowedColorDistinction),
                             (abs(Genetics.greenColor.Value - EggGene.greenColor.Value) <= MaxAllowedColorDistinction),
                             (abs(Genetics.blueColor.Value - EggGene.blueColor.Value) <= MaxAllowedColorDistinction),
                             (abs(Genetics.Adaptability.Value - EggGene.Adaptability.Value) <= MaxAllowedGeneticsDistinction),
                             (abs(Genetics.Aggressiveness.Value - EggGene.Aggressiveness.Value) <= MaxAllowedGeneticsDistinction)};
            if (Similarity == {true, true, true, true, true})Fertilize(); */
        //}
    }

    void LayEgg()
    {
        Genetics Genetics = gibletPrefab.GetComponent<GibletBlueprint>().genetics;
        Genetics.Mutate();

        GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
        _energy.Give(BornConsumption, egg.GetComponent<Energy>());
        egg.GetComponent<GibletBlueprint>().genetics = Genetics;
    }

    void Fertilize()
    {
    }

    float abs(float a)
    {
        if (a >= 0) return a;
        return -a;
    }
}
