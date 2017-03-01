using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTransition : MonoBehaviour
{
    GameObject center;
    RandomSpawnNote scriptNote;
    public GameObject accShow;
    int accuracy;
    int indexNote;
    float speed = 3.5f;
    void Start()
    {
        center = GameObject.Find("center");
        scriptNote = GameObject.Find("RandomSpawn").GetComponent<RandomSpawnNote>();
        indexNote = GetComponent<NoteTracker>().indexNote;
        accuracy = GetComponent<NoteTracker>().accuracy;
        GetComponent<NoteTracker>().enabled = false;
        StartCoroutine(IngToCauldron());
    }
    IEnumerator IngToCauldron()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        while (Vector3.Distance(transform.position, center.transform.position) > 0.3f)
        {
            transform.position = Vector3.Lerp(transform.position, center.transform.position, Time.deltaTime * speed * 3);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FadeObj());
    }

    IEnumerator FadeObj()
    {
        GameObject ez = Instantiate(accShow, accShow.transform.position, Quaternion.identity);
        checkAccuracy(ez);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        while (sprite.color.a > 0.1f)
        {
            Color temp = sprite.color;
            temp.a = Mathf.Lerp(temp.a, 0, Time.deltaTime * speed);
            sprite.color = temp;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    void checkAccuracy(GameObject change)
    {
        AccuracyShower script = change.GetComponent<AccuracyShower>();
        if (accuracy == 1)
        {
            script.GetComponent<SpriteRenderer>().sprite = script.great;
        }
        else if (accuracy == 2)
        {
            script.GetComponent<SpriteRenderer>().sprite = script.perfect;
        }
    }
}
