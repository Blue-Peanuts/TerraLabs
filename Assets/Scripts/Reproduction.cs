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
        GameObject egg = Instantiate(gibletPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        float xFloored = Mathf.Floor(transform.position.x);
        float yFloored = Mathf.Floor(transform.position.y);
        if (_energy.energyLevel >= Least2Born) //
        {
            GiveBirth();
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

    void GiveBirth()
    {
        Genetics Genetics = gibletPrefab.GetComponent<GibletBlueprint>().genetics;
        Genetics.Mutate();

        //GameObject egg = Instantiate(gibletPrefab, transform.position, Quaternion.identity);
        //_energy.Give(BornConsumption, egg.GetComponent<Energy>());
        //egg.GetComponent<GibletBlueprint>().genetics = Genetics;
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
