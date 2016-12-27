using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour {
    public FadeEffect fadeEffect;
    public Interactable interactScript;
    public Text text;

    [HideInInspector]
    public Item item;
    [HideInInspector]
    public int count;

    void Awake()
    {
        SetItem(ItemCollection.itemDict["Water Rune"], 1);
    }
    

    void Start()
    {
        fadeEffect.FadeIn();
    }

    public void SetItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
        string s = (count > 1) ? "s" : "";
        text.text = string.Format("Can you make me {0} {1}{2}", count, item.name, s);
    }

    public void Leave()
    {
        interactScript.active = false;
        fadeEffect.FadeOut();
        Destroy(this.gameObject, 1.5f);
    }
}