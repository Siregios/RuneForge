using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishManager : MonoBehaviour
{
    public Timer timer;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.time <= 0f)
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
    }



}
