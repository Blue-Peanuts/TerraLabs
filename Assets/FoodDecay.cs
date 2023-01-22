using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDecay : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Exhaust());
    }

    IEnumerator Exhaust()
    {
        while (true)
        {
            GetComponent<Energy>().Give(1, Overseer.Instance.GetComponent<Energy>());
            yield return new WaitForSeconds(3);
        }
    }
}
