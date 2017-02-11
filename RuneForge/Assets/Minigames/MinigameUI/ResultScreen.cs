using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    public Text result;
    public Image product;
    public GameObject score;
    public GameObject scoreFill;
    public GameObject progress;
    public Button done;
    float time = 1.5f;
    int minigameScore, totalScore;
    float st = 500, hq = 1000, mc = 1500;
    Image bronze, silver, gold;
    //float transition = 1.5f;

    public string nextScene = "Workshop";

    void Start() {
        //set fills
        bronze = scoreFill.transform.FindChild("Standard").GetComponent<Image>();
        silver = scoreFill.transform.FindChild("High Quality").GetComponent<Image>();
        gold = scoreFill.transform.FindChild("Master Craft").GetComponent<Image>();

        //Find final score text
        //string scoreText = GameObject.Find("Score").transform.Find("ScoreText").GetComponent<Text>().text;
        //transform.Find("Final Score").gameObject.GetComponent<Text>().text = "Final Score: " + scoreText;

        //Update the work order with the score
        foreach (WorkOrder order in MasterGameManager.instance.workOrderManager.currentWorkOrders)
        {
            order.score = 400;
            bronze.fillAmount = Mathf.Clamp(order.score / st, 0, 1);
            silver.fillAmount = Mathf.Clamp((order.score - st) / (hq - st), 0, 1);
            gold.fillAmount =   Mathf.Clamp((order.score - hq) / (mc - hq), 0, 1);
            minigameScore = GameObject.Find("Score").GetComponent<Score>().score;
            order.UpdateOrder(MasterGameManager.instance.sceneManager.currentScene, minigameScore);
            totalScore = order.score;   

            //Start to fade into result screen
            StartCoroutine(FadeResultText());

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

    IEnumerator FadeResultText()
    {
        Time.timeScale = 0;
        while (GetComponent<Image>().color.a <= 1)
        {
            Color temp = GetComponent<Image>().color;        
            temp.a += Time.unscaledDeltaTime / time;
            GetComponent<Image>().color = temp;
            temp = result.color;
            temp.a += Time.unscaledDeltaTime / time;
            result.color = temp;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FadeProduct());
    }

    IEnumerator FadeProduct()
    {
        while (product.color.a <= 1)
        {
            Color temp = product.color;
            temp.a += Time.unscaledDeltaTime / time;
            product.color = temp;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FadeScore());
    }

    IEnumerator FadeScore()
    {
        while (score.GetComponent<CanvasGroup>().alpha < 1)
        {
            score.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;
            scoreFill.GetComponent<CanvasGroup>().alpha = score.GetComponent<CanvasGroup>().alpha;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FadeScoreFill());
    }

    IEnumerator FadeScoreFill()
    {
        while (moveFillAmount())
        {
            yield return new WaitForEndOfFrame();
        }

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

    bool moveFillAmount()
    {
        if (bronze.fillAmount < Mathf.Clamp((float)totalScore / st, 0f, 1f))
        {
            bronze.fillAmount = Mathf.Lerp(bronze.fillAmount, Mathf.Clamp((float)totalScore / st, 0f, 1f), Time.unscaledDeltaTime * 3);
            if (1 - bronze.fillAmount <= 0.0015f)           
                bronze.fillAmount = 1;
            
        }
        else if (silver.fillAmount < Mathf.Clamp((float)totalScore / hq, 0f, 1f))
        {
            silver.fillAmount = Mathf.Lerp(silver.fillAmount, Mathf.Clamp(((float)totalScore- st) / (hq- st), 0f, 1f), Time.unscaledDeltaTime * 3);
            if (1 - silver.fillAmount <= 0.0015f)
                silver.fillAmount = 1;
        }
        else if (gold.fillAmount < Mathf.Clamp((float)totalScore / mc, 0f, 1f))
        {
            gold.fillAmount = Mathf.Lerp(gold.fillAmount, Mathf.Clamp(((float)totalScore- hq) / (mc- hq), 0f, 1f), Time.unscaledDeltaTime * 3);
            if (1 - gold.fillAmount <= 0.0015f)
                gold.fillAmount = 1;
        }
        else
        {
            return false;
        }
        return true;
    }

    public  void LoadScene()
    {
        MasterGameManager.instance.sceneManager.LoadScene(nextScene);
    }
}

