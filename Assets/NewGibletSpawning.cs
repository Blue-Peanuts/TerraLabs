using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGibletSpawning : MonoBehaviour
{
    [SerializeField] private GameObject gibletPrefab;
    [SerializeField] private Slider redPigment;
    [SerializeField] private Slider greenPigment;
    [SerializeField] private Slider bluePigment;
    [SerializeField] private Slider aggressiveness;
    [SerializeField] private Slider adaptability;

    private bool randomGene = false;

    public void SetRandomGene(bool boo)
    {
        randomGene = boo;
    }
    private void Update()
    {
        
        if (Utility.IsButtonPushedOnNonUI(KeyCode.Mouse0))
        {
            if (Utility.GetBlock())
            {
                GameObject giblet = Instantiate(gibletPrefab, Utility.GetBlock().transform.position,
                    Quaternion.identity);
                GibletBlueprint blueprint = giblet.GetComponent<GibletBlueprint>();
                blueprint.genetics = new Genetics();
                blueprint.genetics.Randomize();
                blueprint.genetics.redColor.Value = redPigment.value;
                blueprint.genetics.greenColor.Value = greenPigment.value;
                blueprint.genetics.blueColor.Value = bluePigment.value;
                blueprint.genetics.aggressiveness.Value = aggressiveness.value;
                blueprint.genetics.adaptability.Value = adaptability.value;
                
                if(randomGene)
                    blueprint.genetics.Randomize();
                
                Overseer.Instance.GetComponent<Energy>().Give(50, giblet.GetComponent<Energy>());
            }
        }
        if (Utility.IsButtonPushedOnNonUI(KeyCode.Mouse1))
        {
            
        }
    }
}
