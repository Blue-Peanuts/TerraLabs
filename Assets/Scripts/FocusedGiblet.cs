using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedGiblet : MonoBehaviour
{
    public GameObject focusedGiblet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Utility.IsButtonPushedOnNonUI(KeyCode.Mouse0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            focusedGiblet = Utility.FindNearestTaggedObject("Giblet", mousePos, 0.1f);

        }
    }

    public Genetics GetGenetics()
    {
        return focusedGiblet.GetComponent<GibletBlueprint>().genetics;
    }
}
