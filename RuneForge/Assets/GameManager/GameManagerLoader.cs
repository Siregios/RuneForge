using UnityEngine;
using System.Collections;

public class GameManagerLoader : MonoBehaviour {
    public GameObject gameManager;

    void Awake()
    {
        if (MasterGameManager.instance == null)
            Instantiate(gameManager);
    }
}