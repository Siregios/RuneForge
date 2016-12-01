using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour
{
    public Item item;

    public bool showHover = true;
    bool followingMouse = false;
    public Image typeCharm;
    public Image attribute1;
    public Image attribute2;
    public Text countText;
    GameObject LastClicked;
    Inventory referenceInventory;

    //Drag objects
    public GameObject dragObject;
    GameObject draggable;

    public int ItemCount
    {
        get
        {
            return (this.item != null) ? referenceInventory.GetItemCount(this.item) : 0;
        }
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != this.gameObject || LastClicked == null)
        {
            LastClicked = EventSystem.current.currentSelectedGameObject;
        }
        if (countText != null)
        {
            SetText();
        }
        if (followingMouse) { 
            draggable.transform.position = new Vector3(Input.mousePosition.x - 20, Input.mousePosition.y + 15, 0) ;
            OnHover(false);
        }
}

    public void Initialize(Item item, Inventory referenceInventory, List<Action<Item>> buttonFunctions)
    {
        this.item = item;
        this.referenceInventory = referenceInventory;
        this.transform.FindChild("Icon").GetComponent<Image>().sprite = item.icon;
        foreach (Action<Item> function in buttonFunctions)
        {
            this.GetComponent<Button>().onClick.AddListener(() => function(this.item));
        }
        if (countText != null)
            SetText();
        if (attribute1 != null || attribute2 != null)
            setAttributes();
        if (typeCharm != null)
            setType();
    }

    public void OnHover(bool active)
    {
        if (showHover)
        {
            if (active)
            {
                HoverInfo.Load();
                HoverInfo.instance.DisplayItem(this);
            }
            else
            {
                HoverInfo.instance.Hide();
            }
        }
    }

    public void OnMouseDown()
    {
        if (LastClicked != null)
        {
            if ((LastClicked.name == "QuestUI(Clone)"))
            {
                if (LastClicked.transform.Find("QuestIcon").GetComponent<Image>().sprite == gameObject.transform.Find("Icon").GetComponent<Image>().sprite)
                {
                    GameObject.Find("QuestPanel").GetComponent<QuestBoardUI>().turnInQuest(LastClicked, gameObject);
                    LastClicked = null;
                }
            }

            else {
                DragCopy();              
            }
        }
        else {
            DragCopy();
        }
    }

    public void OnMouseRelease()
    {
        followingMouse = false;
        Destroy(draggable);
        draggable = null;
        gameObject.GetComponent<Button>().interactable = true;
    }

    void SetText()
    {
        if (ItemCount == int.MaxValue)
            this.countText.text = "∞";
        else
            this.countText.text = "x" + ItemCount.ToString();
    }

    void DragCopy()
    {
        //Used to bring UI element to the front.
        gameObject.transform.SetAsLastSibling();
        //Then drag object.
        followingMouse = true;
        draggable = (GameObject)Instantiate(dragObject, this.transform.position, Quaternion.identity);
        draggable.transform.SetParent(gameObject.transform, false);
        draggable.GetComponent<Image>().sprite = gameObject.transform.Find("Icon").GetComponent<Image>().sprite;
        gameObject.GetComponent<Button>().interactable = false;
    }

    //REFACTOR : Use instatiation rather than set GameObjects
    void setAttributes()
    {
        if (attribute1 != null && item.provtAttrStr != null && item.provtAttrStr.Contains("ALL"))
        {
            attribute1.sprite = Resources.Load<Sprite>("ItemSprites/Charms/all_attr_charm");
            int value = item.providedAttributes["Fire"];    //Not very clean, but simple - just get the value of fire and it'll be the value for all
            attribute1.transform.Find("Text").GetComponent<Text>().text = value.ToString();
            return;
        }

        KeyValuePair<string, int> mainItemAttribute = new KeyValuePair<string, int>("", 0);
        KeyValuePair<string, int> secondaryItemAttribute = new KeyValuePair<string, int>("", 0);
        Dictionary<string, string> attributeToColor = new Dictionary<string, string>()
        {
            { "Fire", "red" },
            { "Water", "blue" },
            { "Earth", "yellow" },
            { "Air", "green" },
        };

        //Should only be two things in providedAttributes
        foreach (var kvp in item.providedAttributes)
        {
            if (kvp.Value > mainItemAttribute.Value)
            {
                secondaryItemAttribute = mainItemAttribute;
                mainItemAttribute = kvp;
            }
            else
                secondaryItemAttribute = kvp;
        }

        if (attribute1 != null && mainItemAttribute.Key != "" && mainItemAttribute.Value != 0)
        {
            attribute1.sprite = Resources.Load<Sprite>(string.Format("ItemSprites/Charms/{0}_circle", attributeToColor[mainItemAttribute.Key]));
            attribute1.transform.Find("Text").GetComponent<Text>().text = mainItemAttribute.Value.ToString();
        }
        if (attribute2 != null && secondaryItemAttribute.Key != "" && secondaryItemAttribute.Value != 0)
        {
            attribute2.sprite = Resources.Load<Sprite>(string.Format("ItemSprites/Charms/{0}_circle", attributeToColor[secondaryItemAttribute.Key]));
            attribute2.transform.Find("Text").GetComponent<Text>().text = secondaryItemAttribute.Value.ToString();
        }
    }

    void setType()
    {
        Image newImage = (Image)Instantiate(typeCharm, this.transform);
        newImage.rectTransform.localScale = Vector3.one;
        newImage.rectTransform.anchoredPosition = Vector3.zero;
        newImage.sprite = Resources.Load<Sprite>(string.Format("ItemSprites/Charms/{0} Charm", item.ingredientType));
    }
}