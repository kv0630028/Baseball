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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger �浹");
    }


    public void AutoBtn()
    {
        isAuto = !isAuto;
    }

}
