using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    public bool isAuto = false;
    
    void Update()
    {
        if(isAuto == true)
        HomeRunGameManager.swing();
    }

    void Check()
    {
        
    }

    public void AutoBtn()
    {
        isAuto = !isAuto;
    }

}
