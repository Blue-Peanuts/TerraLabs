using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibletSize : MonoBehaviour
{
    
    void Update()
    {
        transform.localScale = Vector3.one *  (0.1f + 0.9f * GetComponent<Energy>().energyLevel / GetComponent<Energy>().maxLevel);
    }
}
