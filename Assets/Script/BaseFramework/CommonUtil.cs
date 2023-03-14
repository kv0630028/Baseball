using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class CommonUtil : MonoBehaviour
{
    public static int GetRandomScore(int minScore, int maxScore)
    {
        return Random.Range(minScore / 1000, maxScore / 1000) * 1000;
    }

    public static int GetTotalScore(List<int> scoreList)
    {
        int totalScore = 0;
        for (int i = 0; i < scoreList.Count; i++)
        {
            totalScore += scoreList[i];
        }
        return totalScore;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scoreList"></param>
    /// <param name="leftScore"></param>
    public static void SharingScore(ref List<int> scoreList, int leftScore, int maxLimit, int scoreUnit = 1000)
    {
        int index = 0;
        int loopCount = 0;
        while (leftScore > 0)
        {
            if (scoreList[index] + scoreUnit <= maxLimit)
            {
                scoreList[index] += scoreUnit;
                leftScore -= scoreUnit;
                loopCount = 0;
            }
            else
            {
                loopCount++;
            }

            index++;
            if (index >= scoreList.Count) index = 0;

            if (loopCount >= scoreList.Count) break;
        }
    }

    public static void FadeCanvas(CanvasGroup canvas, float from, float to, float duration)
    {
        canvas.alpha = from;
        DOTween.To(() => canvas.alpha, x => canvas.alpha = x, to, duration);
    }
    public static void FadeCanvas(CanvasGroup canvas, float from, float to, float duration, System.Action call)
    {
        canvas.alpha = from;
        DOTween.To(() => canvas.alpha, x => canvas.alpha = x, to, duration).OnComplete(() => call.Invoke());
    }
    public static void FadeColor(RawImage rawImage, float from, float to, float duration)
    {
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, from);
        DOTween.ToAlpha(() => rawImage.color, x => rawImage.color = x, to, duration);
    }

    public static void FadeColor(Image image, float from, float to, float duration)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, from);
        DOTween.ToAlpha(() => image.color, x => image.color = x, to, duration);
    }







    public static List<int> CreateBigScoreList(int totalScore, bool max = true)
    {
        int rndBigScore = 500000;

        if (max)
        {
            rndBigScore = 500000;
        }
        else
        {
            rndBigScore = Random.Range(150, 500) * 1000;
        }

        List<int> scoreList = new List<int>();

        if (rndBigScore >= totalScore)
        {
            scoreList.Add(totalScore);
        }
        else if (totalScore - rndBigScore < 22000)
        {
            scoreList.Add(rndBigScore);
            scoreList.Add(22000);
        }
        else
        {
            totalScore -= rndBigScore;
            scoreList = ScoreSeparator(totalScore, 150, 300);
            scoreList.Add(rndBigScore);
        }

        return scoreList;
    }

    const int SCORE_UNIT = 1000;
    /// <summary>
    /// 점수를 Queue[int]로 나눠주는 함수입니다. 기본 단위는 천 단위입니다
    /// </summary>
    /// <param name="score">총 획득 점수</param>
    /// <param name="min">최소 점수 (단위 : 천)</param>
    /// <param name="max">최대 점수 (단위 : 천)</param>
    /// <returns></returns>
    public static List<int> ScoreSeparator(int score, int min, int max)
    {
        List<int> scoreList = new List<int>();
        int totalScore = score;

        if (score >= 500000)
        {
            int rndBigScore = 500000;

            scoreList.Add(rndBigScore);
            totalScore -= rndBigScore;
        }

        while (totalScore > 0)
        {
            int randomScore = UnityEngine.Random.Range(min, max);
            if (randomScore % 2 != 0) randomScore -= 1;
            randomScore *= SCORE_UNIT;
            // * SCORE_UNIT;
            randomScore = Mathf.Min(totalScore, randomScore);
            int left = totalScore - randomScore;

            //1. 랜덤 스코어를 일반적으로 넣어주는 경우
            if ((left > min * SCORE_UNIT))
            {
                totalScore -= randomScore;
                scoreList.Add(randomScore);
            }
            //2. 나머지가 최소값 미만인 경우
            else
            {
                // 2 - 1.랜덤 스코어에서 나머지를 더했을 때 최대 값 이상인 경우
                if (left + randomScore > max * SCORE_UNIT)
                {
                    totalScore -= randomScore;
                    scoreList.Add(randomScore);

                    int index = 0;      //인덱스
                    int maxCount = 0;   //모든 인덱스가 포화인 경우 체크
                    while (left > 0)
                    {
                        if (scoreList.Count == 0) Debug.LogError("[ Error ] Score Count :" + scoreList.Count);
                        if (index > scoreList.Count - 1) index = 0;

                        //다른 스코어리스트의 값들에 스코어 유닛 단위만큼 넣어준다. 
                        //만약 모든 스코어리스트들이 최대 값이라면, 최대값을 초과하더라도 넣어준다.
                        if (scoreList[index] < max * SCORE_UNIT || maxCount > scoreList.Count)
                        {
                            int addMoney = 0;
                            if (left > SCORE_UNIT * 2) addMoney = SCORE_UNIT * 2;
                            else addMoney = left;

                            scoreList[index] += addMoney;
                            left -= addMoney;
                            totalScore -= addMoney;
                        }
                        else
                        {
                            maxCount++;
                        }

                        index++;
                    }
                }
                //나머지를 더한 값이 최대값 미만인 경우
                else
                {
                    randomScore += left;
                    totalScore -= randomScore;
                    scoreList.Add(randomScore);
                }
            }
        }

        ShuffleList(scoreList);

#if DEBUG
        int checkTotalScore = 0;
        for (int i = 0; i < scoreList.Count; i++)
        {
            checkTotalScore += scoreList[i];
        }

        if (checkTotalScore != score) Debug.LogError(string.Format("[ Error ] ScoreSeparator 결과 값이 다르게 도출되었습니다. 총 사용 점수 : [{0}] 입력 점수 : [{1}]", checkTotalScore, score));
#endif

        return scoreList;
    }
    public enum LOGCOLOR
    {
        AQUA,
        BLACK,
        BLUE,
        BROWN,
        CYAN,
        RED,
        YELLOW
    }

    /// <summary>
    /// 에러로그에 색깔 넣어주는 함수입니다
    /// </summary>
    /// <param name="color">색깔</param>
    /// <param name="log">텍스트</param>
    public static void LogError(LOGCOLOR color, string log)
    {
        switch (color)
        {
            case LOGCOLOR.AQUA:
                Debug.LogError("<color=aqua>" + log + "</color>");
                break;
            case LOGCOLOR.BLACK:
                Debug.LogError("<color=black>" + log + "</color>");
                break;
            case LOGCOLOR.BLUE:
                Debug.LogError("<color=blue>" + log + "</color>");
                break;
            case LOGCOLOR.BROWN:
                Debug.LogError("<color=brown>" + log + "</color>");
                break;
            case LOGCOLOR.CYAN:
                Debug.LogError("<color=cyan>" + log + "</color>");
                break;
            case LOGCOLOR.RED:
                Debug.LogError("<color=red>" + log + "</color>");
                break;
            case LOGCOLOR.YELLOW:
                Debug.LogError("<color=yellow>" + log + "</color>");
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 게임 오브젝트가 Active가 Off인 경우 On으로 변경해줍니다.
    /// </summary>
    /// <param name="go">타겟 오브젝트</param>
    public static void ShowGameObject(GameObject go)
    {
        if (!go.activeSelf) go.gameObject.SetActive(true);
    }

    /// <summary>
    /// 특정 숫자를 제외한 임의의 값을 가져옵니다.
    /// </summary>
    /// <param name="min">최소 값 </param>
    /// <param name="max">최대 값</param>
    /// <param name="exceptingNum">제외 값</param>
    /// <returns>임의의 숫자 추출 값</returns>
    public static int RandomRangeExcept(int min, int max, int exceptingNum)
    {
        int randNum = 0;
        while (true)
        {
            randNum = Random.Range(min, max);
            if (randNum != exceptingNum)
                return randNum;
        }
    }

    /// <summary>
    /// 특정 숫자를 제외한 임의의 값을 가져옵니다.
    /// </summary>
    public static int RandomRangeExcept(int min, int max, int ex1, int ex2)
    {
        int randNum = 0;
        while (true)
        {
            randNum = Random.Range(min, max);
            if (randNum != ex1 && randNum != ex2)
                return randNum;
        }
    }

    /// <summary>
    /// 특정 숫자를 제외한 임의의 값을 가져옵니다.
    /// </summary>
    public static int RandomRangeExcept(int min, int max, int ex1, int ex2, int ex3)
    {
        int randNum = 0;
        while (true)
        {
            randNum = Random.Range(min, max);
            if (randNum != ex1 && randNum != ex2 && randNum != ex3)
                return randNum;
        }
    }

    /// <summary>
    /// 특정 숫자를 제외한 임의의 값을 가져옵니다.
    /// </summary>
    public static int RandomRangeExcept(int min, int max, int ex1, int ex2, int ex3, int ex4)
    {
        int randNum = 0;
        while (true)
        {
            randNum = Random.Range(min, max);
            if (randNum != ex1 && randNum != ex2 && randNum != ex3 && randNum != ex4)
                return randNum;
        }
    }

    /// <summary>
	/// 특정 숫자를 제외한 임의의 값을 가져옵니다.
	/// </summary>
    public static int RandomRangeExcept(int min, int max, int ex1, int ex2, int ex3, int ex4, int ex5)
    {
        int randNum = 0;
        while (true)
        {
            randNum = Random.Range(min, max);
            if (randNum != ex1 && randNum != ex2 && randNum != ex3 && randNum != ex4 && randNum != ex5)
                return randNum;
        }
    }

    /// <summary>
    /// 배열을 셔플해주는 함수입니다.
    /// </summary>
    /// <typeparam name="T">타입</typeparam>
    /// <param name="array">배열</param>
    public static void ShuffleArray<T>(T[] array)
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < array.Length; ++index)
        {
            random1 = UnityEngine.Random.Range(0, array.Length);
            random2 = UnityEngine.Random.Range(0, array.Length);

            tmp = array[random1];
            array[random1] = array[random2];
            array[random2] = tmp;
        }
    }

    public static List<T> SumList<T>(List<T> list1, List<T> list2)
    {
        List<T> totalList = new List<T>();
        foreach (var item in list1)
        {
            totalList.Add(item);
        }

        foreach (var item in list2)
        {
            totalList.Add(item);
        }

        return totalList;
    }

    /// <summary>
    /// 리스트를 셔플 해주는 함수입니다.
    /// </summary>
    /// <typeparam name="T">타입</typeparam>
    /// <param name="list">배열</param>
    public static List<T> ShuffleList<T>(List<T> list)
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
        return list;
    }

    /// <summary>
    /// 스왑 함수 입니다.
    /// </summary>
    /// <typeparam name="T">타입</typeparam>
    /// <param name="value1">값1</param>
    /// <param name="value2">값2</param>
    public static void Swap<T>(ref T value1, ref T value2)
    {
        T tmp;
        tmp = value1;
        value1 = value2;
        value2 = tmp;
    }

    /// <summary>
    /// 리스트 스왑함수입니다.
    /// </summary>
    /// <typeparam name="T">타입</typeparam>
    /// <param name="list">리스트</param>
    /// <param name="index1">번호1</param>
    /// <param name="index2">번호2</param>
    public static void Swap<T>(ref List<T> list, int index1, int index2)
    {
        T tmp;
        tmp = list[index1];
        list[index1] = list[index2];
        list[index2] = tmp;
    }


    public static int GetRandomInt(int min, int max)
    {
        int rnd = UnityEngine.Random.Range(min, max + 1);
        return rnd;
    }
    public static int GetRandomDirection()
    {
        int dir = 1;
        int rnd = UnityEngine.Random.Range(0, 2);
        dir = (rnd == 0) ? -1 : 1;
        return dir;
    }
    /// <summary>
	/// 1의 자리 점수 리스트를 만들어주는 함수입니다
	/// </summary>
	/// <param name="totalScore">전체 점수</param>
	/// <param name="minScore">최소 점수(0 제외)</param>
	/// <param name="maxScore">최대 점수</param>
	/// <param name="addZero">0 추가 여부를 결정합니다</param>
	/// <param name="minZero">0을 추가할 경우 미니멈 퍼센트</param>
	/// <param name="maxZero">0을 추가할 경우 맥시멈 퍼센트</param>
	/// <returns></returns>
	public static List<int> GetRandomScoreList(int totalScore, int minScore, int maxScore, bool addZero, int minZero = 10, int maxZero = 30)
    {
        List<int> scoreList = new List<int>();

        int currentScore = totalScore;

        while (currentScore > 0)
        {
            int rndScore = UnityEngine.Random.Range(minScore, maxScore + 1);
            //현재 남은 값이 랜덤 값보다 작은 경우
            if (rndScore > currentScore)
            {
                rndScore = currentScore;
            }

            //랜덤 값이 미니멈보다 작은 경우
            if (rndScore < minScore)
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (scoreList[i] + rndScore <= maxScore)
                    {
                        scoreList[i] += rndScore;
                        currentScore -= rndScore;
                        break;
                    }
                }
            }
            else
            {
                scoreList.Add(rndScore);
                currentScore -= rndScore;
            }
        }

        //0을 추가하는 경우
        if (addZero)
        {
            int rndZeroPercent = UnityEngine.Random.Range(minZero, maxZero);
            int zeroTotalNumber = Mathf.FloorToInt(scoreList.Count * rndZeroPercent * 0.01f);

            for (int i = 0; i < zeroTotalNumber; i++)
            {
                scoreList.Add(0);
            }
        }

        return ShuffleList(scoreList);
    }

    #region Alpha
    public static void SetAlpha(Image target, float alpha)
    {
        Color currentColor = target.color;
        currentColor.a = alpha;
        target.color = currentColor;
    }

    public static void SetAlpha(SpriteRenderer target, float alpha)
    {
        Color currentColor = target.color;
        currentColor.a = alpha;
        target.color = currentColor;
    }
    public static void SetAlpha(Text target, float alpha)
    {
        Color currentColor = target.color;
        currentColor.a = alpha;
        target.color = currentColor;
    }


    public static void FadeAlpha(Image Image, float from, float to, float duration)
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, from);
        DOTween.ToAlpha(() => Image.color, x => Image.color = x, to, duration);
    }

    public static void FadeAlpha(RawImage rawImage, float from, float to, float duration)
    {
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, from);
        DOTween.ToAlpha(() => rawImage.color, x => rawImage.color = x, to, duration);
    }

    public static void FadeAlpha(SpriteRenderer sprite, float from, float to, float duration)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, from);
        DOTween.ToAlpha(() => sprite.color, x => sprite.color = x, to, duration);
    }

    public static void FadeAlpha(TextMeshProUGUI textMeshProUGUI, float from, float to, float duration)
    {
        textMeshProUGUI.color = new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, from);
        DOTween.ToAlpha(() => textMeshProUGUI.color, x => textMeshProUGUI.color = x, to, duration);
    }

    public static void FadeAlpha(Image Image, float from, float to, float duration, System.Action finishCall)
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, from);
        DOTween.ToAlpha(() => Image.color, x => Image.color = x, to, duration).OnComplete(finishCall.Invoke);
    }

    public static void FadeAlpha(SpriteRenderer sprite, float from, float to, float duration, System.Action finishCall)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, from);
        DOTween.ToAlpha(() => sprite.color, x => sprite.color = x, to, duration).OnComplete(finishCall.Invoke);
    }
    #endregion


    public static void ScaleChange(Transform transform, float from, float to, float duration)
    {
        transform.localScale = Vector2.one * from;
        transform.DOScale(to, duration);
    }
    public static void ScaleChange(Transform transform, float from, float to, float duration, Ease ease)
    {
        transform.localScale = Vector2.one * from;
        transform.DOScale(to, duration).SetEase(ease);
    }
    public static void Shake(GameObject obj, float duration, float strength = 3, int vibrato = 10, System.Action finishCall = null)
    {
        if (finishCall != null) DOTween.Shake(() => obj.transform.localPosition, x => obj.transform.localPosition = x, duration, strength, vibrato).OnComplete(finishCall.Invoke);
        else DOTween.Shake(() => obj.transform.localPosition, x => obj.transform.localPosition = x, duration, strength, vibrato);
    }
    /// <summary>
    /// 오브젝트 밀어내는 함수
    /// </summary>
    /// <param name="transform">밀어낼 오브젝트 트랜스폼</param>
    /// <param name="targetTr">이동할곳</param>
    /// <param name="duration">걸리는시간</param>
    /// <param name="isStartPos">시작위치로 갈껀지 안갈껀지</param>
    /// <param name="finishCall">끝나고나서</param>
    public static void Push(Transform transform, Transform targetTr, float duration, bool isStartPos, System.Action finishCall = null)
    {
        if (isStartPos)
        {
            Vector3 startPos = transform.localPosition;
            transform.DOLocalMove(targetTr.localPosition, duration).SetEase(Ease.OutExpo).OnComplete(() =>
            {
                if (finishCall != null) transform.DOLocalMove(startPos, 0.5f).OnComplete(finishCall.Invoke);
                else transform.DOLocalMove(startPos, 0.5f);
            });
        }
        else
        {
            if (finishCall != null) transform.DOLocalMove(targetTr.localPosition, duration).OnComplete(finishCall.Invoke);
            else transform.DOLocalMove(targetTr.localPosition, duration);
        }
    }

    public static SpriteRenderer CreateSpriteRenderer(GameObject parentPos, Sprite sprite, string sortingLayerName = null, int sortingOrder = 0)
    {
        GameObject go = new GameObject();
        go.transform.parent = parentPos.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.Euler(Vector3.zero);
        go.AddComponent<SpriteRenderer>();
        go.GetComponent<SpriteRenderer>().sprite = sprite;
        go.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
        go.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;

        return go.GetComponent<SpriteRenderer>();
    }

    public static Image CreateImage(GameObject parentPos, Sprite sprite)
    {
        GameObject go = new GameObject();
        go.transform.parent = parentPos.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.Euler(Vector3.zero);
        go.transform.localScale = Vector3.one;

        go.AddComponent<Image>();
        go.GetComponent<Image>().sprite = sprite;

        return go.GetComponent<Image>();
    }

    public static Image CreateImage(GameObject parentPos, Sprite sprite, Material material, float scale)
    {
        GameObject go = new GameObject();
        go.transform.parent = parentPos.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.Euler(Vector3.zero);
        go.transform.localScale = parentPos.transform.localScale * scale;

        go.AddComponent<Image>();
        go.GetComponent<Image>().sprite = sprite;
        go.GetComponent<Image>().material = material;

        return go.GetComponent<Image>();
    }

    public static int[] CreateArray(int max)
    {
        int[] array = new int[max];
        for (int i = 0; i < max; i++)
        {
            array[i] = i;
        }
        return array;
    }

    #region DOTween_KDY
    public static void Move(GameObject target, Vector2 endValue, float time, System.Action completeCall = null, Ease ease = Ease.OutQuad)
    {
        target.transform.DOMove(endValue, time).SetEase(ease).OnComplete(() => completeCall?.Invoke());
    }
    public static void LocalMove(GameObject target, Vector2 endValue, float time, System.Action completeCall = null, Ease ease = Ease.OutQuad)
    {
        target.transform.DOLocalMove(endValue, time).SetEase(ease).OnComplete(() => completeCall?.Invoke());
    }
    /// <summary>
    /// 흔들흔들
    /// </summary>
    /// <param name="target">목표물</param>
    /// <param name="duration">트윈 시간</param>
    /// <param name="strength">흔들림 강도(범위)</param>
    /// <param name="vibrato">얼마나 진동 하는지</param>
    /// <param name="randomness">흔들림 무작위 발생 0으로 설정하면 한방향으로 흔들림</param>
    /// <param name="fadeOut">True인 경우 흔들림이 자동으로 부드럽게 페이드 아웃됨</param>
    public static void ShakeScale(GameObject target, float duration, float strength, int vibrato = 10, float randomness = 90, bool fadeOut = true, System.Action completeCall = null, Ease ease = Ease.OutQuad)
    {
        target.transform.DOShakeScale(duration, strength, vibrato, randomness, fadeOut).SetEase(ease).OnComplete(() => completeCall?.Invoke());
    }
    public static void ShakeScale(GameObject target, float duration, Vector3 strength, int vibrato = 10, float randomness = 90, bool fadeOut = true, System.Action completeCall = null, Ease ease = Ease.OutQuad)
    {
        target.transform.DOShakeScale(duration, strength, vibrato, randomness, fadeOut).SetEase(ease).OnComplete(() => completeCall?.Invoke());
    }
    /// <summary>
    /// 주어진 방향으로 펀칭 한 다음 마치 탄성체를 통해 시작 위치에 연결된 것처럼 시작 위치로 돌아갑니다.
    /// </summary>
    /// <param name="target">목표물</param>
    /// <param name="punch">펀치의 방향과 강도 (Transform의 현재 위치에 추가됨).</param>
    /// <param name="duration">트윈시간</param>
    /// <param name="vibrato">펀치가 진동하는 정도</param>
    /// <param name="elasticity">뒤로 바운딩 할 때 벡터가 시작 위치를 넘어갈 정도(0~1) 1은 펀치 방향과 반대 방향 사이에 완전한 진동을 생성하고 0은 펀치와 시작 위치 사이에서만 진동 합니다.</param>
    /// <param name="snapping">True인 경우 트윈은 모든 값을 정수에 매끄럽게 스냅합니다.</param>
    public static void PunchPosition(GameObject target, Vector3 punch, float duration, int vibrato = 10, float elasticity = 1, bool snapping = false, System.Action completeCall = null, Ease ease = Ease.OutQuad)
    {
        target.transform.DOPunchPosition(punch, duration, vibrato, elasticity, snapping).SetEase(ease).OnComplete(() => completeCall?.Invoke());
    }
    public static void LocalRotate(GameObject target, Vector2 endValue, float time, RotateMode mode = RotateMode.FastBeyond360, System.Action completeCall = null, Ease ease = Ease.OutQuad)
    {
        target.transform.DOLocalRotate(endValue, time, mode).SetEase(ease).OnComplete(() => completeCall?.Invoke());
    }
    public static void BlendableLocalMoveBy(GameObject target, Vector2 byValue, float duration, bool snapping = false, System.Action completeCall = null, int loopCount = 0, LoopType loopType = LoopType.Yoyo)
    {
        target.transform.DOBlendableLocalMoveBy(byValue, duration).SetLoops(loopCount, loopType).OnComplete(() => completeCall?.Invoke());
    }
    #endregion
}