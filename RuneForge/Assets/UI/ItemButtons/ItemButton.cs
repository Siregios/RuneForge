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
            if (ItemCount == int.MaxValue)
                this.countText.text = "∞";
            else
                this.countText.text = "x" + ItemCount.ToString();
        }
        //if (ItemCount <= 0)
        //{
        //    this.GetComponent<Button>().interactable = false;
        //    this.transform.Find("Icon").GetComponent<Image>().color = new Color(this.transform.Find("Icon").GetComponent<Image>().color.r,
        //        this.transform.Find("Icon").GetComponent<Image>().color.g,
        //        this.transform.Find("Icon").GetComponent<Image>().color.b,
        //        0.5f);
        //}
        //else
        //{
        //    this.GetComponent<Button>().interactable = true;
        //    this.transform.Find("Icon").GetComponent<Image>().color = new Color(this.transform.Find("Icon").GetComponent<Image>().color.r,
        //        this.transform.Find("Icon").GetComponent<Image>().color.g,
        //        this.transform.Find("Icon").GetComponent<Image>().color.b,
        //        1f);
        //}
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
        setAttributes();
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
                    Debug.Log("YOLOTWO");
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

    void DragCopy()
    {
        followingMouse = true;
        draggable = (GameObject)Instantiate(dragObject, this.transform.position, Quaternion.identity);
        draggable.transform.SetParent(gameObject.transform, false);
        draggable.GetComponent<Image>().sprite = gameObject.transform.Find("Icon").GetComponent<Image>().sprite;
        gameObject.GetComponent<Button>().interactable = false;
    }
    void setAttributes()
    {
        KeyValuePair<string, int> mainItemAttribute = new KeyValuePair<string, int>("", 0);
        KeyValuePair<string, int> secondaryItemAttribute = new KeyValuePair<string, int>("", 0);
        Dictionary<string, string> attributeToColor = new Dictionary<string, string>()
        {
            { "Fire", "red" },
            { "Water", "blue" },
            { "Earth", "yellow" },
            { "Air", "green" }
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

        if (attribute1 != null && mainItemAttribute.Key != "")
        {
            attribute1.sprite = Resources.Load<Sprite>(string.Format("ItemSprites/Charms/{0}_circle", attributeToColor[mainItemAttribute.Key]));
            attribute1.transform.Find("Text").GetComponent<Text>().text = mainItemAttribute.Value.ToString();
        }
        if (attribute2 != null && secondaryItemAttribute.Key != "")
        {
            attribute2.sprite = Resources.Load<Sprite>(string.Format("ItemSprites/Charms/{0}_circle", attributeToColor[secondaryItemAttribute.Key]));
            attribute2.transform.Find("Text").GetComponent<Text>().text = secondaryItemAttribute.Value.ToString();
        }
    }
}