using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HomeRunGameManager;

public class FeelEffect : MonoBehaviour
{
    [Header("[1루타]")]
    [SerializeReference]
    private MMFeedbacks base1_Feel_1;

    [Header("[2루타]")]
    [SerializeReference]
    private MMFeedbacks base2_Feel_1;

    [Header("[3루타]")]
    [SerializeReference]
    private MMFeedbacks base3_Feel_1;
    [SerializeReference]
    private MMFeedbacks base3_Feel_2;

    [Header("[홈런]")]
    [SerializeReference]
    private MMFeedbacks homerun_Feel_1;
    [SerializeReference]
    private MMFeedbacks homerun_Feel_2;

    [Header("[스트라이크]")]
    [SerializeReference]
    private MMFeedbacks strike;

    [Header("[투수먼지이펙트]")]
    [SerializeReference]
    private MMFeedbacks pitcherFeel;

    [Header("[1,2,3루타,스트라이크 이미지]")]
    [SerializeReference]
    private MMFeedbacks baseHitImageFeel;

    [Header("[홈런 이미지]")]
    [SerializeReference]
    private MMFeedbacks homeRunHitImageFeel;

    [Header("[스트라이크 이미지]")]
    [SerializeReference]
    private MMFeedbacks strikeImage;

    [Header("[코인]")]
    [SerializeReference]
    private MMFeedbacks coinFeel;

    [Header("[페이드]")]
    [SerializeReference]
    private MMFeedbacks fadeFeel;
    public void FadeFeel()
    {
        fadeFeel.PlayFeedbacks();
    }
    public void CoinFeel()
    {
        coinFeel.PlayFeedbacks();
    }
    public void StrikeFeel()
    {
        strikeImage.PlayFeedbacks();
    }
    public void BaseHitImageFeel()
    {
        baseHitImageFeel.PlayFeedbacks();
    }
    public void HomeRunHitImageFeel()
    {
        homeRunHitImageFeel.PlayFeedbacks();
    }
    public void PitcherFeel()
    {
        pitcherFeel.PlayFeedbacks();
    }

    public void HitterFeel(eHIT_TYPE hitType)
    {
        switch (hitType)
        {
            case eHIT_TYPE.BASE_1:
                base1_Feel_1.PlayFeedbacks();
                break;
            case eHIT_TYPE.BASE_2:
                base2_Feel_1.PlayFeedbacks();
                break;
            case eHIT_TYPE.BASE_3:
                base3_Feel_1.PlayFeedbacks();
                base3_Feel_2.PlayFeedbacks();
                break;
            case eHIT_TYPE.HOMERUN:
                homerun_Feel_1.PlayFeedbacks();
                homerun_Feel_2.PlayFeedbacks();
                break;
            case eHIT_TYPE.STRIKE:
                //strike.PlayFeedbacks();
                break;
            default:
                break;
        }
    }
}
