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
        //Temporary to test
        for (int i = 0; i < 5; i++)
        {
            todaysQuests.Add(new Quest());
        }
    }
}
