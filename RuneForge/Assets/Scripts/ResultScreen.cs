using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    CanvasGroup fade;
    float time = 1.5f;
    float transition = 1.5f;

    public string nextScene = "Workshop";

    void Start() {
        //Fade
        fade = GetComponent<CanvasGroup>();
        
        //Start to fade into result screen
        StartCoroutine("FadeIn");

        //Find final score text
        //string scoreText = GameObject.Find("Score").transform.Find("ScoreText").GetComponent<Text>().text;
        //transform.Find("Final Score").gameObject.GetComponent<Text>().text = "Final Score: " + scoreText;

        //Update the work order with the score
        foreach (WorkOrder order in MasterGameManager.instance.workOrderManager.currentWorkOrders)
        {
            order.UpdateOrder(MasterGameManager.instance.sceneManager.currentScene, GameObject.Find("Score").GetComponent<Score>().s);
            
            //If complete, show work order score and rune completed.
            if (order.isComplete)
            {
                transform.Find("Actions").gameObject.SetActive(false);
                transform.Find("Final Score").gameObject.SetActive(false);
                transform.Find("CompletedOrder").gameObject.SetActive(true);
                Item completedItem = MasterGameManager.instance.workOrderManager.CompleteOrder(order);
                string name = completedItem.name;
                string quality = "Standard";
                if (completedItem.name.Contains("(HQ)"))
                {
                    name = completedItem.name.Substring(0, completedItem.name.Length - 5);
                    quality = "High Quality";
                }
                else if (completedItem.name.Contains("(MC)"))
                {
                    name = completedItem.name.Substring(0, completedItem.name.Length - 5);
                    quality = "Master Craft";
                }
                transform.Find("CompletedOrder").transform.Find("completeOrderText").GetComponent<Text>().text = 
                    string.Format("Completed: {0}\nQuality: {1}\nTotal Score: {2}", name, quality, order.score.ToString());
            }
        }
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

    //IEnumerator FadeOut()    
    //{
    //    Time.timeScale = 0;
    //    while (fade.alpha > 0)
    //    {
    //        fade.alpha -= Time.unscaledDeltaTime / time;
    //        yield return null;
    //    }
    //    Time.timeScale = 1;
    //}   

    //IEnumerator Workshop()
    //{
    //    while (transition >= 0)
    //    {
    //        transition -= Time.unscaledDeltaTime / time;
    //        yield return null;
    //    }
    //    Debug.Log("true");
    //    MasterGameManager.instance.sceneManager.LoadScene(nextScene);
    //}

    public  void LoadScene()
    {
        MasterGameManager.instance.sceneManager.LoadScene(nextScene);
    }
}

