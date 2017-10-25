using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Obstacle : MonoBehaviour {

    public float speed;

    float leftConstraint;
    float rightConstraint;
    float bottomConstraint = 960.0f;
    float buffer = 0.5f; // set this so the obstacle disappears offscreen before re-appearing on other side
    Camera cam;
    float distanceZ;

    System_Gravity sgrav;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        sgrav = GameObject.FindObjectOfType<System_Gravity>();

        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
    }

    // Update is called once per frame
    void Update () {
        //Update Transform
        transform.Translate(0, -speed * Time.deltaTime, 0);
        transform.rotation = sgrav.transform.rotation;

        //Looping magic
        if (transform.position.x < leftConstraint - buffer)
            transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
        if (transform.position.x > rightConstraint + buffer)
            transform.position = new Vector3(leftConstraint - buffer, transform.position.y, transform.position.z);

        //Check if obstacle is off the bottom of the screen
        if (transform.position.y < bottomConstraint - buffer)
            Destroy(this.gameObject);
    }
}
