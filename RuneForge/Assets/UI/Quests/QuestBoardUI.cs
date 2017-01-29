using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestBoardUI : MonoBehaviour {
    public GameObject questNote;
    [HideInInspector]
    public int currentDisplayedDay = 0;    
    public List<GameObject> questObjects;
    float xPos, yPos, padY;
    int objCount;
    public ItemListUI productList;

    void Awake()
    {
        //rectTransform = this.GetComponent<RectTransform>();
        xPos = 175;
        yPos = 150;
        padY = -70;
    }

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.Enable(this.gameObject, active);
        //MasterGameManager.instance.uiManager.uiOpen = active;
        //MasterGameManager.instance.uiManager.EnableMenuBar(!active);
        //MasterGameManager.instance.interactionManager.canInteract = !active;
    }

    //void OnEnable()
    //{
    //    DisplayBoard();
    //    MasterGameManager.instance.uiManager.uiOpen = true;
    //}

    //void OnDisable()
    //{
    //    MasterGameManager.instance.uiManager.uiOpen = false;
    //}

    public void DisplayBoard()
    {
        objCount = 0;
        Debug.Log(MasterGameManager.instance.questGenerator.currentQuests.Count);
        foreach(Quest quest in MasterGameManager.instance.questGenerator.currentQuests)
        {            
            GameObject newQuest = (GameObject)Instantiate(questNote, questNote.transform.position, Quaternion.identity);
            newQuest.GetComponent<QuestNote>().quest = quest;
            questObjects.Add(newQuest);
            newQuest.transform.SetParent(this.transform);
            newQuest.transform.localScale = Vector3.one;
            newQuest.transform.SetAsFirstSibling();
            float yPosNew = yPos + (padY * objCount);
            newQuest.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPosNew, 0);
            newQuest.transform.FindChild("QuestName").GetComponent<Text>().text = "Need: " + quest.amountProduct.ToString() + "x " + quest.product.name;           
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

    public void turnInQuest(GameObject quest, GameObject item)
    {
        Quest qValues = quest.GetComponent<QuestNote>().quest;
        if (PlayerInventory.inventory.GetItemCount(item.GetComponent<ItemButton>().item) >= qValues.amountProduct)
        {
            objCount = 0;            
            questObjects.Remove(quest);
            MasterGameManager.instance.questGenerator.currentQuests.Remove(quest.GetComponent<QuestNote>().quest);
            foreach (GameObject q in questObjects)
            {
                float yPosNew = yPos + (padY * objCount);
                q.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPosNew, 0);
                objCount++;
            }
            PlayerInventory.inventory.AddItem(qValues.ingredient, qValues.amountIngredient);
            PlayerInventory.inventory.SubtractItem(item.GetComponent<ItemButton>().item, qValues.amountProduct);
            PlayerInventory.money += qValues.gold;
            Destroy(quest);
            productList.RefreshPage();
            Debug.Log(true);
        }
        else
        {
            Debug.Log(false);
        }
    }
}