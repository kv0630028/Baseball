using GooglePlayGames;
//using Google.Play.Review;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleManager : MonoBehaviour
{
    // Create instance of ReviewManager
    //private ReviewManager _reviewManager;

    //public LoginSceneEvent loginSceneEvent;
    //public Text infoText;
    //public GameObject blackGO;
    //private void Awake()
    //{
    //    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
    //    PlayGamesPlatform.InitializeInstance(config);
    //    PlayGamesPlatform.DebugLogEnabled = true;
    //    PlayGamesPlatform.Activate();
    //    infoText.text = "";
    //    blackGO.SetActive(false);
    //}
    //public void LogIn()
    //{
    //    Debug.Log("GoogleManager >> LogIn");

    //    infoText.text = "�α��� ��";
    //    blackGO.SetActive(true);
    //    Social.localUser.Authenticate((bool success) =>
    //    {
    //        blackGO.SetActive(false);
    //        if (success)
    //        {
    //            Debug.Log("LogIn Success :" + Social.localUser.id + " / " + Social.localUser.userName);
    //            //googleID = Social.localUser.id;
    //            infoText.text = "�α��� ����";

    //            loginSceneEvent.Fade_ButtonTouch();
    //        }
    //        else
    //        {
    //            Debug.Log("Login Fail");
    //            //StartCoroutine(cGoogleDataCheck()); //�׽�Ʈ
    //            infoText.text = "�α��� ����";
    //        }
    //    });
    //}
}