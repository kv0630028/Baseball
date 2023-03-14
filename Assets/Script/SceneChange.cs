using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public enum eSCENE_TYPE
    {
        LOGIN,
        LOBBY,
        INGAME,
    }
    [SerializeField]
    private GameObject[] scene;
    eSCENE_TYPE currScene = eSCENE_TYPE.LOGIN;
    [SerializeField]
    private FeelEffect feelEffect;

    private void Start()
    {
        if (scene[(int)eSCENE_TYPE.LOGIN].activeSelf) currScene = eSCENE_TYPE.LOGIN;
        else if (scene[(int)eSCENE_TYPE.LOBBY].activeSelf) currScene = eSCENE_TYPE.LOBBY;
        else if (scene[(int)eSCENE_TYPE.INGAME].activeSelf) currScene = eSCENE_TYPE.INGAME;
    }
    public void Fade()
    {
        feelEffect.FadeFeel();
    }
    public void Change(eSCENE_TYPE type)
    {
        scene[(int)currScene].SetActive(false);

        currScene = type;
        scene[(int)type].SetActive(true);
    }
}