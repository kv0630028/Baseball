using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPopupComponent : BasePopup
{
    public Text titleText;
    public Text contentText;

    public override void OpenPopup(string title, string content)
    {
        base.OpenPopup(title, content);

        titleText.text = title;
        contentText.text = content;
    }
    
    public override void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
