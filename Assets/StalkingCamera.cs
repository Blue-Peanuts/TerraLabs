using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StalkingCamera : MonoBehaviour
{
    public GameObject stalkSubject;

    private void Update()
    {
        if(stalkSubject)
            transform.position = stalkSubject.transform.position;
    }
}
