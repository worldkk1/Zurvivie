using UnityEngine;
using System.Collections;

public class ZombieCollectorScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Zombie")
        {
            target.gameObject.SetActive(false);
        }
    }
}
