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
        if (isAuto == true)
        {
            if (other.name == "Ball")
            {
                HomeRunGameManager.swing();
            }
        }
        //Debug.Log("Trigger Ãæµ¹");
    }   

  

    public void AutoBtn()
    {
        isAuto = !isAuto;
    }

}
