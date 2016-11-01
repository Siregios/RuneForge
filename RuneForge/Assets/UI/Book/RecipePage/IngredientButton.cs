using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngredientButton : MonoBehaviour {
    public Item item;

    public void Initialize(Item item)
    {
        this.item = item;
        this.GetComponent<Image>().sprite = item.icon;
    }

    public void OnClick()
    {
        PlayerInventory.inventory.AddItem(this.item.name);

        //Destroy(this.gameObject);   //Probably tell the recipe panel to kill me so it can also clean its list.
    }
}
