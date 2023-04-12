using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlay : MonoBehaviour
{
    public Button btn;
    public bool isAuto = false;
    
    void Start()
    {
        
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (isAuto == true && other.name == "Ball")
        {         
            HomeRunGameManager.swing();
        }        
    }   

  

    public void AutoBtn()
    {
        ColorBlock colorBlock = btn.colors;
        isAuto = !isAuto;
        if (isAuto == true)
        {            
            colorBlock.normalColor = new Color(0f, 1f, 0f, 1f);
            colorBlock.highlightedColor = new Color(0f, 1f, 0f, 1f);
            colorBlock.selectedColor = new Color(0f, 1f, 0f, 1f);
            btn.colors = colorBlock;
        }
        else
        {
            colorBlock.normalColor = new Color(1f, 1f, 1f, 1f);
            colorBlock.highlightedColor = new Color(1f, 1f, 1f, 1f);
            colorBlock.selectedColor = new Color(1f, 1f, 1f, 1f);
            btn.colors = colorBlock;
        }

    }

}
