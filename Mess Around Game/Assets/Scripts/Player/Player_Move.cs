using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public float speed;

    private float leftConstraint; // = 0.0f;
    private float rightConstraint; // = Screen.width; //960.0f;
    //private float bottomConstraint; // = 0.0f;
    //private float topConstraint; // = Screen.height; //960.0f;
    private float buffer = 1f; // set this so the ball disappears offscreen before re-appearing on other side
    Camera cam;
    private float distanceZ;

    System_Gravity sgrav;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        //bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        //topConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;

        //sgrav = GameObject.FindObjectOfType<System_Gravity>();
    }
	
	// Update is called once per frame
	void Update () {
        //Looping magic
        //Left/Right
        if (transform.position.x < leftConstraint - buffer)
        {
            transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightConstraint + buffer)
        {
            transform.position = new Vector3(leftConstraint - buffer, transform.position.y, transform.position.z);
        }
        ////Top/Bottom
        //if (transform.position.y < (bottomConstraint * sgrav.newDir.y) - buffer)
        //{
        //    transform.position = new Vector3(transform.position.x, (topConstraint * sgrav.newDir.y) + buffer, transform.position.z);
        //}
        //if (transform.position.y > (topConstraint * sgrav.newDir.y) + buffer)
        //{
        //    transform.position = new Vector3(transform.position.x, (bottomConstraint * sgrav.newDir.y) - buffer, transform.position.z);
        //}
    }
}
