using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    public void SetScale(float scale)
    {
        Time.timeScale = scale;
    }
}
