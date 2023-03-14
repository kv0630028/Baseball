using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static HomeRunGameManager;

public class HitEffect : MonoBehaviour
{
    public Image hitImage;
    public Sprite[] hitSprites;
    public FeelEffect feelEffect;
    bool isEffectPlaying;

    public void ShowHitEffect(eHIT_TYPE hitType)
    {
        StartCoroutine(cShowHitEffect(hitType));
    }
    IEnumerator cShowHitEffect(eHIT_TYPE hitType)
    {
        if (hitType == eHIT_TYPE.HOMERUN)
        {
            SetTimeScale(0.1f);
            slow = true;
        }

        if (isEffectPlaying == false)
        {
            isEffectPlaying = true;
            HitImageSetting(hitType);
            FeelEffectPlay(hitType);

            hitImage.gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            hitImage.gameObject.SetActive(false);
            isEffectPlaying = false;
        }
        else yield break;
    }
    bool slow;
    private void FixedUpdate()
    {
        if (slow) SetTimeScale(Time.timeScale + 0.01f);
    }
    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
        Time.fixedDeltaTime = 0.02f * time;
        if (Time.timeScale >= 0.5f) Time.timeScale = 1;
    }

    void FeelEffectPlay(eHIT_TYPE hitType)
    {
        switch (hitType)
        {
            case eHIT_TYPE.BASE_1:
            case eHIT_TYPE.BASE_2:
            case eHIT_TYPE.BASE_3:
                feelEffect.BaseHitImageFeel();
                feelEffect.HitterFeel(hitType);
                break;
            case eHIT_TYPE.HOMERUN:
                feelEffect.HomeRunHitImageFeel();
                feelEffect.HitterFeel(hitType);
                break;
            case eHIT_TYPE.STRIKE:
                feelEffect.StrikeFeel();
                //feelEffect.HitterFeel(hitType);
                break;
            default:
                break;
        }
    }
    void HitImageSetting(eHIT_TYPE hitType)
    {
        hitImage.sprite = hitSprites[(int)hitType];
        hitImage.SetNativeSize();
    }
}