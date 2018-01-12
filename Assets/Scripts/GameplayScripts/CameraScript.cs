using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public float speed = 0.1f;
    public float acceleration = 0.01f;
    public float maxSpeed = 2.0f;

    public bool moveCamera;

	// Use this for initialization
	void Start () {

        moveCamera = true;

	}
	
	// Update is called once per frame
	void Update () {
	
        if (moveCamera)
        {
            MoveCamera();
        }

	}

    void MoveCamera()
    {
        Vector3 temp = transform.position;
        float oldY = temp.y;
        float newY = temp.y - (speed * Time.deltaTime);

        temp.y = Mathf.Clamp(temp.y, oldY, newY);
        transform.position = temp;
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed)
            speed = maxSpeed;
    }

}
