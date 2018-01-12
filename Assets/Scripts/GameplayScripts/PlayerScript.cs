using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    //public float speed = 6.0f;
    public float speed = 2.0f;
    public float maxVelocity = 3.0f;

    private Animator animator;

    public Vector3 boundaries;

    //the variable for check is player attacked by zombie
    public static bool isPlayerAttacked;

    public static int currentScore;
    int countScore;

    bool isGameStart;

    GameObject showScore;

	// Use this for initialization
	void Start () {

        boundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        animator = GetComponent<Animator>();

        isPlayerAttacked = false;

        currentScore = 0;

        showScore = GameObject.FindGameObjectWithTag("ShowScore");
        //showScore.SetActive(false);

        Time.timeScale = 0.0f;

        isGameStart = true;
    }
	
	// Update is called once per frame
	void Update () {

        moveBykeyboard();
        moveByTouch();

        countScore = (int)Time.timeSinceLevelLoad;
        if (countScore != currentScore)
        {
            currentScore = countScore;
            Debug.Log(currentScore);
        }

        if (Time.timeScale == 0.0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isGameStart)
                {
                    isGameStart = false;
                    showScore.SetActive(false);
                } else
                {
                    Application.LoadLevel(0);
                }
                
                Time.timeScale = 1.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    void LateUpdate()
    {
        CheckBounds();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "BoundUp")
        {
            Vector3 temp = transform.position;
            temp.y = target.transform.position.y - 0.5f;
            transform.position = temp;
            //Debug.Log("KKK");
        }
        if (target.tag == "BoundDown")
        {
            Vector3 temp = transform.position;
            temp.y = target.transform.position.y + 0.5f;
            transform.position = temp;
            //Debug.Log("KKK");
        }

        if (target.tag == "Zombie")
        {
            StartCoroutine(DelayForPlayerAttacked());
        }
    }

    void CheckBounds()
    {
        if (transform.position.x > boundaries.x)
        {
            Vector3 temp = transform.position;
            temp.x = boundaries.x;
            transform.position = temp;
        }
        if (transform.position.x < (-boundaries.x))
        {
            Vector3 temp = transform.position;
            temp.x = -boundaries.x;
            transform.position = temp;
        }
    }

    void moveByTouch()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            ////transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

            var move = new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 0);
            transform.position += move * speed * Time.deltaTime;

            animator.SetInteger("Walk", 1);
            //Debug.Log("Touched x "+ pointer_x + ", y "+ pointer_y);

        }


    }

    void moveBykeyboard()
    {

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            animator.SetInteger("Walk", 0);
        } else
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * speed * Time.deltaTime;

            animator.SetInteger("Walk", 1);
        }

        //var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //transform.position += move * speed * Time.deltaTime;

    }

    //***** method for check is player attacked by zombie because when player touch inside zombie. It calling the method same time then we will delay for player side
    IEnumerator DelayForPlayerAttacked()
    {
        yield return new WaitForSeconds(0.1f);
        if (isPlayerAttacked)
        {
            Time.timeScale = 0.0f;
            Debug.Log("Dead");
            showScore.SetActive(true);
            showScore.GetComponent<GUIText>().text = "You can survival " + currentScore + " seconds\n"+"Tap the screen for restart game";
            

        }
    }
}
