using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuBar : MonoBehaviour {
    public Text moneyText;
    public Text dayText;
    public Text actionText;
    public Image clockHand;

    void Update()
    {
        moneyText.text = PlayerInventory.money.ToString();
        dayText.text = "Day: " + MasterGameManager.instance.actionClock.Day;

        actionText.text = string.Format("Actions: {0}/{1}", MasterGameManager.instance.actionClock.ActionCount, MasterGameManager.instance.actionClock.ActionsPerDay);
        clockHand.rectTransform.rotation = Quaternion.Euler(0, 0, (float)MasterGameManager.instance.actionClock.ActionCount / MasterGameManager.instance.actionClock.ActionsPerDay * 360);
    }
}
