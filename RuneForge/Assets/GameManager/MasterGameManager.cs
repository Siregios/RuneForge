using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterGameManager : MonoBehaviour
{
    //Static instance of GameManager which allows it to be accessed by any other script.
    public static MasterGameManager instance = null;

    public List<string> minigameList = new List<string>();

    public ActionClock actionClock;
    public QuestGenerator questGenerator;
    public SceneManagerWrapper sceneManager;
    public InteractionManager interactionManager;
    public WorkOrderManager workOrderManager;
    public UIManager uiManager;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}