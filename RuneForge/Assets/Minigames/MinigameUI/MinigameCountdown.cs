using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinigameCountdown : MonoBehaviour {

    int max = 3;
    public GameObject[] activate;
    public AudioSource[] countdown;
	void Start () {
        StartGame(false);
        StartCoroutine(Countdown(max));
	}

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        while (count > -1)
        {
            if (count == 3)
                countdown[0].Play();
            if (count == 2)
                countdown[1].Play();
            if (count == 1)
                countdown[2].Play();
            if (count == 0)
            {
                GetComponent<Text>().text = "Go!";
                countdown[0].PlayScheduled(.15f);
                countdown[1].Play();
                countdown[2].Play();
            }
            else
                GetComponent<Text>().text = count.ToString();
            count--;
            yield return new WaitForSeconds(1f);
        }
        StartGame(true);
        gameObject.SetActive(false);
    }

    void StartGame(bool set)
    {
        foreach (GameObject a in activate)
        {
            foreach (Transform child in a.transform)
            {
                if (child.GetComponent<MonoBehaviour>() != null && child.GetComponent<Text>() == null)
                    child.GetComponent<MonoBehaviour>().enabled = set;
            }
            foreach (MonoBehaviour s in a.GetComponents<MonoBehaviour>())
                s.enabled = set;
        }
    }
}
