using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlyphsRemaining : MonoBehaviour {
    // Update is called once per frame
    public TracerManager gameManager;
	void Update () {
        gameObject.GetComponent<Text>().text = (gameManager.mapsPerPlay - gameManager.count).ToString();
	}
}
