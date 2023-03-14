using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePopup : MonoBehaviour
{
    public enum SHOW_TYPE
    {
        NORMAL,
        SCALE_X,
        SCALE_Y,
    }

    public SHOW_TYPE showType = SHOW_TYPE.NORMAL;
    protected Vector3 startScale;
 
    public virtual void OpenPopup()
    {
        Show();
    }

    public virtual void OpenPopup(string content)
    {
        Show();
    }

    public virtual void OpenPopup(string title, string content)
    {
        Show();
    }

    public virtual void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Show()
    {
        switch (showType)
        {
            case SHOW_TYPE.NORMAL:
                ShowInstance();
                break;
            case SHOW_TYPE.SCALE_X:
                ShowScaleX();
                break;
            case SHOW_TYPE.SCALE_Y:
                ShowScaleY();
                break;
            default:
                break;
        }
    }
    public void ShowInstance()
    {
        gameObject.SetActive(true);
    }

    public void ShowScaleX()
    {
        gameObject.transform.localScale = new Vector3(0, startScale.y);
        gameObject.SetActive(true);
        gameObject.transform.DOScaleX(startScale.x, 0.25f).SetEase(Ease.OutExpo);
    }

    public void ShowScaleY()
    {
        gameObject.transform.localScale = new Vector3(startScale.x, 0);
        gameObject.SetActive(true);
        gameObject.transform.DOScaleY(startScale.y, 0.25f).SetEase(Ease.OutExpo);
    }
}
