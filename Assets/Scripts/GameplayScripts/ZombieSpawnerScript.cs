using UnityEngine;
using System.Collections;

public class ZombieSpawnerScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] zombies;
    public float distanceBetweenZombie = 3.0f;
    public float minX, maxX;
    private PlayerScript playerScript;
    private GameObject[] zombiesInGame;
    private float lastZombiePositionY;

    float lastY;
    float currentY;

    private GameObject[] tempZombies;

    private int countWave;

    void Awake()
    {
        //lastY = BGSpawnerScript.lastY;
        //currentY = BGSpawnerScript.lastY;

        Vector3 t = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        maxX = t.x - 0.5f;
        minX = -t.x + 0.5f;

        float center = Screen.width / Screen.height;

        CreateZombie(center);

        countWave = 1;
    }

	// Use this for initialization
	void Start () {

        lastY = BGSpawnerScript.lastY;
        currentY = -40;

        tempZombies = zombies;

    }


    void CreateZombie(float positionY)
    {
        Shuffle(zombies);

        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i] = Instantiate(zombies[i], Vector3.zero, Quaternion.identity) as GameObject;
            Vector3 temp = zombies[i].transform.position;
            float randomX = Random.Range(minX, maxX);
            temp.x = randomX;
            temp.y = positionY;
            lastZombiePositionY = temp.y;
            zombies[i].transform.position = temp;
            positionY -= distanceBetweenZombie;
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

    // Update is called once per frame
    void Update () {
        lastY = BGSpawnerScript.lastY;
        if (lastY != currentY)
        {
            Vector3 temp = new Vector3(0,0,0);
            zombies = tempZombies;
            Shuffle(zombies);
            temp.y = lastY + 40;

            for (int i = 0; i < zombies.Length; i++)
            {

                    temp.x = Random.Range(minX, maxX);
                    //temp.y = lastY+40;
                    temp.y -= distanceBetweenZombie;

                    lastZombiePositionY = temp.y;

                    zombies[i].transform.position = temp;
                    zombies[i].SetActive(true);


            }

            for (int i = 0; i < countWave; i++)
            {
                zombies = tempZombies;
                Shuffle(zombies);
                temp.y = lastY + 40;

                for (int y = 0; y < zombies.Length; y++)
                {

                    temp.x = Random.Range(minX, maxX);
                    //temp.y = lastY+40;
                    temp.y -= distanceBetweenZombie;

                    lastZombiePositionY = temp.y;

                    zombies[i].transform.position = temp;
                    zombies[i].SetActive(true);


                }

            }

            countWave++;
            currentY = lastY;
            Debug.Log("End of currentY: lasty="+lastY+", currentY="+currentY);
        }

	}
}
