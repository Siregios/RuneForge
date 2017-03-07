using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyShower : MonoBehaviour {
    public Sprite miss, great, perfect;
    private float start;
    void Start()
    {
        start = transform.position.y + 1.5f;
        StartCoroutine(BAMRESULTS());
    }

    IEnumerator BAMRESULTS()
    {
        while (start - transform.position.y > 0.2f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, start, 0), Time.deltaTime);
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.a = Mathf.Lerp(temp.a, 0, Time.deltaTime * 2);
            GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
