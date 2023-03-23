using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    public bool isAuto = false;
    
    void Update()
    {
        
    }

    void Check()
    {
        HomeRunGameManager.swing();
    }

    public void AutoBtn()
    {
        isAuto = !isAuto;
    }

}
