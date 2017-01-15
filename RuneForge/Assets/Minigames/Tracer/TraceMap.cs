using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceMap : MonoBehaviour {
    public List<DotController> dots;
    public int currentDot = 0;
    public bool onLastDot { get { return (currentDot == dots.Count - 1); } }

    public SpriteRenderer sphere, magicCircle;

    void Awake()
    {
        for (int i = 1; i < dots.Count; i++)
        {
            dots[i].gameObject.SetActive(false);
        }
    }

    public void ActivateNextDot()
    {
        //sphere.color = Color.Lerp(sphere.color, Color.red, 1 / dots.Count);
        //sphere.color -= new Color(0, 1.0f / dots.Count, 1.0f / dots.Count, 1);
        //Color goalColor = new Color(sphere.color.r, 1 - ((float)currentDot / (dots.Count - 1)), 1 - ((float)currentDot / (dots.Count - 1)));
        Color colorStep = ColorStep(Color.cyan);
        sphere.color = colorStep;
        //StartCoroutine(LerpColor(goalColor, .02f, .01f));
        currentDot++;
        dots[currentDot].gameObject.SetActive(true);
    }

    Color ColorStep(Color goalColor)
    {
        float step = (float)currentDot / (dots.Count - 1);
        return new Color(1 - (1 - goalColor.r)*step, 1 - (1 - goalColor.g)*step, 1 - (1 - goalColor.b)*step);
    }
}
