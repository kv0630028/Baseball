using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SceneChangeEvnet : MonoBehaviour
{
    protected SceneChange sceneChange;

    [SerializeField]
    protected Button evnetButton;

    private void Start()
    {
        sceneChange = FindObjectOfType<SceneChange>();
        if (evnetButton != null)
            AddListener(() => Fade_ButtonTouch());
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Fade_Touch();
    }

    public virtual void Fade_Touch(Action call = null)
    {
        if (call == null) return;
        StartCoroutine(cSceneChangeChange(call));
    }

    public void AddListener(UnityAction call)
    {
        evnetButton.onClick.AddListener(call);
    }

    public virtual void Fade_ButtonTouch()
    {
        Debug.Log("페이드 버튼 터치");
        StartCoroutine(cSceneChangeChange(null));
    }
    public virtual void Fade_ButtonTouch(Action call = null)
    {
        StartCoroutine(cSceneChangeChange(call));
    }

    IEnumerator cSceneChangeChange(Action call)
    {
        if (call == null) yield break;

        sceneChange.Fade();
        yield return new WaitForSeconds(1f);
        call.Invoke();
    }
}