using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TailWingScaler : MonoBehaviour
{
    private GameObject tail;
    private GameObject wing;
    private GameObject horn;

    private void Start()
    {
        tail = transform.Find("Tail").gameObject;
        wing = transform.Find("Wings").gameObject;
        horn = transform.Find("Horn").gameObject;
    }

    private void Update()
    {
        if(GetComponent<GibletBlueprint>().genetics.adaptability.Value > 0)
            wing.transform.localScale = Vector3.one * GetComponent<GibletBlueprint>().genetics.adaptability.Value;
        if(GetComponent<GibletBlueprint>().genetics.adaptability.Value < 0)
            tail.transform.localScale = Vector3.one * -GetComponent<GibletBlueprint>().genetics.adaptability.Value;
        if(GetComponent<GibletBlueprint>().genetics.aggressiveness.Value > 0)
            horn.transform.localScale = Vector3.one * GetComponent<GibletBlueprint>().genetics.aggressiveness.Value;
    }
}
