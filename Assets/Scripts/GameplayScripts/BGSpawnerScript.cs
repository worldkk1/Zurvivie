using UnityEngine;
using System.Collections;

public class BGSpawnerScript : MonoBehaviour {

    public GameObject[] background;
    public static float lastY;
    public static float currentY;

    // Use this for initialization
    void Start () {

        background = GameObject.FindGameObjectsWithTag("Background");
        lastY = background[0].transform.position.y;

        for (int i=1; i<background.Length; i++)
        {
            if (lastY > background[i].transform.position.y)
                lastY = background[i].transform.position.y;
        } 
	
	}
	
	void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Background")
        {
            currentY = target.transform.position.y;
            if (currentY == lastY)
            {

                Shuffle(background);

                Vector3 temp = target.transform.position;
                float height = ((BoxCollider2D)target).size.y;

                for (int i = 0; i < background.Length; i++)
                {

                    if (!background[i].activeInHierarchy)
                    {
                        temp.y -= height;
                        lastY = temp.y;

                        background[i].transform.position = temp;
                        background[i].SetActive(true);

                    }

                }

            }
        }
    }

    void Shuffle(GameObject[] array)
    {

        for (int i = 0; i < array.Length; i++)
        {

            GameObject temp = array[i];
            int random = Random.Range(i, array.Length);
            array[i] = array[random];
            array[random] = temp;

        }

    }

}
