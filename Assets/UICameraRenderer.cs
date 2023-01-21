using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICameraRenderer : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private RawImage _rawImage;
    private RenderTexture _renderTexture;
    void Start()
    {
        _rawImage = GetComponent<RawImage>();
        _renderTexture = new RenderTexture(256, 256, 24);
        camera.targetTexture = _renderTexture;
        _rawImage.texture = _renderTexture;
    }
}
