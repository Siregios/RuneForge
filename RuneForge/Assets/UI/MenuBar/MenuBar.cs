using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuBar : MonoBehaviour {
    Text moneyText;

    void Awake()
    {
        moneyText = this.transform.FindChild("MoneyText").GetComponent<Text>();
    }

    void Update()
    {
        moneyText.text = PlayerInventory.inventory.GetItemCount("Money").ToString();
    }
}
