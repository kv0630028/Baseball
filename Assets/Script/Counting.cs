using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counting : MonoBehaviour
{
    public TextMeshProUGUI number;

    public void ShowNumber()
    {
        number.gameObject.SetActive(true);
    }


    public float currScore = 0;

    public void CountingPlay(float setScore)
    {
        float current;
        float next;

        current = currScore;
        next = setScore + currScore;
        currScore = next;
        StartCoroutine(cCount(next, current));

    }
    IEnumerator cCount(float target, float current)
    {
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * Time.deltaTime;
            //number.text = $"<color=#ffd700>{(int)current}</color>";
            number.text = $"{(int)current}";
            yield return null;
        }

        current = target;
        //number.text = $"<color=#ffd700>{(int)current}</color>";
        number.text = $"{(int)current}";
    }

    public void DownCountingPlay(float score)
    {
        StartCoroutine(cDownCount(score));

    }
    IEnumerator cDownCount(float target)
    {
        Debug.Log(target + " t / c " + currScore);
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 
        float offset = (target - currScore) / duration;

        while (currScore > target)
        {
            currScore += offset * Time.deltaTime;
            //number.text = $"<color=#ffd700>{(int)current}</color>";
            number.text = $"{(int)currScore}";
            yield return null;
        }

        currScore = target;
        //number.text = $"<color=#ffd700>{(int)current}</color>";
        number.text = $"{(int)currScore}";
    }

    public void ResetScore()
    {
        StopAllCoroutines();
        currScore = 0;
        number.text = currScore.ToString();
    }
}