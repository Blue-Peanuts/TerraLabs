using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoTest : MonoBehaviour
{
    public Energy blob1;
    public Energy blob2;

    // Start is called before the first frame update
    void Start()
    {
        blob1.Drain(5, blob2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
