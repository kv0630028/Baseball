using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HomeRunGameManager;

public class HitCoin : MonoBehaviour
{
    public Counting score;
    public FeelEffect feelEffect;
    public ScoreFont scoreFont;
    int currentCoin;

    [Button]
    public void SetCoin(eHIT_TYPE hitType)
    {
        int coin = 0;
        switch (hitType)
        {
            case eHIT_TYPE.BASE_1:
                coin += 100;
                break;
            case eHIT_TYPE.BASE_2:
                coin += 200;
                break;
            case eHIT_TYPE.BASE_3:
                coin += 1000;
                break;
            case eHIT_TYPE.HOMERUN:
                coin += 5000;
                break;
            default:
                break;
        }
        currentCoin += coin;
        scoreFont.CreateScore(hitType, coin);
        feelEffect.CoinFeel();
        score.CountingPlay(coin);
        LobbyManager.Instance.GameCoin += coin;
    }
}