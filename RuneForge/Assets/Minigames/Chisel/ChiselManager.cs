using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChiselManager : MonoBehaviour {

    public int score = 0;
    public Text scoreText;
    public GameObject map;

    int level = 0;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

      
	}

    void LateUpdate()
    {
        if (map != null && map.transform.childCount == 0)
        {
            score += 10;
            level++;
            Destroy(map);
        }
        scoreText.text = "Score: " + score;
    }
}
