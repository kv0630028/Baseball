using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TOPInfoController : MonoBehaviour
{
    public TextMeshProUGUI lvText;
    public TextMeshProUGUI gameMoneyText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI mileageText;

    public void UpdateInfo()
    {
        Debug.Log("TopInfoController >> UpdateInfo : " + DataManager.Instance.Cash);
        lvText.text = DataManager.Instance.Lv.ToString();
        gameMoneyText.text = DataManager.Instance.GameMoney.ToString();
        cashText.text = DataManager.Instance.Cash.ToString();
        mileageText.text = DataManager.Instance.Mileage.ToString();
    }
}
