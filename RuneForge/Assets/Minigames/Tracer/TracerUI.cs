using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TracerUI : MonoBehaviour {

    TracerManager GM;
    Text scoreText;

    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<TracerManager>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = "Score: " + GM.score;
    }
}