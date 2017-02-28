using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList_ExtraFilter : MonoBehaviour {

    public string[] extraFilters;

    private ItemListUI itemList;

	// Use this for initialization
	void Start () {
        itemList = this.gameObject.GetComponent<ItemListUI>();
        applyExtraFilters();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void applyExtraFilters()
    {
        foreach (string filter in extraFilters)
            itemList.DisplayNewFilter(filter);
    }
}
