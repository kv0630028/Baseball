using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : SingletonComponent<LobbyManager>
{
    private const string gameCoinKey = "GAME_COIN";
    private const string gameCouponKey = "GAME_COUPON";

    public TextMeshProUGUI gameCoinText;
    public TextMeshProUGUI goldCouponText;

    private int gameCoin = 0;
    private int gameCoupon = 0;

    public int GameCoin { get => gameCoin; set
        {
            gameCoin = value;
            PlayerPrefs.SetInt(gameCoinKey, gameCoin);
            gameCoinText.text = gameCoin.ToString();
        }
    }

    public int GameCoupon { get => gameCoupon; set
        {
            gameCoupon = value;
            PlayerPrefs.SetInt(gameCouponKey, gameCoupon);
            goldCouponText.text = gameCoupon.ToString();
        }
    }

    private void Awake()
    {
        SetInstance();
        GameCoin = PlayerPrefs.GetInt(gameCoinKey, 0);
        GameCoupon = PlayerPrefs.GetInt(gameCouponKey, 0);
    }
}
