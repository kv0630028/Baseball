using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneManager : MonoBehaviour
{
    public static LoginSceneManager Instance;

    public LoginController loginController;
    public JoinController joinContoller;
    public LoginPopupController loginPopupController;

    private void Awake()
    {
        Instance = this;
        //loginController.OpenPopup();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenJoin()
    {
        joinContoller.OpenPopup();
    }

    public void OpenPopup(string title, string content)
    {
        loginPopupController.OpenPopup(title, content);
    }
}
