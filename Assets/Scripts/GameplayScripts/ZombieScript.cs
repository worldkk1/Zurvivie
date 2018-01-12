using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {

    public float speed = 0.5f;

    private Vector3 playerPosition;
    private GameObject playerGameObj;

    private float angleOfZombie = 0;
    private bool isPlayerInArea;

    private bool isTimer1sUp;

    private float lastPlayerAngle;

    private Animator animator;

    // Use this for initialization
    void Start () {

        //*****get player position
        playerGameObj = GameObject.FindGameObjectWithTag("Player");
        playerPosition = playerGameObj.transform.position;

        lastPlayerAngle = GetAngleInDeg(playerPosition);

        isPlayerInArea = false;
        isTimer1sUp = false;

        animator = GetComponent<Animator>();

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        //***** Player first time get in area of zombie
        if (!isPlayerInArea)
        {
            if (target.tag == "Player")
            {
                //Debug.Log("Crash-1");
                isPlayerInArea = true;

                //***** statement for first rotation
                float angle = GetAngleInDeg(playerPosition);
                lastPlayerAngle = angle;
                transform.Rotate(new Vector3(0, 0, angle));

                StartCoroutine(countdown1sec());
                //***** end of statement
            }
        } else //***** Player get in zombie's area and touch the zombie
        {
            if (target.tag == "Player")
            {
                PlayerScript.isPlayerAttacked = true;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D target)
    {
        //***** Player get out of zombie's area
        //if (isPlayerInArea)
        //{
        //    if (target.tag == "Player")
        //    {
        //        //Debug.Log("Exit-1");
        //        isPlayerInArea = false;
        //        animator.SetInteger("Walk", 0);
        //    }
        //}

        if (target.tag == "Player")
        {
            //Debug.Log("Exit-1");
            isPlayerInArea = false;
            animator.SetInteger("Walk", 0);
        }

    }


    // Update is called once per frame
    void Update () {

        //****** When player get in area, the zombie will move to player
        if (isPlayerInArea)
        {
            MoveToPlayer();

            if (isTimer1sUp)
            {
                //MoveToPlayer();
                float angle = GetAngleInDeg(playerPosition);

                if (angle != lastPlayerAngle) // for check angle of current player pos equal to last angle of player
                {
                    transform.Rotate(new Vector3(0, 0, -lastPlayerAngle));
                    transform.Rotate(new Vector3(0, 0, angle));
                    //Debug.Log(angle);
                }
                
                //Debug.Log(playerPosition.x+" "+playerPosition.y);
                isTimer1sUp = false;
                StartCoroutine(countdown1sec());
                lastPlayerAngle = angle;
            }

        }


	}

    //***** method for delay 1 sec of rotation of zombie
    private IEnumerator countdown1sec()
    {
        yield return new WaitForSeconds(1.0f);

        isTimer1sUp = true;
        playerPosition = playerGameObj.transform.position;
    }

    //******method for zombie move to player
    void MoveToPlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);

        animator.SetInteger("Walk", 1);
    }

    float GetAngleInDeg(Vector3 angle)
    {
        float angleInRad = Mathf.Atan2(angle.y - transform.position.y, angle.x - transform.position.x);
        float angleInDeg = angleInRad * 180 / Mathf.PI;

        return angleInDeg + 90;
    }
}
