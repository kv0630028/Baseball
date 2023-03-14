using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : SingletonComponent<GUIManager>
{
    public TOPInfoController topController;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        topController.UpdateInfo();
    }
}
