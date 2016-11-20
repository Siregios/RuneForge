using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
    public ChiselManager manager;

	void OnMouseDown()
    {
        manager.score += 10;
        Destroy(this.gameObject);

    }
}
