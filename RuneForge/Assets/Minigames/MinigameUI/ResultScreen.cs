using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    bool checkLast = true;
    public Image board;
    public Image product;
    public GameObject scoreFill;
    public GameObject starFill;
    public GameObject progressFill;
    public Image expFill;
    public GameObject Minigame;
    public Text scoreText;
    public GameObject qualityStamp;
    float time = 1.5f;
    int minigameScore, totalScore;
    float st = 500, hq = 1000, mc = 1500;
    Image bronze, silver, gold;
    GameObject star1, star2, star3;
    int currentStage, requiredStage;
    float fillSpeed = 0.5f;
    WorkOrder currentOrder;
    AudioSource[] sfxSources;
    public AudioClip[] barSounds;
    public AudioClip[] completionSounds;
    public AudioClip[] completionSongs;
    Color starAlpha;
    float starWidth = 210, starHeight = 210;
    float starBigW = 4000, starBigH = 4000;
    bool corRun = false;
    bool finish = false;
    bool canClick = false;
    bool nextItem = false;
    float expToLevel;
    float previousLevel;
    AudioManager audioManager;
    AudioSource audioManagerObject;
    int workOrderIndex = 0;

    //float transition = 1.5f;

    public string nextScene = "Workshop";

    //Ctrl+F EFREN to find where to add sounds 
    //Ctrl+F EFREN to find where to add sounds 
    //Ctrl+F EFREN to find where to add sounds 
    //Ctrl+F EFREN to find where to add sounds 
    //Ctrl+F EFREN to find where to add sounds 

    void Start()
    {
        //set exp requirement
        expToLevel = MasterGameManager.instance.playerStats.nextLevelUp();
        previousLevel = MasterGameManager.instance.playerStats.previousLevelUp();
        //Sets all necessary variables.
        //Sounds
        sfxSources = this.gameObject.GetComponents<AudioSource>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManagerObject = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        //score fills
        bronze = scoreFill.transform.FindChild("Standard").GetComponent<Image>();
        silver = scoreFill.transform.FindChild("High Quality").GetComponent<Image>();
        gold = scoreFill.transform.FindChild("Master Craft").GetComponent<Image>();
        //star stuff
        star1 = starFill.transform.FindChild("Star1").gameObject;
        star2 = starFill.transform.FindChild("Star2").gameObject;
        star3 = starFill.transform.FindChild("Star3").gameObject;
        starAlpha = starFill.transform.FindChild("Star1").GetComponent<Image>().color;
        starAlpha.a = 1;
        //starts with the first order
        EntireFunction();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            //Goes to nextScene variable when coroutines and updating orders are done
            if (finish)
                LoadScene();
            else if (nextItem)
            {
                //Goes to next item on click
                EntireFunction();
                nextItem = false;
            }
            else
            {
                //TO EFREN: Do not add any sounds here! Ctrl+F EFREN to find where to add sounds 
                //When clicked, all board stuff will show up
                StopAllCoroutines();
                //Sets the result prefab alpha to 1
                setAlphaImage(GetComponent<Image>(), 1);
                //Sets board alpha to 1
                board.GetComponent<CanvasGroup>().alpha = 1;
                //Sets progressfill alpha to 1
                setAlphaImage(progressFill.GetComponent<Image>(), 1);
                //Sets scorefill, minigame text, and 3 stars to alpha 1
                scoreFill.GetComponent<CanvasGroup>().alpha = 1;
                Minigame.GetComponent<CanvasGroup>().alpha = 1;
                starFill.GetComponent<CanvasGroup>().alpha = 1;
                //Gets the default fill amounts for next item
                bronze.fillAmount = Mathf.Clamp((float)totalScore / st, 0f, 1f);
                silver.fillAmount = Mathf.Clamp(((float)totalScore - st) / (hq - st), 0f, 1f);
                gold.fillAmount = Mathf.Clamp(((float)totalScore - hq) / (mc - hq), 0f, 1f);
                progressFill.GetComponent<Image>().fillAmount = (float)currentStage / requiredStage;
                //Sets expfill
                expFill.fillAmount = Mathf.Clamp((MasterGameManager.instance.playerStats.currentExperience - previousLevel) / (expToLevel - previousLevel), 0f, 1f);
                //Sets text of minigames and alpha
                Text currentMinigame = Minigame.transform.FindChild(currentOrder.currentStage.ToString()).GetComponent<Text>();
                currentMinigame.text = currentOrder.minigameList[currentOrder.currentStage - 1].Key + ": " + currentOrder.minigameList[currentOrder.currentStage - 1].Value;
                setAlphaText(currentMinigame, 1);
                //Sets alpha of total score text
                scoreText.text = currentOrder.score.ToString();
                setAlphaText(scoreText, 1);
                //Set star alpha and size
                if (bronze.fillAmount == 1)
                {
                    setAlphaImage(star1.GetComponent<Image>(), 1);
                    star1.GetComponent<RectTransform>().sizeDelta = new Vector2(starWidth, starHeight);
                }
                if (silver.fillAmount == 1)
                {
                    setAlphaImage(star2.GetComponent<Image>(), 1);
                    star2.GetComponent<RectTransform>().sizeDelta = new Vector2(starWidth, starHeight);
                }
                if (gold.fillAmount == 1)
                {
                    setAlphaImage(star3.GetComponent<Image>(), 1);
                    star3.GetComponent<RectTransform>().sizeDelta = new Vector2(starWidth, starHeight);
                }
                //Check if current order is complete and sets its alpha to 1 and plays sound
                if (currentOrder.isComplete)
                {
                    GameObject quality = qualityStamp.transform.FindChild(currentOrder.quality).gameObject;
                    if (currentOrder.quality == "fail")
                    {
                        Debug.Log("What the fuck did you just fucking say about me, you little bitch? I’ll have you know I graduated top of my class in the Navy Seals, and I’ve been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You’re fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that’s just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little “clever” comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn’t, you didn’t, and now you’re paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You’re fucking dead, kiddo.");
                    }
                    else if (currentOrder.quality == "standard")
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
                    setAlphaImage(quality.GetComponent<Image>(), 1);
                }
                //Increments to work on next order and allows the click
                workOrderIndex++;
                nextItem = true;
                //Checks if its the last item in the order list
                if (checkLast)
                    finish = true;
            }
        }
    }

    //Fades to black
    IEnumerator FadeResults()
    {
        Time.timeScale = 0;
        //Changes alpha gradually to result screen fader
        float alpha = GetComponent<Image>().color.a;
        while (GetComponent<Image>().color.a <= 1)
        {
            alpha += Time.unscaledDeltaTime / time;
            setAlphaImage(GetComponent<Image>(), alpha);
            yield return new WaitForEndOfFrame();
        }
        //Afterwards start the next animation and allow player to click to skip
        canClick = true;
        StartCoroutine(FadeBoard());
    }

    //Fade product and board
    IEnumerator FadeBoard()
    {
        product.sprite = currentOrder.item.icon;
        float alpha = board.GetComponent<CanvasGroup>().alpha;
        while (board.GetComponent<CanvasGroup>().alpha < 1)
        {
            //Board
            board.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;
            yield return new WaitForEndOfFrame();

            alpha += Time.unscaledDeltaTime / time;

            //Progress bar
            setAlphaImage(progressFill.GetComponent<Image>(), alpha);

            //Scorefill and minigame text
            scoreFill.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;
            Minigame.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;

            //Stars
            starFill.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime / time;

            //exp fill
            setAlphaImage(expFill, alpha);
        }
        StartCoroutine(FadeScoreFill());
    }

    //adds the score fill
    IEnumerator FadeScoreFill()
    {
        //moveFillAmount is a very big function to control star fill and score fill go down to find it
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
        //EFREN: dunno if we need a progress fill sound but up 2 u
        foreach (AudioSource sfx in sfxSources)
            sfx.Stop();
        while (progressFill.GetComponent<Image>().fillAmount < (float)currentStage / requiredStage)
        {
            progressFill.GetComponent<Image>().fillAmount = Mathf.MoveTowards(progressFill.GetComponent<Image>().fillAmount, (float)currentStage / requiredStage, Time.unscaledDeltaTime * fillSpeed);
            if (((float)currentStage / requiredStage) - progressFill.GetComponent<Image>().fillAmount <= 0.015f)
                progressFill.GetComponent<Image>().fillAmount = (float)currentStage / requiredStage;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(NextMinigame());
    }

    //Minigame line text setter
    IEnumerator NextMinigame()
    {
        //Have to set to 0 beforehand cuz i set them all to 1 in the beginning for some reason that i forgot
        Text currentMinigame = Minigame.transform.FindChild(currentOrder.currentStage.ToString()).GetComponent<Text>();
        setAlphaText(currentMinigame, 0);
        currentMinigame.text = currentOrder.minigameList[currentOrder.currentStage - 1].Key + ": " + currentOrder.minigameList[currentOrder.currentStage - 1].Value;
        float alpha = currentMinigame.color.a;
        while (currentMinigame.color.a < 1)
        {
            alpha += Time.unscaledDeltaTime / time;
            setAlphaText(currentMinigame, alpha);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(scoreTextFade());
    }

    IEnumerator scoreTextFade()
    {
        scoreText.text = currentOrder.score.ToString();
        while (scoreText.color.a < 1)
        {
            Color temp = scoreText.color;
            temp.a += Time.unscaledDeltaTime / time;
            scoreText.color = temp;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(EXPFill());
    }

    IEnumerator EXPFill()
    {
        //EFREN: we probably do need an exp fill sound tho....idk
        Debug.Log("Current Exp: " + MasterGameManager.instance.playerStats.currentExperience);
        Debug.Log("Previous Level: " + previousLevel);
        Debug.Log("EXP to Level: " + expToLevel);
        float exp = (MasterGameManager.instance.playerStats.currentExperience - previousLevel) / (expToLevel - previousLevel);
        while (expFill.fillAmount < exp)
        {
            expFill.fillAmount = Mathf.MoveTowards(expFill.fillAmount, exp, Time.unscaledDeltaTime * fillSpeed);
            if (1 - expFill.fillAmount <= 0.015f)
            {
                if (exp > 1)
                {
                    expFill.fillAmount = 0;
                    exp -= 1;
                    MasterGameManager.instance.playerStats.incrementLevel();
                    expToLevel = MasterGameManager.instance.playerStats.nextLevelUp();
                    previousLevel = MasterGameManager.instance.playerStats.previousLevelUp();
                }
                else
                    expFill.fillAmount = 1;
            }            
            yield return new WaitForEndOfFrame();
        }
        if (checkLast)
            finish = true;
        if (currentOrder.isComplete)
            StartCoroutine(DebateQuality());
        else
        {
            workOrderIndex++;
            nextItem = true;
        }
    }

    IEnumerator DebateQuality()
    {
        //realfill checks which bar to do jitter with, roll makes it fill if you rolled successfully
        Image realFill = null;
        bool roll = false;
        if (currentOrder.quality == "fail")
            realFill = bronze;
        else if (currentOrder.quality == "standard")
        {
            if (bronze.fillAmount >= .99f)
                realFill = silver;
            else {
                realFill = bronze;
                roll = true;
            }
        }
        else if (currentOrder.quality == "hq")
        {
            if (silver.fillAmount >= .99f)
                realFill = gold;
            else {
                realFill = silver;
                roll = true;
            }
        }
        else if (currentOrder.quality == "mc")
        {
            if (gold.fillAmount >= .99f)
                StartCoroutine(Stamp());
            else {
                realFill = gold;
                roll = true;
            }
        }

        //jitter variables
        float change = realFill.fillAmount + 0.02f;
        float unchange = realFill.fillAmount - 0.02f;
        bool changeBool = false;
        float timer = 2f;
        if (realFill == null)
        {
            timer = 0f;
        }
        //EFREN: add jitter sound or nah?
        while (timer > 0f)
        {
            if (realFill.fillAmount < change && !changeBool)
            {
                realFill.fillAmount = Mathf.MoveTowards(realFill.fillAmount, change, Time.unscaledDeltaTime * fillSpeed);
                if (change - realFill.fillAmount <= 0.001f)
                    changeBool = true;
            }
            else if (changeBool)
            {
                realFill.fillAmount = Mathf.MoveTowards(realFill.fillAmount, unchange, Time.unscaledDeltaTime * fillSpeed);
                if (realFill.fillAmount - unchange <= 0.001f)
                    changeBool = false;
            }
            timer -= Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(TrueQuality(realFill, roll));
    }

    //This sets quality after jitter
    IEnumerator TrueQuality(Image fill, bool roll)

    {
        if (roll)
        {
            while (fill.fillAmount < 1)
            {
                fill.fillAmount = Mathf.MoveTowards(fill.fillAmount, 1, Time.unscaledDeltaTime * fillSpeed);
                if (1 - fill.fillAmount <= 0.015f)
                {
                    fill.fillAmount = 1;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        StartCoroutine(Stamp());
    }


    //Shows quality fade
    IEnumerator Stamp()
    {
        GameObject quality = qualityStamp.transform.FindChild(currentOrder.quality).gameObject;
        Debug.Log(quality.name);
        if (currentOrder.quality == "fail")
        {
            Debug.Log("What the fuck did you just fucking say about me, you little bitch? I’ll have you know I graduated top of my class in the Navy Seals, and I’ve been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You’re fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that’s just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little “clever” comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn’t, you didn’t, and now you’re paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You’re fucking dead, kiddo.");
        }
        else if (currentOrder.quality == "standard")
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

        float alpha = quality.GetComponent<Image>().color.a;
        while (quality.GetComponent<Image>().color.a < 1)
        {
            alpha += Time.unscaledDeltaTime / time;
            setAlphaImage(quality.GetComponent<Image>(), alpha);
            yield return new WaitForEndOfFrame();
        }
        //THIS IS THE END, so set next item and workorderindex
        workOrderIndex++;
        nextItem = true;
    }

    //Moves fill amount for score... not going to explain this its a lot
    bool moveFillAmount()
    {
        float bronzeFill = Mathf.Clamp((float)totalScore / st, 0f, 1f);
        float silverFill = Mathf.Clamp(((float)totalScore - st) / (hq - st), 0f, 1f);
        float goldFill = Mathf.Clamp(((float)totalScore - hq) / (mc - hq), 0f, 1f);
        if (bronze.fillAmount < bronzeFill)
        {

            sfxSources[0].PlayOneShot(barSounds[0]);
            bronze.fillAmount = Mathf.MoveTowards(bronze.fillAmount, bronzeFill, Time.unscaledDeltaTime * fillSpeed);
            if (bronzeFill - bronze.fillAmount <= 0.015f)
            {
                bronze.fillAmount = bronzeFill;
                if (bronze.fillAmount == 1)
                    StartCoroutine(StarFill(star1));
            }
        }
        else if (silver.fillAmount < silverFill)
        {
            sfxSources[1].PlayOneShot(barSounds[1]);
            silver.fillAmount = Mathf.MoveTowards(silver.fillAmount, silverFill, Time.unscaledDeltaTime * fillSpeed);
            if (silverFill - silver.fillAmount <= 0.015f)
            {
                silver.fillAmount = silverFill;
                if (silver.fillAmount == 1)
                    StartCoroutine(StarFill(star2));
            }
        }
        else if (gold.fillAmount < goldFill)
        {
            sfxSources[2].PlayOneShot(barSounds[2]);
            gold.fillAmount = Mathf.MoveTowards(gold.fillAmount, goldFill, Time.unscaledDeltaTime * fillSpeed);
            if (goldFill - gold.fillAmount <= 0.015f)
            {
                gold.fillAmount = goldFill;
                if (gold.fillAmount == 1)
                    StartCoroutine(StarFill(star3));
            }
        }
        //this part checks if coroutine is still running so that star can finish
        else if (corRun)
            return true;
        else
        {
            return false;
        }
        return true;
    }

    //Starboy fill
    IEnumerator StarFill(GameObject star)
    {
        corRun = true;
        RectTransform starTrans = star.GetComponent<RectTransform>();
        star.GetComponent<Image>().color = starAlpha;
        starTrans.sizeDelta = new Vector2(starBigW, starBigH);
        //EFREN: add star DING sound here
        while (starTrans.sizeDelta.x != starWidth && starTrans.sizeDelta.y != starHeight)
        {
            starTrans.sizeDelta = Vector2.MoveTowards(starTrans.sizeDelta, new Vector2(starWidth, starHeight), Time.unscaledDeltaTime * 9000);
            if (starTrans.sizeDelta.x - starWidth <= 0.015f)
                starTrans.sizeDelta = new Vector2(starWidth, starHeight);
            yield return new WaitForEndOfFrame();
        }
        corRun = false;
    }

    //This runs once on one order, the workorderindex will make it work on the next order if there are multiple and reset values.
    void EntireFunction()
    {
        WorkOrder order = MasterGameManager.instance.workOrderManager.currentWorkOrders[workOrderIndex];
        //If workorder is not 0, it will reset variables
        if (workOrderIndex != 0)
        {
            //quality reset
            GameObject quality = qualityStamp.transform.FindChild(MasterGameManager.instance.workOrderManager.currentWorkOrders[workOrderIndex - 1].quality).gameObject;
            Color temp = quality.GetComponent<Image>().color;
            temp.a = 0;
            quality.GetComponent<Image>().color = temp;
            quality.SetActive(false);

            //score reset
            temp = scoreText.color;
            temp.a = 0;
            scoreText.color = temp;

            //minigame text lines reset
            int noAlpha = currentOrder.currentStage - 1;
            foreach (Transform child in Minigame.transform)
            {
                if (noAlpha == 0)
                    setAlphaText(child.gameObject.GetComponent<Text>(), 0);
                else
                    noAlpha--;
            }

            //star reset
            temp = star1.GetComponent<Image>().color;
            temp.a = 0;
            star1.GetComponent<Image>().color = temp;
            star2.GetComponent<Image>().color = temp;
            star3.GetComponent<Image>().color = temp;
        }
        if (order != MasterGameManager.instance.workOrderManager.currentWorkOrders[MasterGameManager.instance.workOrderManager.currentWorkOrders.Count - 1])
            checkLast = false;
        else
            checkLast = true;

        /* Calculate total thresholds for entire product. (e.g. if requires 3 games, then st = 3*1000. if 4 games, st = 4*1000) */
        st = (float)MasterGameManager.instance.SDThreshold * order.requiredStages;
        hq = (float)MasterGameManager.instance.HQThreshold * order.requiredStages;
        mc = (float)MasterGameManager.instance.MCThreshold * order.requiredStages;
        expFill.fillAmount = Mathf.Clamp((MasterGameManager.instance.playerStats.currentExperience - previousLevel) / (expToLevel - previousLevel), 0f, 1f);
        currentOrder = order;
        bronze.fillAmount = Mathf.Clamp(order.score / st, 0, 1);
        silver.fillAmount = Mathf.Clamp((order.score - st) / (hq - st), 0, 1);
        gold.fillAmount = Mathf.Clamp((order.score - hq) / (mc - hq), 0, 1);
        if (bronze.fillAmount == 1)
            star1.GetComponent<Image>().color = starAlpha;
        if (silver.fillAmount == 1)
            star2.GetComponent<Image>().color = starAlpha;
        if (gold.fillAmount == 1)
            star3.GetComponent<Image>().color = starAlpha;
        progressFill.GetComponent<Image>().fillAmount = ((float)order.currentStage / order.requiredStages);
        minigameScore = GameObject.Find("Score").GetComponent<Score>().score;
        for (int stage = 1; stage <= order.currentStage; stage++)
        {
            Minigame.transform.FindChild(stage.ToString()).GetComponent<Text>().text = order.minigameList[stage - 1].Key + ": " + order.minigameList[stage - 1].Value;
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
    }

    //Set alpha of desired image
    void setAlphaImage(Image image, float alpha)
    {
        Color temp = image.color;
        temp.a = alpha;
        image.color = temp;
    }

    //Set alpha of desired text
    void setAlphaText(Text text, float alpha)
    {
        Color temp = text.color;
        temp.a = alpha;
        text.color = temp;
    }

    //Loads to next scene
    public void LoadScene()
    {
        MasterGameManager.instance.sceneManager.LoadScene(nextScene);
    }
}

