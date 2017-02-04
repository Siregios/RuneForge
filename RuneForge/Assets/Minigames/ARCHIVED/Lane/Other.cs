using UnityEngine;
using System.Collections;

public class Other : MonoBehaviour {

	void Update () {
        if (Camera.main.WorldToViewportPoint(transform.position).y < -0.1   )
            Destroy(gameObject);
    }
}
