using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {


    //float leftConstraint; // = 0.0f;
    //float rightConstraint; // = Screen.width; //960.0f;
    float bottomConstraint;
    float buffer = 0.25f; // set this so the ball disappears offscreen before re-appearing on other side
    Camera cam;
    float distanceZ;

    System_GameManager gm;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        //leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        //rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;

        gm = GameObject.FindObjectOfType<System_GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        //Looping magic
        //if (transform.position.x < leftConstraint - buffer)
        //{
        //    transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
        //}
        //if (transform.position.x > rightConstraint + buffer)
        //{
        //    transform.position = new Vector3(leftConstraint - buffer, transform.position.y, transform.position.z);
        //}
        if (transform.position.y < bottomConstraint - buffer)
        {
            Debug.Log("dead");
            gm.GameOver();
            Destroy(this.gameObject);
        }
    }
}
