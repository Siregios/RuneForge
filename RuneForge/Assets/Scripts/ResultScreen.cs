using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    CanvasGroup fade;
    float time = 1.5f;
    float transition = 3f;

    void Start() {
        //Fade
        fade = GetComponent<CanvasGroup>();
        
        //Start to fade into result screen
        StartCoroutine("FadeIn");
        //Find final score text
        string scoreText = GameObject.Find("Score").GetComponent<Text>().text;        
        transform.Find("Final Score").gameObject.GetComponent<Text>().text = "Final " + scoreText;
        int score;
        int.TryParse(scoreText.Split(' ')[1], out score);
        //Update the work order with the score
        foreach (WorkOrder order in MasterGameManager.instance.workOrderManager.currentWorkOrders)
        {
            order.UpdateOrder(MasterGameManager.instance.sceneManager.currentScene, score);
            
            //If complete, show work order score and rune completed.
            if (order.isComplete)
            {
                transform.Find("Actions").gameObject.SetActive(false);
                transform.Find("Final Score").gameObject.SetActive(false);
                transform.Find("CompletedOrder").gameObject.SetActive(true);
                transform.Find("CompletedOrder").transform.Find("completeOrderText").GetComponent<Text>().text = "Completed Rune: " + order.item.name + "\nQuality: Standard\nTotal Score: " + order.score.ToString();
                MasterGameManager.instance.workOrderManager.CompleteOrder(order);
            }
        }
        StartCoroutine("Workshop");
    }

    IEnumerator FadeIn()
    {
        Time.timeScale = 0;
        while (fade.alpha <= 1)
        {
            fade.alpha += Time.unscaledDeltaTime / time;
            yield return null;
        }
        Time.timeScale = 1;
    }

    IEnumerator FadeOut()    
    {
        Time.timeScale = 0;
        while (fade.alpha > 0)
        {
            fade.alpha -= Time.unscaledDeltaTime / time;
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

