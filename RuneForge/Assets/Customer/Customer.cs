using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour {
    public bool spawnWithSpecificItem = false;
    public string itemName;
    public int amount;
    public FadeEffect fadeEffect;
    public Interactable interactScript;
    public Text text;

    public Quest quest;

    void Start()
    {
        if (spawnWithSpecificItem)
        {
            SetItem(new Quest(ItemCollection.itemDict[itemName], amount));
        }
        fadeEffect.FadeIn();
    }

    public void SetItem(Quest quest)
    {
        this.quest = quest;
        string plural = (quest.amountProduct > 1) ? "s" : "";
        text.text = string.Format("Can you make me {0} {1}{2}", quest.amountProduct, quest.product.name, plural);
    }

    public void AcceptQuest(bool autoLeave = true)
    {
        MasterGameManager.instance.questGenerator.currentQuests.Add(quest);
        if(autoLeave)
            Leave();
    }
    public void Leave()
    {
        interactScript.active = false;
        fadeEffect.FadeOut();
        Destroy(this.gameObject, 1.5f);
    }
}