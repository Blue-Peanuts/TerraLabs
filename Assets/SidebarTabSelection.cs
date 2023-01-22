using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SidebarTabSelection : MonoBehaviour
{
    [SerializeField] private Image[] borders;
    [SerializeField] private GameObject[] menu;

    private int _currentTab = 0;
    
    private void Awake()
    {
        SetTab(0);
    }

    public void SetTab(int tab)
    {
        _currentTab = tab;
        foreach (Image i in borders)
        {
            i.enabled = false;
        }
        foreach (GameObject g in menu)
        {
            g.SetActive(false);
        }
        borders[_currentTab].enabled = true;
        menu[_currentTab].SetActive(true);


    }
}
