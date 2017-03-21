using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestBoardUI : MonoBehaviour
{
    public GameObject questNote;
    [HideInInspector]
    public int currentDisplayedDay = 0;
    public List<GameObject> questObjects;
    float xPos = 150, yPos = 120, padY = -60;
    int objCount;
    public ItemListUI productList;

    public AudioClip clickSound;
    public AudioClip turnInSound;

    public void playClickSound()
    {
        MasterGameManager.instance.audioManager.PlaySFXClip(clickSound);
    }

    //public void Enable(bool active)
    //{
    //    if (active)
    //        DisplayBoard();
    //    else
    //    {
    //        ClearBoard();
    //    }
    //    this.gameObject.SetActive(active);
    //    MasterGameManager.instance.uiManager.Enable(this.gameObject, active);
    //    //MasterGameManager.instance.uiManager.uiOpen = active;
    //    //MasterGameManager.instance.uiManager.EnableMenuBar(!active);
    //    //MasterGameManager.instance.interactionManager.canInteract = !active;
    //}

    //void OnEnable()
    //{
    //    DisplayBoard();
    //    MasterGameManager.instance.uiManager.uiOpen = true;
    //}

    //void OnDisable()
    //{
    //    MasterGameManager.instance.uiManager.uiOpen = false;
    //}

    void OnEnable()
    {
        DisplayBoard();
    }

    void OnDisable()
    {
        ClearBoard();
    }

    public void DisplayBoard()
    {
        objCount = 0;
        foreach (Quest quest in MasterGameManager.instance.questGenerator.currentQuests)
        {
            GameObject newQuest = (GameObject)Instantiate(questNote, questNote.transform.position, Quaternion.identity);
            newQuest.transform.SetParent(this.transform);
            newQuest.transform.localScale = Vector3.one;
            float yPosNew = yPos + (padY * objCount);
            newQuest.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPosNew, 0);
            newQuest.GetComponent<QuestNote>().quest = quest;
            questObjects.Add(newQuest);
            //newQuest.transform.SetAsFirstSibling();
            newQuest.transform.FindChild("QuestName").GetComponent<Text>().text = quest.amountProduct.ToString() + " " + quest.product.name;
            newQuest.transform.FindChild("QuestIcon").GetComponent<Image>().sprite = quest.product.icon;
            newQuest.transform.FindChild("QuestGold").transform.FindChild("GoldInfo").GetComponent<Text>().text = "x" + quest.gold;
            newQuest.transform.FindChild("QuestIngredient").GetComponent<Image>().sprite = quest.ingredient.icon;
            newQuest.transform.FindChild("QuestIngredient").transform.FindChild("IngredientInfo").GetComponent<Text>().text = "x" + quest.amountIngredient;
            objCount++;
        }
        //foreach (Order order in MasterGameManager.instance.orderGenerator.todaysOrders)
        //{
        //    GameObject newOrderNote = (GameObject)Instantiate(questNote, this.transform.position, Quaternion.identity);
        //    newOrderNote.transform.SetParent(this.transform);
        //    newOrderNote.transform.localScale = Vector3.one;

        //    float xPos = Random.Range(rectTransform.rect.xMin + padX, rectTransform.rect.xMax - padX);
        //    float yPos = Random.Range(rectTransform.rect.yMin + padY, rectTransform.rect.yMax - padY);
        //    newOrderNote.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPos, 0);

        //    newOrderNote.transform.FindChild("OrderIcon").GetComponent<Image>().sprite = order.item.icon;
        //    newOrderNote.transform.FindChild("OrderName").GetComponent<Text>().text = "Need One: " + order.item.name;

        //    //newOrderNote.transform.Rotate(Vector3.forward, Random.Range(-15f, 15f));
        //}
        //currentDisplayedDay = MasterGameManager.instance.actionClock.Day;
    }

    public void ClearBoard()
    {
        foreach (GameObject quest in questObjects)
        {
            Destroy(quest);
        }
        questObjects.Clear();
    }

    public virtual void turnInQuest(GameObject quest, GameObject item)
    {
        Quest qValues = quest.GetComponent<QuestNote>().quest;
        int addIngr = 0;
        float goldMultiplier = 1f;
        string submitName = item.GetComponent<ItemButton>().item.name;
        string productName = qValues.product.name;
        if (PlayerInventory.inventory.GetItemCount(item.GetComponent<ItemButton>().item) >= qValues.amountProduct &&
            string.Compare(submitName, productName) >= 0)
        {
            if (item.GetComponent<ItemButton>().item.name.Contains("(HQ)"))
            {
                addIngr++;
                goldMultiplier = 1.5f;
            }
            if (item.GetComponent<ItemButton>().item.name.Contains("(MC)"))
            {
                addIngr += 2;
                goldMultiplier = 2f;
            }
            objCount = 0;
            questObjects.Remove(quest);
            MasterGameManager.instance.audioManager.PlaySFXClip(turnInSound);
            MasterGameManager.instance.questGenerator.currentQuests.Remove(quest.GetComponent<QuestNote>().quest);
            foreach (GameObject q in questObjects)
            {
                float yPosNew = yPos + (padY * objCount);
                q.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPosNew, 0);
                objCount++;
            }
            PlayerInventory.inventory.AddItem(qValues.ingredient, qValues.amountIngredient + addIngr);
            PlayerInventory.inventory.SubtractItem(item.GetComponent<ItemButton>().item, qValues.amountProduct);
            PlayerInventory.money += Mathf.RoundToInt(qValues.gold * goldMultiplier);
            MasterGameManager.instance.storeDayStats.quests++;
            Destroy(quest);
            productList.RefreshPage();
        }
        else
        {
            Debug.Log(false);
        }
    }
}