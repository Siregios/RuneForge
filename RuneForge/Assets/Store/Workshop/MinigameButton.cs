using UnityEngine;
using System.Collections;

public class MinigameButton : MonoBehaviour {
    public void LoadMinigame(string sceneName)
    {
        // Prevent loading multiple scenes as well as decrementing multiple times when another scene is loading.
        if (!MasterGameManager.instance.sceneManager.loadingScene)
        {
            MasterGameManager.instance.actionClock.SpendAction();
            MasterGameManager.instance.sceneManager.LoadScene(sceneName);
        }
    }
}
