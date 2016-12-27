using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour {
    public float fadeTime = 1.5f;
    SpriteRenderer spriteRenderer;
    Image image;

    void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        image = this.GetComponent<Image>();
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeAlpha(0, 1));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeAlpha(1, 0));
    }

    IEnumerator FadeAlpha(float start, float end)
    {
        float timeElapsed = 0;
        if (start < end)
        {
            while (start < end - 0.01f)
            {
                timeElapsed += Time.deltaTime;
                if (spriteRenderer != null)
                    spriteRenderer.color = new Color(1, 1, 1, timeElapsed / fadeTime);
                else if (image != null) 
                    image.color = new Color(1, 1, 1, timeElapsed / fadeTime);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (start > end + 0.01f)
            {
                timeElapsed += Time.deltaTime;
                if (spriteRenderer != null)
                    spriteRenderer.color = new Color(1, 1, 1, 1 - (timeElapsed / fadeTime));
                else if (image != null)
                    image.color = new Color(1, 1, 1, 1 - (timeElapsed / fadeTime));
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
