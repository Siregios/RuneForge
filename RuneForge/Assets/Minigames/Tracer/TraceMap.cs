using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceMap : MonoBehaviour
{
    public List<DotController> dots;
    public int currentDot = 0;
    public bool onLastDot { get { return (currentDot == dots.Count - 1); } }
    public int dotsHit = 0;
    public SpriteRenderer sphere, magicCircle;
    public Color goalColor;

    void Awake()
    {
        for (int i = 1; i < dots.Count; i++)
        {
            dots[i].gameObject.SetActive(false);
        }
    }

    void Start()
    {
        magicCircle.color = new Color(1, 1, 1, (float)1 / (dots.Count - 1));
    }

    public void ActivateNextDot()
    {
        

        currentDot++;

        //AdvanceSprites();
        dots[currentDot].gameObject.SetActive(true);

    }

    public void AdvanceSprites()
    {
        sphere.color = ColorStep(goalColor);
        magicCircle.color = AlphaStep();
    }

    Color ColorStep(Color goalColor)
    {
        float step = (float)dotsHit / (dots.Count - 1);
        return new Color(1 - (1 - goalColor.r) * step, 1 - (1 - goalColor.g) * step, 1 - (1 - goalColor.b) * step);
    }

    Color AlphaStep()
    {
        float step = (float)dotsHit / (dots.Count - 1);
        return new Color(1, 1, 1, step);
    }
}
