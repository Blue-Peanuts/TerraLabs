using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeTeleport : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (!Utility.FindNearestTaggedObject("Block", transform.position, 0.2f))
        {
            transform.position = -transform.position;
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, 3);
        }
    }
}
