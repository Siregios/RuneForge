using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneManagerWrapper : MonoBehaviour {

    Fader fader;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject faderGO = GameObject.Find("Fader");
        if (faderGO != null)
            fader = faderGO.GetComponent<Fader>();
    }

    public void LoadScene(string sceneName)
    {
        //if (fader != null)
        //{
        //    FadeToBlack();
        //    StartCoroutine(DelayedSceneLoad(sceneName, fader.fadeTime));
        //}
        //else
        //{
            SceneManager.LoadScene(sceneName);
        //}
    }

    public void FadeToBlack()
    {
        if (fader != null)
            fader.FadeToBlack();
    }

    IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName);
    }
}
