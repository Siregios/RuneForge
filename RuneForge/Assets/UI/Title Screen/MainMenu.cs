﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public void NewGame()
    {
        MasterGameManager.instance.sceneManager.LoadScene("WorkshopTutorialPt1");
    }

    public void LoadGame()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}