using UnityEngine;
using System.Collections;

public class MinigameButton : MonoBehaviour {
    public void LoadMinigame(string sceneName)
    {
        MasterGameManager.instance.actionClock.SpendAction();
        MasterGameManager.instance.sceneManager.LoadScene(sceneName);
    }
}
