using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HomeRunGameManager;

public class FeelEffect : MonoBehaviour
{
    [Header("[1��Ÿ]")]
    [SerializeReference]
    private MMFeedbacks base1_Feel_1;

    [Header("[2��Ÿ]")]
    [SerializeReference]
    private MMFeedbacks base2_Feel_1;

    [Header("[3��Ÿ]")]
    [SerializeReference]
    private MMFeedbacks base3_Feel_1;
    [SerializeReference]
    private MMFeedbacks base3_Feel_2;

    [Header("[Ȩ��]")]
    [SerializeReference]
    private MMFeedbacks homerun_Feel_1;
    [SerializeReference]
    private MMFeedbacks homerun_Feel_2;

    [Header("[��Ʈ����ũ]")]
    [SerializeReference]
    private MMFeedbacks strike;

    [Header("[������������Ʈ]")]
    [SerializeReference]
    private MMFeedbacks pitcherFeel;

    [Header("[1,2,3��Ÿ,��Ʈ����ũ �̹���]")]
    [SerializeReference]
    private MMFeedbacks baseHitImageFeel;

    [Header("[Ȩ�� �̹���]")]
    [SerializeReference]
    private MMFeedbacks homeRunHitImageFeel;

    [Header("[��Ʈ����ũ �̹���]")]
    [SerializeReference]
    private MMFeedbacks strikeImage;

    [Header("[����]")]
    [SerializeReference]
    private MMFeedbacks coinFeel;

    [Header("[���̵�]")]
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
