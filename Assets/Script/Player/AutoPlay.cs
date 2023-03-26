using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    public bool isAuto = false;
    
    void Update()
    {
        Check();        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Ãæµ¹");
    }



    void Check()
    {
        if (isAuto == true)
        {
            HomeRunGameManager.swing();
        }

    }

    public void AutoBtn()
    {
        isAuto = !isAuto;
    }

}
