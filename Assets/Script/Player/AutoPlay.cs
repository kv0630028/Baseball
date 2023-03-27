using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    public bool isAuto = false;
    
    void Update()
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
        isAuto = !isAuto;
    }

}
