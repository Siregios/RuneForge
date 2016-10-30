using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text timer;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }



}
