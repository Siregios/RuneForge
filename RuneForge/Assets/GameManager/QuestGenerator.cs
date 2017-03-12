using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestGenerator : MonoBehaviour {
    public List<Quest> currentQuests = new List<Quest>();
    public List<Quest> todaysQuests = new List<Quest>();

    void Awake()
    {
        GenerateQuests();
    }

    public void GenerateQuests()
    {
        todaysQuests.Clear();
        int questsToday;
        if (MasterGameManager.instance.upgradeManager.level3 == 2 || MasterGameManager.instance.upgradeManager.level3 == 3)
            questsToday = Random.Range(1, 4);
        else
            questsToday = Random.Range(0, 4);

        for (int i = 0; i < questsToday; i++)
        {
            todaysQuests.Add(new Quest());
        }
    }
}
