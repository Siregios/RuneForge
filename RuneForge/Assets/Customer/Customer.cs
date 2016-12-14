using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Interactable interactScript;

    void Start()
    {
        FadeIn();
    }

    public void Leave()
    {
        interactScript.active = false;
        FadeOut();
        Destroy(this.gameObject, 1.5f);
    }

    void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeAlpha(0, 1));
    }

    void FadeOut()
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
                spriteRenderer.color = new Color(1, 1, 1, timeElapsed / 1.5f);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (start > end + 0.01f)
            {
                timeElapsed += Time.deltaTime;
                spriteRenderer.color = new Color(1, 1, 1, 1 - (timeElapsed / 1.5f));
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
