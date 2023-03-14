using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HomeRunGameManager;

public class ScoreFont : MonoBehaviour
{
    public GameObject[] prefabs;
    public void CreateScore(eHIT_TYPE type, int coin)
    {
        GameObject go = Instantiate(prefabs[(int)type], transform);
        DamageNumber damageNumber = go.GetComponent<DamageNumber>();
        damageNumber.number = coin;
        damageNumber.prefix = "+";
    }
}
