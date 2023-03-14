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
    
    //ȸ������ ��ư Ŭ��
    public void OnJoinButton()
    {
        Debug.Log("LoginController >> OnJoinButton");
        LoginSceneManager.Instance.OpenJoin();
    }

    //�α��� ��ư Ŭ��
    public void OnLoginButton()
    {
        Debug.Log("LoginController >> OnLoginButton");

        if (SocketClientManager.Instance.IsOn() == false)
        {
            LoginSceneManager.Instance.OpenPopup("�˸�", "������ ������ �� �����ϴ�");
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
                LoginSceneManager.Instance.OpenPopup("�˸�", "���� ������ ���� ����");
                break;
            case 1:
                LoginSceneManager.Instance.OpenPopup("�˸�", "���Ե��� ���� Email�Դϴ�");
                break;
            case 2:
                LoginSceneManager.Instance.OpenPopup("�˸�", "��й�ȣ�� Ʋ���ϴ�");
                break;
            case 3:
                DataManager.Instance.SetUserInfo(LoginResponse.goodsData.GameMoney, LoginResponse.goodsData.Cash, LoginResponse.goodsData.Mileage);

                LoginSceneManager.Instance.LoadGameScene();
                Debug.Log("�α��� ���� :" + LoginResponse.goodsData.GameMoney + "/ " + LoginResponse.goodsData.Mileage + "/ " + LoginResponse.goodsData.Cash);
                break;
            default:
                break;
        }
    }
}
