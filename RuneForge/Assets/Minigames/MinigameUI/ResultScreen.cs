using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    bool checkLast = true;
    public Image board;
    public Image product;
    public GameObject scoreFill;
    public GameObject progressFill;
    public GameObject Minigame;
    public GameObject qualityStamp;
    public Button done;
    float time = 1.5f;
    int minigameScore, totalScore;
    float st = 500, hq = 1000, mc = 1500;
    Image bronze, silver, gold;
    int currentStage, requiredStage;
    float fillSpeed = 0.5f;
    WorkOrder currentOrder;
    AudioSource[] sfxSources;
    public AudioClip[] barSounds;
    public AudioClip[] completionSounds;
    public AudioClip[] completionSongs;

    AudioManager audioManager;
    AudioSource audioManagerObject;

    //float transition = 1.5f;

    public string nextScene = "Workshop";

    void Start() {
        done.interactable = false;
        sfxSources = this.gameObject.GetComponents<AudioSource>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManagerObject = GameObject.Find("AudioManager").GetComponent<AudioSource>();
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
            if (order != MasterGameManager.instance.workOrderManager.currentWorkOrders[MasterGameManager.instance.workOrderManager.currentWorkOrders.Count - 1])            
                checkLast = false;            
            else
                checkLast = true;

            /* Calculate total thresholds for entire product. (e.g. if requires 3 games, then st = 3*1000. if 4 games, st = 4*1000) */
            st = (float)MasterGameManager.instance.SDThreshold * order.requiredStages;
            hq = (float)MasterGameManager.instance.HQThreshold * order.requiredStages;
            mc = (float)MasterGameManager.instance.MCThreshold * order.requiredStages;

            currentOrder = order;
            bronze.fillAmount = Mathf.Clamp(order.score / st, 0, 1);
            silver.fillAmount = Mathf.Clamp((order.score - st) / (hq - st), 0, 1);
            gold.fillAmount =   Mathf.Clamp((order.score - hq) / (mc - hq), 0, 1);
            progressFill.GetComponent<Image>().fillAmount = ((float)order.currentStage / order.requiredStages);
            minigameScore = GameObject.Find("Score").GetComponent<Score>().score;
            for (int stage = 1; stage <= order.currentStage; stage++)
            {
                Minigame.transform.FindChild(stage.ToString()).GetComponent<Text>().text = order.minigameList[stage - 1].Key + ": "+ order.minigameList[stage-1].Value;
            }
            order.UpdateOrder(MasterGameManager.instance.sceneManager.currentScene, minigameScore);
            MasterGameManager.instance.playerStats.gainExperience(minigameScore);
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
            //Restult Screen itself
            Color temp = GetComponent<Image>().color;
            temp.a += Time.unscaledDeltaTime / time;
            GetComponent<Image>().color = temp;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FadeBoard());
    }

    //Fade product and board
    IEnumerator FadeBoard()
    {
        product.sprite = currentOrder.item.icon;
        while (board.GetComponent<CanvasGroup>().alpha < 1)
        {
            //Board
            board.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;
            yield return new WaitForEndOfFrame();

            //Progress bar
            Color temp = progressFill.GetComponent<Image>().color;
            temp.a += Time.unscaledDeltaTime / time;
            progressFill.GetComponent<Image>().color = temp;

            //Scorefill and minigame text
            scoreFill.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;
            Minigame.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;
        }
        StartCoroutine(FadeScoreFill());
    }
    ////Fades product worked on
    //IEnumerator FadeProduct()
    //{
    //    product.sprite = currentOrder.item.icon;
    //    while (product.color.a <= 1)
    //    {
    //        Color temp = product.color;
    //        temp.a += Time.unscaledDeltaTime / time;
    //        product.color = temp;
    //        yield return new WaitForEndOfFrame();
    //    }

    //}

    //adds the score fill
    IEnumerator FadeScoreFill()
    {
        while (moveFillAmount())
        {
            yield return new WaitForEndOfFrame();
        }
        foreach (AudioSource sfx in sfxSources)
            sfx.Stop();
        StartCoroutine(ProgressFill());
    }


    //Progress fill
    IEnumerator ProgressFill()
    {
        foreach (AudioSource sfx in sfxSources)
            sfx.Stop();
        while (progressFill.GetComponent<Image>().fillAmount < (float)currentStage/requiredStage)
        {
            progressFill.GetComponent<Image>().fillAmount = Mathf.MoveTowards(progressFill.GetComponent<Image>().fillAmount, (float)currentStage / requiredStage, Time.unscaledDeltaTime * fillSpeed);
            if (((float)currentStage / requiredStage) - progressFill.GetComponent<Image>().fillAmount <= 0.0015f)
                progressFill.GetComponent<Image>().fillAmount = (float)currentStage / requiredStage;
            yield return new WaitForEndOfFrame();
        }     
        StartCoroutine(NextMinigame());
    }

    IEnumerator NextMinigame()
    {
        Text currentMinigame = Minigame.transform.FindChild(currentOrder.currentStage.ToString()).GetComponent<Text>();
        Color temp = currentMinigame.color;
        temp.a = 0;
        currentMinigame.color = temp;
        currentMinigame.text = currentOrder.minigameList[currentOrder.currentStage - 1].Key + ": " + currentOrder.minigameList[currentOrder.currentStage - 1].Value;
        while (currentMinigame.color.a < 1)
        {
            Color temp2 = currentMinigame.color;
            temp2.a += Time.unscaledDeltaTime / time;
            currentMinigame.color = temp2;
            yield return new WaitForEndOfFrame();
        }
        if (currentOrder.isComplete)
        {
            StartCoroutine(Stamp());
        }
        else if (checkLast)
            StartCoroutine(FadeDone());
        //else
        //    StartCoroutine(FadeNext());
    }

    IEnumerator Stamp()
    {
        GameObject quality = qualityStamp.transform.FindChild(currentOrder.quality).gameObject;
        Debug.Log(quality.name);
        if (currentOrder.quality == "fail")
        {
            Debug.Log("What the fuck did you just fucking say about me, you little bitch? I’ll have you know I graduated top of my class in the Navy Seals, and I’ve been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You’re fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that’s just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little “clever” comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn’t, you didn’t, and now you’re paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You’re fucking dead, kiddo.");
        }
        else if(currentOrder.quality == "standard")
        {
            sfxSources[0].PlayOneShot(completionSounds[0]);
            //audioManagerObject.Stop();
            //audioManagerObject.loop = true;
            //audioManagerObject.clip = completionSongs[0];
            //audioManagerObject.Play();
        }
        else if (currentOrder.quality == "hq")
        {
            sfxSources[0].PlayOneShot(completionSounds[1]);
            //audioManagerObject.Stop();
            //audioManagerObject.loop = true;
            //audioManagerObject.clip = completionSongs[1];
            //audioManagerObject.Play();
        }
        else if (currentOrder.quality == "mc")
        {
            sfxSources[0].PlayOneShot(completionSounds[2]);
            //audioManagerObject.Stop();
            //audioManagerObject.loop = true;
            //audioManagerObject.clip = completionSongs[2];
            //audioManagerObject.Play();
        }
        quality.SetActive(true);

        while (quality.GetComponent<Image>().color.a < 1)
        {
            Color temp = quality.GetComponent<Image>().color;
            temp.a += Time.unscaledDeltaTime / time;
            quality.GetComponent<Image>().color = temp;
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
            if (bronzeFill - bronze.fillAmount <= 0.0015f)
                bronze.fillAmount = bronzeFill;
            sfxSources[0].PlayOneShot(barSounds[0]);
            bronze.fillAmount = Mathf.MoveTowards(bronze.fillAmount, bronzeFill, Time.unscaledDeltaTime * fillSpeed);
            
        }
        else if (silver.fillAmount < silverFill)
        {
            if (silverFill - silver.fillAmount <= 0.0015f)
                silver.fillAmount = silverFill;
            sfxSources[1].PlayOneShot(barSounds[1]);
            silver.fillAmount = Mathf.MoveTowards(silver.fillAmount, silverFill, Time.unscaledDeltaTime * fillSpeed);
        }
        else if (gold.fillAmount < goldFill)
        {
            if (goldFill - gold.fillAmount <= 0.0015f)
                gold.fillAmount = goldFill;
            sfxSources[2].PlayOneShot(barSounds[2]);
            gold.fillAmount = Mathf.MoveTowards(gold.fillAmount, goldFill, Time.unscaledDeltaTime * fillSpeed);
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

