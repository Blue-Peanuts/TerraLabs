using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BlockSmoother : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _parentSpriteRenderer;
    private Color ParentColor => _parentSpriteRenderer.color;

    private Block _parentBlock;

    private Vector3 Offset
    {
        get
        {
            Vector3 result = Vector3.zero;
            if (transform.localPosition.x > 0)
            {
                result.x += 1;
            }
            else
                result.x -= 1;
            if (transform.localPosition.y > 0)
            {
                result.y += 1;
            }
            else
                result.y -= 1;

            return result;
        }
    }
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var parent = transform.parent;
        _parentSpriteRenderer = parent.GetComponent<SpriteRenderer>();
        _parentBlock = parent.GetComponent<Block>();
        StartCoroutine(DelaySmoother());
    }

    IEnumerator DelaySmoother()
    {
        yield return new WaitForEndOfFrame();
        UpdateSmoother();
    }

    
    public void UpdateSmoother()
    {
        _spriteRenderer.color = new Color(0,0,0,0);
        
        int xLayerCompare = LayerCompare(new Vector2(Offset.x, 0));
        int yLayerCompare = LayerCompare(new Vector2(0, Offset.y));
        int diaLayerCompare = LayerCompare(new Vector2(Offset.x, Offset.y));
        
        Block xBlock = GetAdjacentBlock(new Vector2(Offset.x, 0));
        Block yBlock = GetAdjacentBlock(new Vector2(0, Offset.y));
        Block diaBlock = GetAdjacentBlock(new Vector2(Offset.x, Offset.y));
    
        if (xLayerCompare == 0 || yLayerCompare == 0)
        {
            _spriteRenderer.color = ParentColor;
        }
        else if (xBlock && yBlock && diaBlock && xLayerCompare == yLayerCompare && yLayerCompare == diaLayerCompare)
        {
            _spriteRenderer.color = xBlock.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            int maxLayerCompare = Mathf.Max(xLayerCompare, yLayerCompare, diaLayerCompare, 0);
            if (maxLayerCompare == 0)
                _spriteRenderer.color = _parentSpriteRenderer.color;
            if (xBlock && maxLayerCompare == xLayerCompare)
                _spriteRenderer.color = xBlock.GetComponent<SpriteRenderer>().color;
            if (yBlock && maxLayerCompare == yLayerCompare)
                _spriteRenderer.color = yBlock.GetComponent<SpriteRenderer>().color;
            if (diaBlock && maxLayerCompare == diaLayerCompare)
                _spriteRenderer.color = diaBlock.GetComponent<SpriteRenderer>().color;
        }
    }

    private int LayerCompare(Vector2 direction)
    {
        if(GetAdjacentBlock(direction))
            return _parentSpriteRenderer.sortingOrder - GetAdjacentBlock(direction).GetComponent<SpriteRenderer>().sortingOrder;
        return 1;
    }

    private Block GetAdjacentBlock(Vector2 direction)
    {
        var position = transform.parent.position;
        GameObject blockObject = Utility.FindNearestTaggedObject("Block",
            (Vector3)direction + new Vector3(Mathf.Floor(position.x) + 0.5f,
                Mathf.Floor(position.y) + 0.5f, 0), 0.1f);
        if(blockObject)
            return blockObject.GetComponent<Block>();
        return null;
    }
}
