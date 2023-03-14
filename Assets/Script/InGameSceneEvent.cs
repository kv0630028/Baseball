using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneEvent : SceneChangeEvnet
{
    public override void Fade_ButtonTouch()
    {
        base.Fade_ButtonTouch(() =>
        {
            HomeRunGameManager.Instance.StopBall();
            sceneChange.Change(SceneChange.eSCENE_TYPE.LOBBY);
        });
    }
}
