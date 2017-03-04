using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int score = 0;
    public Text scoreText;
    public RectTransform scoreCursor;
    //MasterGameManager.Minigame currentMinigame;
    float maxHeight = 480;
    float percentage = 0f;

    void Awake()
    {
        maxHeight = scoreCursor.anchoredPosition.y;
        scoreCursor.anchoredPosition = Vector2.zero;
    }

    void Start()
    {
        //currentMinigame = MasterGameManager.instance.minigameDict[MasterGameManager.instance.sceneManager.currentScene];
    }

    void Update()
    {
        scoreText.text = score.ToString();

        MoveScoreCursor();
    }

    public void addScore(int add)
    {
        score += add;
        StartCoroutine(pulseScore());
    }

    public void subScore(int sub)
    {
        if (score - sub >= 0)
            score -= sub;        
    }

    IEnumerator pulseScore()
    {
        float timeElapsed = 0f;
        while (timeElapsed <= 0.25f)
        {
            timeElapsed += Time.deltaTime;

            scoreText.rectTransform.localScale = 1.25f * Vector3.one;
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = Vector3.one;
    }

    void MoveScoreCursor()
    {
        //if (0 <= score && score <= currentMinigame.SD)
        //{
        //    percentage = ((float)score / currentMinigame.SD);
        //}
        //else if (currentMinigame.SD < score && score <= currentMinigame.HQ)
        //{
        //    percentage = ((float)(score - currentMinigame.SD) / (currentMinigame.HQ - currentMinigame.SD) + 1);
        //}
        //else if (currentMinigame.HQ < score && score <= currentMinigame.MC)
        //{
        //    percentage = ((float)(score - currentMinigame.HQ) / (currentMinigame.MC - currentMinigame.HQ) + 2);
        //}
        //else if (score >= currentMinigame.MC)
        //{
        //    percentage = 3;
        //}
        int SD = MasterGameManager.instance.SDThreshold;
        int HQ = MasterGameManager.instance.HQThreshold;
        int MC = MasterGameManager.instance.MCThreshold;

        if (0 <= score && score <= SD)
        {
            percentage = ((float)score / SD);
        }
        else if (SD < score && score <= HQ)
        {
            percentage = ((float)(score - SD) / (HQ - SD) + 1);
        }
        else if (HQ < score && score <= MC)
        {
            percentage = ((float)(score - HQ) / (MC - HQ) + 2);
        }
        else if (score >= MC)
        {
            percentage = 3;
        }

        scoreCursor.anchoredPosition = Vector2.Lerp(scoreCursor.anchoredPosition, new Vector2(0f, percentage * (maxHeight / 3)), Time.deltaTime);
    }
}
