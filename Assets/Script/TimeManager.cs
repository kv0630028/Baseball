using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : SingletonComponent<TimeManager>
{
    public float leftTime; //남은시간  저장용

    public TextMeshProUGUI tLobbyLeftTime;
    public TextMeshProUGUI tInGameLeftTime;
    public float timer = 0;
    public float totalGameTime = 600;

    bool isPlaying;

    public DateTime currentDay;
    private void Awake()
    {
        SetInstance();
        Load();
        Debug.Log("leftTime => " + leftTime);
        SetLeftTime(Mathf.CeilToInt(leftTime - timer));
    }

    private void Update()
    {
        if(currentDay.Day != DateTime.Now.Day)
        {
            currentDay = DateTime.Now;
            leftTime = 600;
            Save();
            Debug.Log("날짜 업데이트 확인");
            SetLeftTime((int)leftTime);
        }
    }
    public bool PlayTime()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            totalGameTime = this.leftTime;
        }
        timer += Time.deltaTime;

        int leftTime = Mathf.CeilToInt(totalGameTime - timer);
        this.leftTime = leftTime;

        SetLeftTime(leftTime);

        if (timer > totalGameTime) return true;
        else return false;
    }
    void SetLeftTime(int leftTime)
    {
        string min = (leftTime / 60).ToString();
        if (min.Length == 1) min = "0" + min;
        string second = (leftTime % 60).ToString();
        if (second.Length == 1) second = "0" + second;

        tLobbyLeftTime.text = $"{min}:{second}";
        tInGameLeftTime.text = $"Time {min}:{second}";
    }
    public void TimerReset()
    {
        timer = 0;
        isPlaying = false;
    }
    void Load()
    {
        leftTime = PlayerPrefs.GetFloat("LEFT_TIME", 600);
        currentDay = System.DateTime.Parse(PlayerPrefs.GetString("DAY_INFO", DateTime.Now.ToString()));
    }

    [Button("Save Test")]
    public void SaveTest(int day)
    {
        
        DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);
        currentDay = date;
        PlayerPrefs.SetString("DAY_INFO", currentDay.ToString());
        Debug.Log("설정 된 날짜 확인 :" + currentDay);
    }
    void Save()
    {
        PlayerPrefs.SetFloat("LEFT_TIME", leftTime);
        PlayerPrefs.SetString("DAY_INFO", currentDay.ToString());
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
