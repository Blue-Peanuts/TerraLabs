using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction : MonoBehaviour
{
    public int Least2Born = 90;
    public int BornConsumption = 40;
    public GameObject Giblet;
    public GameObject Egg;
    public Rigidbody2D rb;
    void LayEgg(int Energy)
    {
        if(Energy >= Least2Born)
        {
            Energy -= BornConsumption;
            Genetics Genetics = Giblet.GetComponent<Genetics>();
            Genetics.Mutate();
            /* Egg // assign the genetics to the egg
            Instantiate(Egg, rb.position, Quaternion.identity); // spawn the egg */
        }
    }
}
