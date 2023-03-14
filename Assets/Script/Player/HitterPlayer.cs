using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterPlayer : MonoBehaviour
{
    public void Swing()
    {
        HomeRunGameManager.Instance.CheckHit();
    }
}
