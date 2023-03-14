using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeRunGameManager : SingletonComponent<HomeRunGameManager>
{
    public Animator hitterAnimator;

    public Text tDistance;
    public Text tHitTiming;

    public Transform strikePos;
    public PitcherPlayer pitcher;

    public Parabola hitball;
    public ParticleSystem hitParticle;

    public HitEffect hitEffect;
    public HitCoin hitCoin;

    public bool IsPlaying = true;
    public bool isSwing;

    private float distanceFromBallToStrikeZone = 10;

    public enum eHIT_TYPE
    {
        BASE_1,
        BASE_2,
        BASE_3,
        HOMERUN,
        STRIKE,
        NONE
    }
    
    private void Awake()
    {
        SetInstance();
    }
    private void OnEnable()
    {
        Invoke("PlayBall", 0.5f);
    }
    private void Start()
    {
        pitcher.SetFinishBallCallBack(FinishBall);        
    }
    private void Update()
    {
        if (IsPlaying)
        {
            if (TimeManager.instance.PlayTime())
                TimeOver();
        }

        if (pitcher.IsPitchingBall)
        {
            distanceFromBallToStrikeZone = pitcher.ball.transform.position.z - strikePos.position.z;
            tDistance.text = distanceFromBallToStrikeZone.ToString();
        }

        TestKey();
    }

    eHIT_TYPE testType = eHIT_TYPE.NONE;
    void TestKey()
    {
        if (Input.GetKeyDown(KeyCode.A)) testType = eHIT_TYPE.BASE_1;
        if (Input.GetKeyDown(KeyCode.S)) testType = eHIT_TYPE.BASE_2;
        if (Input.GetKeyDown(KeyCode.D)) testType = eHIT_TYPE.BASE_3;
        if (Input.GetKeyDown(KeyCode.F)) testType = eHIT_TYPE.HOMERUN;
    }

    void FinishBall()
    {
        if (IsPlaying)
        {
            if (isSwing) isSwing = false;
            else hitEffect.ShowHitEffect(eHIT_TYPE.STRIKE);
        }            
    }

    public void PlayBall()
    {
        TimeManager.instance.TimerReset();
        IsPlaying = true;
        pitcher.Ready();
    }

    public void StopBall()
    {
        IsPlaying = false;
    }

    public void TimeOver()
    {
        IsPlaying = false;
        pitcher.Finish();
    }
    public void OnSwing()
    {
        if (!IsPlaying) return;
        isSwing = true;
        if (hitterAnimator.GetBool("IsIdle") == false)
        {
            return;
        }

        hitterAnimator.SetTrigger("Swing");
        hitterAnimator.SetBool("IsIdle", false);
    }


    public void CheckHit()
    {
        tHitTiming.text = distanceFromBallToStrikeZone.ToString();
        //Hit
        if (Mathf.Abs(distanceFromBallToStrikeZone) < 0.7f)
        {
            Hit(Mathf.Abs(distanceFromBallToStrikeZone));
        }
        //Miss
        else
        {
            isSwing = false;
        }
    }

    void Hit(float hitValue)
    {
        //hitball.Hit(distanceFromBallToStrikeZone);
        pitcher.HitBall();
        hitParticle.Play();

        eHIT_TYPE hitType;
        if (hitValue <= 0.025) hitType = eHIT_TYPE.HOMERUN;
        else if (hitValue <= 0.125) hitType = eHIT_TYPE.BASE_3;
        else if (hitValue <= 0.25) hitType = eHIT_TYPE.BASE_2;
        else hitType = eHIT_TYPE.BASE_1;

        if (testType != eHIT_TYPE.NONE) hitType = testType;

        hitball.Hit(hitType);
        hitCoin.SetCoin(hitType);
        hitEffect.ShowHitEffect(hitType);
    }
}