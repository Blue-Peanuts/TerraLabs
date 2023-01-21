using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibletBlueprint : MonoBehaviour
{
    public Genetics genetics;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        _spriteRenderer.color = 
            new Color(
                (genetics.redColor.Value + 3) / 4,
                (genetics.greenColor.Value + 3) / 4,
                (genetics.blueColor.Value + 3) / 4
                );
    }
}
