using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonComponent<DataManager>
{
    private int lv;
    private int gameMoney;
    private int cash;
    private int mileage;

    public int Lv { get => lv; set => lv = value; }
    public int GameMoney { get => gameMoney; set => gameMoney = value; }
    public int Cash { get => cash; set => cash = value; }
    public int Mileage { get => mileage; set => mileage = value; }

    private void Awake()
    {
        SetInstance();
    }

    public void SetUserInfo(int gameMoney, int cash, int mileage)
    {
        Debug.Log($"DataManager >> Set User Info : {gameMoney}/{cash}/{mileage}");
        this.gameMoney = gameMoney;
        this.cash = cash;
        this.mileage = mileage;
    }

}
