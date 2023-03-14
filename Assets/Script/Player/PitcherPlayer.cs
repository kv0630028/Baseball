using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherPlayer : MonoBehaviour
{
    private Vector3 ballOriPos;
    public Animator animator;
    public Transform strikePos;
    public Transform rightHand;
    public GameObject ball;
    public FeelEffect feelEffect;

    public bool IsPitchingBall = false;

    System.Action finishBallCall;

    public void SetFinishBallCallBack(System.Action _finishBallCall)
    {
        finishBallCall = _finishBallCall;
    }
    void Start()
    {
        ballOriPos = ball.transform.localPosition;
        ball.transform.DOLocalRotate(new Vector3(180, 0, 0), 0.25f).SetLoops(-1);
    }

    public void Ready()
    {
        animator.SetBool("ReadyToPitch", true);
    }

    public void Finish()
    {
        IsPitchingBall = false;
        animator.SetBool("ReadyToPitch", false);
    }
    public void ReadyToPitch()
    {
        IsPitchingBall = false;
        ball.transform.parent = rightHand;
        ball.transform.localPosition = ballOriPos;
        ball.gameObject.SetActive(false);
    }
    //´øÁü
    public void PitchBall()
    {
        feelEffect.PitcherFeel();

        IsPitchingBall = true;
        ball.gameObject.SetActive(true);
        ball.transform.parent = strikePos;
        float rndTime = Random.Range(0.7f, 0.8f);
        ball.transform.DOLocalMove(Vector3.zero, rndTime).OnComplete(finishBallCall.Invoke);        
    }

    public void HitBall()
    {
        ball.gameObject.SetActive(false);
    }
}
