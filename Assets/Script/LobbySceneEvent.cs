using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LobbySceneEvent : SceneChangeEvnet
{
    public override void Fade_ButtonTouch()
    {
        base.Fade_ButtonTouch(() => sceneChange.Change(SceneChange.eSCENE_TYPE.INGAME));
    }
}
