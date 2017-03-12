using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuBar : MonoBehaviour {
    public Text moneyText;
    public Text dayText;
    public Text actionText;
    //public Button endDayButton;
    public Image clockHand;

    void Awake()
    {
        //endDayButton.gameObject.SetActive(false);
    }

    void Update()
    {
        moneyText.text = PlayerInventory.money.ToString();
        dayText.text = "Day: " + MasterGameManager.instance.actionClock.Day;

        int actionCount = MasterGameManager.instance.actionClock.ActionCount;
        actionText.text = string.Format("Actions: {0}/{1}", actionCount, MasterGameManager.instance.actionClock.ActionsPerDay);
        clockHand.rectTransform.rotation = Quaternion.Euler(0, 0, (float)MasterGameManager.instance.actionClock.ActionCount / MasterGameManager.instance.actionClock.ActionsPerDay * 360);
        //if (actionCount == 0)
        //{
        //    endDayButton.gameObject.SetActive(true);
        //}
        //else
        //{
        //    endDayButton.gameObject.SetActive(false);
        //}

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndDay();
        }
    }

    public void EndDay()
    {
        MasterGameManager.instance.actionClock.EndDay();
    }
}
