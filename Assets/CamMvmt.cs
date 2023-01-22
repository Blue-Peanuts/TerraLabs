using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMvmt : MonoBehaviour
{
    Vector3 movement;
    public float sensitivity = 10f;


    // Start is called before the first frame update
    void Start()
    {
        fov = Camera.main.orthographicSize;
    }

    float fov;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, 5, 50);
        Camera.main.orthographicSize = fov;
    }

    private void FixedUpdate()
    {
        
        transform.position = transform.position + movement * fov * Time.fixedUnscaledDeltaTime;
    }
}
