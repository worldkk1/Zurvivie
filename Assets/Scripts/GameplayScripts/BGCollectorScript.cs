using UnityEngine;
using System.Collections;

public class BGCollectorScript : MonoBehaviour {

	
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Background")
        {
            target.gameObject.SetActive(false);
        }
    }
}
