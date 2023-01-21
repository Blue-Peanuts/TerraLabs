using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPtext : MonoBehaviour
{
    public TextMeshProUGUI description;
    public FocusedGiblet focusedGiblet;// Start is called before the first frame update
    void Start()
    {
        description = FindObjectOfType<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (focusedGiblet == null)
            return;
        Genetics genetics = (Genetics)focusedGiblet.GetGenetics();
        description.text = "ADAPTABILITY:" + genetics.Adaptability;
        Debug.Log("bruh");
    }
}
