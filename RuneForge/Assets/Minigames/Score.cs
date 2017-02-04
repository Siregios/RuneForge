using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int s = 0;
    public Text scoreText;

    void Update()
    {
        scoreText.text = s.ToString();
    }

    public void addScore(int add)
    {
        s += add;
    }

    public void subScore(int sub)
    {
        if (s - sub >= 0)
            s -= sub;        
    }
}
