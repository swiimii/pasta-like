using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCanvasController : MonoBehaviour
{
    // Switch from canvas 1 to canvas 2
    public GameObject[] menus;
    public void SwitchCanvas(int can)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if (i == can)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }
}
