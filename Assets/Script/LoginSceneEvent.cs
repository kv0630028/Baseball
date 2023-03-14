using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSceneEvent : SceneChangeEvnet
{   
    public LoginController loginWindow;   
    public override void Fade_ButtonTouch()
    {
        Debug.Log("On Button");
        base.Fade_ButtonTouch(() => sceneChange.Change(SceneChange.eSCENE_TYPE.LOBBY));
    }
    
    public void ChangeScene()
    {
        sceneChange.Change(SceneChange.eSCENE_TYPE.LOBBY);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && loginWindow.gameObject.activeSelf == false)
            loginWindow.OpenPopup();
    }
}
