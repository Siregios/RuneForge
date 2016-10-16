using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TracerUI : MonoBehaviour {

    TracerManager GM;
    Text scoreText;

    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<TracerManager>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = "x" + GM.score;
    }
}