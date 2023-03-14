using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPopupController : MonoBehaviour
{
    public bool isOpened = false;
    public GameObject backBlack;
    public LoginPopupComponent loginPopup;

    [Button("OpenPopup")]
    public virtual void OpenPopup(string title, string content)
    {
        Debug.Log("LoginPopupController >> Open Popup");
        isOpened = true;
        backBlack.gameObject.SetActive(true);
        loginPopup.OpenPopup(title, content);
    }

    [Button("ClosePopup")]
    public virtual void ClosePopup()
    { 
        Debug.Log("LoginPopupController >> Close Popup");
        isOpened = false;
        backBlack.gameObject.SetActive(false);
        loginPopup.ClosePopup();
    }

    private void Update()
    {
        if(isOpened)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ClosePopup();
            }
        }
    }
}
