using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    public Text result;
    public Image product;
    public GameObject score;
    public GameObject scoreFill;
    public GameObject progress;
    public GameObject progressFill;
    public Button done;
    float time = 1.5f;
    int minigameScore, totalScore;
    float st = 500, hq = 1000, mc = 1500;
    Image bronze, silver, gold;
    int currentStage, requiredStage;
    float fillSpeed = 0.5f;
    WorkOrder currentOrder;
    //float transition = 1.5f;

    public string nextScene = "Workshop";

    void Start() {
        done.interactable = false;
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
            currentOrder = order;
            bronze.fillAmount = Mathf.Clamp(order.score / st, 0, 1);
            silver.fillAmount = Mathf.Clamp((order.score - st) / (hq - st), 0, 1);
            gold.fillAmount =   Mathf.Clamp((order.score - hq) / (mc - hq), 0, 1);
            progressFill.GetComponent<Image>().fillAmount = ((float)order.currentStage / order.requiredStages);
            minigameScore = GameObject.Find("Score").GetComponent<Score>().score;
            order.UpdateOrder(MasterGameManager.instance.sceneManager.currentScene, minigameScore);
            requiredStage = order.requiredStages;
            currentStage = order.currentStage;
            totalScore = order.score;

            if (order.isComplete)
            {
                Item completedItem = MasterGameManager.instance.workOrderManager.CompleteOrder(order);
            }
            //Start to fade into result screen
            StartCoroutine(FadeResults());


            //If complete, show work order score and rune completed.
            //if (order.isComplete)
            //{
                //Item completedItem = MasterGameManager.instance.workOrderManager.CompleteOrder(order);
                //    transform.Find("Actions").gameObject.SetActive(false);
                //    transform.Find("Final Score").gameObject.SetActive(false);
                //    transform.Find("CompletedOrder").gameObject.SetActive(true);
                //    Item completedItem = MasterGameManager.instance.workOrderManager.CompleteOrder(order);
                //    string name = completedItem.name;
                //    string quality = "Standard";
                //    if (completedItem.name.Contains("(HQ)"))
                //    {
                //        name = completedItem.name.Substring(0, completedItem.name.Length - 5);
                //        quality = "High Quality";
                //    }
                //    else if (completedItem.name.Contains("(MC)"))
                //    {
                //        name = completedItem.name.Substring(0, completedItem.name.Length - 5);
                //        quality = "Master Craft";
                //    }
                //    transform.Find("CompletedOrder").transform.Find("completeOrderText").GetComponent<Text>().text = 
                //        string.Format("Completed: {0}\nQuality: {1}\nTotal Score: {2}", name, quality, order.score.ToString());
            //}
        }
    }
    
    //Fades to black
    IEnumerator FadeResults()
    {
        Time.timeScale = 0;
        while (GetComponent<Image>().color.a <= 1)
        {
            Color temp = GetComponent<Image>().color;
            temp.a += Time.unscaledDeltaTime / time;
            GetComponent<Image>().color = temp;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FadeResultText());
    }

    //Fades "Results" Text
    IEnumerator FadeResultText()
    {
        while (result.color.a <= 1)
        {
            Color temp = result.color;        
            temp.a += Time.unscaledDeltaTime / time;
            result.color = temp;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FadeProduct());
    }

    //Fades product worked on
    IEnumerator FadeProduct()
    {
        product.sprite = currentOrder.item.icon;
        while (product.color.a <= 1)
        {
            Color temp = product.color;
            temp.a += Time.unscaledDeltaTime / time;
            product.color = temp;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FadeScore());
    }

    //Fades score bar
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

    //adds the score fill
    IEnumerator FadeScoreFill()
    {
        while (moveFillAmount())
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(ProgressFade());
    }

    //Fades progress bar
    IEnumerator ProgressFade()
    {
        while (progress.GetComponent<CanvasGroup>().alpha < 1)
        {
            progress.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(ProgressFill());
    }

    //Progress fill
    IEnumerator ProgressFill()
    {
        while (progressFill.GetComponent<Image>().fillAmount < (float)currentStage/requiredStage)
        {
            progressFill.GetComponent<Image>().fillAmount = Mathf.MoveTowards(progressFill.GetComponent<Image>().fillAmount, (float)currentStage / requiredStage, Time.unscaledDeltaTime * fillSpeed);
            if (((float)currentStage / requiredStage) - progressFill.GetComponent<Image>().fillAmount <= 0.0015f)
                progressFill.GetComponent<Image>().fillAmount = (float)currentStage / requiredStage;
            yield return new WaitForEndOfFrame();
        }     
        StartCoroutine(FadeDone());
    }

    //Fades done button
    IEnumerator FadeDone()
    {
        while (done.GetComponent<CanvasGroup>().alpha < 1)
        {
            done.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        done.interactable = true;
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

    //Moves fill amount for score
    bool moveFillAmount()
    {
        float bronzeFill = Mathf.Clamp((float)totalScore / st, 0f, 1f);
        float silverFill = Mathf.Clamp(((float)totalScore-st) / (hq- st), 0f, 1f);
        float goldFill = Mathf.Clamp(((float)totalScore- hq) / (mc- hq), 0f, 1f);
        if (bronze.fillAmount < bronzeFill)
        {
            bronze.fillAmount = Mathf.MoveTowards(bronze.fillAmount, bronzeFill, Time.unscaledDeltaTime * fillSpeed);
            if (bronzeFill - bronze.fillAmount <= 0.0015f)
                bronze.fillAmount = bronzeFill;
            
        }
        else if (silver.fillAmount < silverFill)
        {
            silver.fillAmount = Mathf.MoveTowards(silver.fillAmount, silverFill, Time.unscaledDeltaTime * fillSpeed);
            if (silverFill - silver.fillAmount <= 0.0015f)
                silver.fillAmount = silverFill;
        }
        else if (gold.fillAmount < goldFill)
        {
            gold.fillAmount = Mathf.MoveTowards(gold.fillAmount, goldFill, Time.unscaledDeltaTime * fillSpeed);
            if (goldFill - gold.fillAmount <= 0.0015f)
                gold.fillAmount = goldFill;
        }
        else
        {
            return false;
        }
        return true;
    }

    public void LoadScene()
    {
        MasterGameManager.instance.sceneManager.LoadScene(nextScene);
    }
}

