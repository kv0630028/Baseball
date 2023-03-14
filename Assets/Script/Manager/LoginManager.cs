using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : SingletonComponent<LoginManager>
{
    private void Awake()
    {
        SetInstance();
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("");
        }
    }
}
