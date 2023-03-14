using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class JoinController : BasePopup
{
    public InputField input_ID;
    public InputField input_NickName;
    public InputField input_Email;
    public InputField input_Password;
    public InputField input_PasswordConfirm;

    private void Start()
    {
        input_ID.onValueChanged.AddListener((word) => input_ID.text = Regex.Replace(word, @"[^0-9a-zA-Z��-�R]", ""));
        input_NickName.onValueChanged.AddListener((word) => input_NickName.text = Regex.Replace(word, @"[^0-9a-zA-Z��-�R]", ""));

        input_ID.onValidateInput += delegate (string text, int charIndex, char addedChar) {
            return ChangeLowerCase(addedChar);
        };
    }

    public override void OpenPopup()
    {
        base.OpenPopup();
        input_ID.text = "";
        input_NickName.text = "";
        input_Email.text = "";
        input_Password.text = "";
        input_PasswordConfirm.text = "";

    }

    public void OnConfirm()
    {
        Debug.Log("JoinController >> OnConfirm");

        //Debug.Log("���̵� üũ : " + IsValidID(input_ID.text));

        //if (IsValidID(input_ID.text) == false) return;
        if (IsValidNickName(input_NickName.text) == false) return;
        if (IsValidEmail(input_Email.text) == false) return;
        if (IsValidPassWord(input_Password.text, input_PasswordConfirm.text) == false) return;

        if(SocketClientManager.Instance.IsOn() == false)
        {
            LoginSceneManager.Instance.OpenPopup("�˸�", "������ ������ �� �����ϴ�");
            return;
        }
        SocketClientManager.Instance.Join(input_Email.text, input_NickName.text, input_Password.text, JoinResult);
    }

    public void JoinResult(int result)
    {
        switch (result)
        {
            case 0:
                LoginSceneManager.Instance.OpenPopup("�˸�", "���� ������ ���� ����");
                break;
            case 1:
                LoginSceneManager.Instance.OpenPopup("�˸�", "�����ϴ� Email �ּ��Դϴ�");
                break;
            case 2:
                LoginSceneManager.Instance.OpenPopup("�˸�", "�����ϴ� �г����Դϴ�");
                break;
            case 3:
                LoginSceneManager.Instance.OpenPopup("�˸�", "ȸ������ �Ϸ�");
                ClosePopup();
                break;
            default:
                break;
        }
    }

    private bool IsValidID(string id)
    {
        bool IsValidLength = (id.Length >= 6 && id.Length <= 20) ? true : false;
        if (IsValidLength == false)
        {
            LoginSceneManager.Instance.OpenPopup("�˸�", "ID ���̴� 6~20���Դϴ�");
            return false;
        }

        bool IsValidRequire = Regex.IsMatch(id, @"^(?=.*[a-z])(?=.*[0-9])");
        if(IsValidRequire == false)
        {
            LoginSceneManager.Instance.OpenPopup("�˸�", "ID�� ����� ���ڰ� �ݵ�� ���ԵǾ�� �մϴ�.");
        }
       
        return IsValidRequire;
    }

    private bool IsValidNickName(string id)
    {
        bool IsValidLength = (id.Length <= 6) ? true : false;
        if (IsValidLength == false)
        {
            LoginSceneManager.Instance.OpenPopup("�˸�", "�г����� ���̴� 6���Դϴ�");
        }

        return IsValidLength;
    }

    public bool IsValidEmail(string email)
    {
        bool IsValidEmail = Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        if(IsValidEmail == false)
        {
            LoginSceneManager.Instance.OpenPopup("�˸�", "Email ����� ���� �ʽ��ϴ�");
        }
        return IsValidEmail;
    }

    public bool IsValidPassWord(string password, string passwordConfirm)
    {
        bool IsValidLength = (password.Length < 6) ? false : true;
        if (IsValidLength == false)
        {
            LoginSceneManager.Instance.OpenPopup("�˸�", "��й�ȣ�� 6�� �Է����ּ���");
            return false;
        }

        if(password != passwordConfirm)
        {
            LoginSceneManager.Instance.OpenPopup("�˸�", "��й�ȣ Ȯ�� ���� �ٸ��ϴ�");
            return false;
        }

        return true;
    }

    private char ChangeLowerCase(char _cha)
    {
        char tmpChar = _cha;

        string tmpString = tmpChar.ToString();

        tmpString = tmpString.ToLower();

        tmpChar = System.Convert.ToChar(tmpString);

        return tmpChar;
    }
}
