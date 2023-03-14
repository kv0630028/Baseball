using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameViewController : MonoBehaviour
{
    public void OnBettingGame()
    {
        SceneManager.LoadScene("BettingGameScene");
    }
}
