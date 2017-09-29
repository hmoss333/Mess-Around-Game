using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public float speed;

    float leftConstraint = 0.0f;
    float rightConstraint = 960.0f;
    float buffer = 1.0f; // set this so the spaceship disappears offscreen before re-appearing on other side
    Camera cam;
    float distanceZ;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        //bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        //topConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
    }
	
	// Update is called once per frame
	void Update () {
        //Looping magic
        if (transform.position.x < leftConstraint - buffer)
        {
            transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightConstraint + buffer)
        {
            transform.position = new Vector3(leftConstraint - buffer, transform.position.y, transform.position.z);
        }
        //if (transform.position.y < bottomConstraint - buffer)
        //{
        //    transform.position = new Vector3(transform.position.x, topConstraint + buffer, transform.position.z);
        //}
        //if (transform.position.y > topConstraint + buffer)
        //{
        //    transform.position = new Vector3(transform.position.x, bottomConstraint - buffer, transform.position.z);
        //}
    }
}
