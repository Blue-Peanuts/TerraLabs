using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Gene
{
    private float _value;
    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            _value = Mathf.Clamp(_value, -1, 1);
        }
    }

    public void Mutate()
    {
        float delta = Random.Range(-0.1f, 0.1f);
        _value += Mathf.Clamp(_value + delta, -1, 1);
    }

    public void Randomize()
    {
        _value = Random.Range(-1f, 1f);
    }
}
