using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    CanvasGroup fade;
    float time = 1.5f;
    float transition = 3f;

    void Start() {
        fade = GetComponent<CanvasGroup>();
        Time.timeScale = 0;
        StartCoroutine("FadeIn");
        string scoreText = GameObject.Find("Score").GetComponent<Text>().text;
        transform.Find("Final Score").gameObject.GetComponent<Text>().text = "Final " + scoreText;

        int score;
        int.TryParse(scoreText.Split(' ')[1], out score);
        foreach (WorkOrder order in MasterGameManager.instance.workboard.currentWorkOrders)
        {
            order.UpdateOrder(MasterGameManager.instance.sceneManager.currentScene, score);
        }

        StartCoroutine("Workshop");
    }

    IEnumerator FadeIn()
    {
        while (fade.alpha <= 1)
        {
            fade.alpha += Time.unscaledDeltaTime / time;
            yield return null;
        }
        Time.timeScale = 1;
    }

    IEnumerator Workshop()
    {
        while (transition >= 0)
        {
            transition -= Time.unscaledDeltaTime / time;
            yield return null;
        }
        MasterGameManager.instance.sceneManager.LoadScene("Workshop");
    }
}

