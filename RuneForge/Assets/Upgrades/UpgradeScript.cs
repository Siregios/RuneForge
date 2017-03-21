using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour {

    public Text levelText;
    public Image expBar;

    public int tier = 0;
    public int upgrade = 0;

    public Text UpgradeName;
    public Text Description;
    public Text Cost;
    public Button UpgradeButton;

    public GameObject DescriptionShow;

    public AudioClip clickSound;
    public AudioClip buySound;

    void FixedUpdate()
    {
        DescriptionShow.SetActive(tier > 0);
        SetExpBar();
        switch (tier)
        {
            case 1:
                UpgradeButton.interactable = (PlayerInventory.money >= MasterGameManager.instance.upgradeManager.l1cost && MasterGameManager.instance.upgradeManager.level1 != 3 && MasterGameManager.instance.upgradeManager.level1 != upgrade);
                break;
            case 2:
                UpgradeButton.interactable = (PlayerInventory.money >= MasterGameManager.instance.upgradeManager.l2cost && MasterGameManager.instance.upgradeManager.level2 != 3 && MasterGameManager.instance.upgradeManager.level2 != upgrade);
                break;
            case 3:
                UpgradeButton.interactable = (PlayerInventory.money >= MasterGameManager.instance.upgradeManager.l3cost && MasterGameManager.instance.upgradeManager.level3 != 3 && MasterGameManager.instance.upgradeManager.level3 != upgrade);
                break;
            case 4:
                UpgradeButton.interactable = (PlayerInventory.money >= MasterGameManager.instance.upgradeManager.l4cost && MasterGameManager.instance.upgradeManager.level4 != 3 && MasterGameManager.instance.upgradeManager.level4 != upgrade);
                break;
            case 5:
                UpgradeButton.interactable = (PlayerInventory.money >= MasterGameManager.instance.upgradeManager.l5cost && MasterGameManager.instance.upgradeManager.level5 != 3 && MasterGameManager.instance.upgradeManager.level5 != upgrade);
                break;
            case 6:
                UpgradeButton.interactable = (PlayerInventory.money >= MasterGameManager.instance.upgradeManager.omnicost && !MasterGameManager.instance.upgradeManager.omni);
                break;
            default:
                UpgradeButton.interactable = false;
                break;
        }
    }

    public void Clear()
    {
        tier = 0;
    }


    public void setUpgrade()
    {
        MasterGameManager.instance.upgradeManager.tier = tier;
        MasterGameManager.instance.upgradeManager.upgrade = upgrade;
        MasterGameManager.instance.upgradeManager.setUpgrade();
        MasterGameManager.instance.audioManager.PlaySFXClip(buySound);
    }

    public void setDescription(int temp)
    {
        bool omni = MasterGameManager.instance.upgradeManager.omni;
        tier = temp / 10;
        upgrade = temp % 10;
        MasterGameManager.instance.audioManager.PlaySFXClip(clickSound);
        switch (tier)
        {
            case 1:
                if (upgrade == 1)
                {
                    UpgradeName.text = "Fire and Water";
                    Description.text = "Harness the natural powers of fire and water. Whenever you create a work order for an item, it automatically gains a +1 bonus to its fire and water attributes.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l1cost.ToString();
                }
                else if(upgrade == 2)
                {
                    UpgradeName.text = "Air and Earth";
                    Description.text = "Harness the natural powers of air and earth. Whenever you create a work order for an item, it automatically gains a +1 bonus to its air and earth attributes.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l1cost.ToString();
                }
                break;
            case 2:
                if (upgrade == 1)
                {
                    UpgradeName.text = "Easy Standard";
                    Description.text = "Persistent practice at the magical arts allows you to achieve standard quality runes with less effort, allowing you to focus on crafting high quality and mastercraft items.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l2cost.ToString();
                }
                else if (upgrade == 2)
                {
                    UpgradeName.text = "More Actions";
                    Description.text = "By twisting the fabric of time slightly, you can wake up earlier in the day while still feeling rested, allowing you to do 2 more actions in a day.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l2cost.ToString();
                }
                break;
            case 3:
                if (upgrade == 1)
                {
                    UpgradeName.text = "Sell for More";
                    Description.text = "By improving your relationship and bussiness ties with Nolak the Merchant, you can sell items to him at a higher price.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l3cost.ToString();
                }
                else if (upgrade == 2)
                {
                    UpgradeName.text = "More Quests";
                    Description.text = "A global advertising campaign will allow you to bring in more customers on average every day.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l3cost.ToString();
                }
                break;
            case 4:
                if (upgrade == 1)
                {
                    UpgradeName.text = "Cheaper Crafts";
                    Description.text = "You leave nothing to waste. Through conservative efforts, you can craft items at a lower cost.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l4cost.ToString();
                }
                else if (upgrade == 2)
                {
                    UpgradeName.text = "Cheaper Rent";
                    Description.text = "By optimizing the magical pipelines within your house, you will be spending less money every day on rent.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l4cost.ToString();
                }
                break;
            case 5:
                if (upgrade == 1)
                {
                    UpgradeName.text = "Multi-Crafting";
                    Description.text = "Your masterful skills and improved equipment allows you to work on multiple crafts at the same time.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l5cost.ToString();
                }
                else if (upgrade == 2)
                {
                    UpgradeName.text = "Easy HQ/MC";
                    Description.text = "Your masterful skills and refined magic allows you to craft high quality and mastercraft items with greater ease.";
                    Cost.text = MasterGameManager.instance.upgradeManager.l5cost.ToString();
                }
                break;
            case 6:
                UpgradeName.text = "Omni-Skills";
                Description.text = "The ultimate power: you will have access to both upgrades in each upgrade tier with this ability.";
                Cost.text = MasterGameManager.instance.upgradeManager.omnicost.ToString();
                break;
            default:
                break;
        }
    }

    void SetExpBar()
    {
        PlayerStats playerStats = MasterGameManager.instance.playerStats;
        if (levelText != null)
            levelText.text = playerStats.level.ToString();
        if (expBar != null)
        {
            int experience = playerStats.currentExperience - playerStats.previousLevelUp();
            int experienceDelta = playerStats.nextLevelUp() - playerStats.previousLevelUp();
            float expPercentage = (float)experience / (float)experienceDelta;
            expBar.fillAmount = Mathf.Clamp(expPercentage, 0, 1);
        }
    }
}
