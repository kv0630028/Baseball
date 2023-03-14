using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SocketClientManager;

public class LoginController : BasePopup
{
    public InputField input_ID;
    public InputField input_Password;

    public GameObject touchToStart;
    [SerializeField]
    private Toggle autoLogin;
    
    private void Awake()
    {
        autoLogin.isOn = bool.Parse(PlayerPrefs.GetString("autoLogin", "False"));
        autoLogin.SetIsOnWithoutNotify(autoLogin.isOn);

        startScale = transform.localScale;
        gameObject.SetActive(false);
    }

    protected override void Show()
    {
        base.Show();
        touchToStart.SetActive(false);
    }
    public void SetToggle(bool value)
    {
        PlayerPrefs.SetString("autoLogin", value.ToString());
        Debug.Log("LoginController >> SetToggle : " + value);
    }
    
    //회원가입 버튼 클릭
    public void OnJoinButton()
    {
        Debug.Log("LoginController >> OnJoinButton");
        LoginSceneManager.Instance.OpenJoin();
    }

    //로그인 버튼 클릭
    public void OnLoginButton()
    {
        Debug.Log("LoginController >> OnLoginButton");

        if (SocketClientManager.Instance.IsOn() == false)
        {
            LoginSceneManager.Instance.OpenPopup("알림", "서버에 접속할 수 없습니다");
            return;
        }
        SocketClientManager.Instance.Login(input_ID.text, input_Password.text, LoginResult);

        //LoginSceneManager.Instance.LoadGameScene();
    }

    public void LoginResult(LOGIN_RESPONSE LoginResponse)
    {
        switch (LoginResponse.result)
        {
            case 0:
                LoginSceneManager.Instance.OpenPopup("알림", "서버 데이터 접속 실패");
                break;
            case 1:
                LoginSceneManager.Instance.OpenPopup("알림", "가입되지 않은 Email입니다");
                break;
            case 2:
                LoginSceneManager.Instance.OpenPopup("알림", "비밀번호가 틀립니다");
                break;
            case 3:
                DataManager.Instance.SetUserInfo(LoginResponse.goodsData.GameMoney, LoginResponse.goodsData.Cash, LoginResponse.goodsData.Mileage);

                LoginSceneManager.Instance.LoadGameScene();
                Debug.Log("로그인 성공 :" + LoginResponse.goodsData.GameMoney + "/ " + LoginResponse.goodsData.Mileage + "/ " + LoginResponse.goodsData.Cash);
                break;
            default:
                break;
        }
    }
}
