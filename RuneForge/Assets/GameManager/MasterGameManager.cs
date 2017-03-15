using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterGameManager : MonoBehaviour
{
    //Static instance of GameManager which allows it to be accessed by any other script.
    public static MasterGameManager instance = null;

    //[System.Serializable]
    //public class Minigame
    //{
    //    public string Name;
    //    public int SD = 50;
    //    public int HQ = 400;
    //    public int MC = 500;
    //    public bool isTutorial = false;
    //}

    public List<string> minigameList;
    //public Dictionary<string, Minigame> minigameDict = new Dictionary<string, Minigame>();
    public int SDThreshold;
    public int HQThreshold;
    public int MCThreshold;

    public PlayerStats playerStats;
    public ActionClock actionClock;
    public QuestGenerator questGenerator;
    public SceneManagerWrapper sceneManager;
    public InteractionManager interactionManager;
    public WorkOrderManager workOrderManager;
    public UIManager uiManager;
    public SaveManager saveManager;
    public UpgradeManager upgradeManager;
    public StoreDayStats storeDayStats;

    public bool inputActive = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        //SetMinigameDict();
    }

    //void Start()
    //{
    //    //For testing
    //    this.saveManager.SaveData();
    //}

    void Update()
    {
        //For testing
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    this.saveManager.SaveData();
        //}
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    this.saveManager.LoadData();
        //}
        if (MasterGameManager.instance.upgradeManager.level2 == 1 || MasterGameManager.instance.upgradeManager.level2 == 3)
            SDThreshold = 800;
        else
            SDThreshold = 1000;
        if (MasterGameManager.instance.upgradeManager.level5 == 2 || MasterGameManager.instance.upgradeManager.level5 == 3)
        {
            HQThreshold = 1800;
            MCThreshold = 3500;
        }
        else
        {
            HQThreshold = 2000;
            MCThreshold = 4000;

        }
    }

    //void SetMinigameDict()
    //{
    //    foreach (Minigame minigame in minigameList)
    //    {
    //        minigameDict[minigame.Name] = minigame;
    //    }
    //}
}