using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public float speed;
    public float force = 9.8f;

    //variables for Gravity Rotation
    public static float up;// = Mathf.Atan2(0f, Input.acceleration.x) * Mathf.Rad2Deg;
    public float rotay;// = up;
    float smoothTime = 0.3f;
    float smoothVelocity = 0.3f;

    //Vector2 dir;
    public Vector2 gravity;

    //Variables for Screen Looping
    float leftConstraint = 0.0f;
    float rightConstraint = 960.0f;
    float bottomConstraint = 0.0f;
    float topConstraint = 960.0f;
    float buffer = 1.0f; // set this so the spaceship disappears offscreen before re-appearing on other side
    Camera cam;
    float distanceZ;

    // Use this for initialization
    void Start () {
        gravity = new Vector2(0, force);

        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
    }
	
	// Update is called once per frame
	void Update () {
        //Movement controls; Not needed with rotating gravity
#if UNITY_EDITOR
        transform.Translate(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0); //for testing
#elif UNITY_ANDROID
        transform.Translate(Input.acceleration.x * speed * Time.deltaTime, 0, 0); //using tilt controls
#endif


        //Rotate Gravity
        //use x and y readings to read the up vector
        up = Mathf.Atan2(0f, Input.acceleration.x) * Mathf.Rad2Deg;
        //smooth the acceleration data (otherwise it will be very jaggy ^_^ but if you want absolute precision, just skip this line and transform.eulerAngles with up)
        rotay = Mathf.SmoothDampAngle(rotay, up, ref smoothVelocity, smoothTime);
        //transform
        transform.Rotate(transform.eulerAngles.x, transform.eulerAngles.y, rotay);
        //transform.Translate(gravity * Time.deltaTime); //ignores physics
        //transform.GetComponent<Rigidbody2D>().AddForce(gravity);


        //Looping magic
        if (transform.position.x < leftConstraint - buffer)
        {
            transform.position = new Vector3(rightConstraint + buffer, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightConstraint + buffer)
        {
            transform.position = new Vector3(leftConstraint - buffer, transform.position.y, transform.position.z);
        }
        if (transform.position.y < bottomConstraint - buffer)
        {
            transform.position = new Vector3(transform.position.x, topConstraint + buffer, transform.position.z);
        }
        if (transform.position.y > topConstraint + buffer)
        {
            transform.position = new Vector3(transform.position.x, bottomConstraint - buffer, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        //#if UNITY_EDITOR
        //        dir.x = 0f;
        //        dir.y = -force;
        //#elif UNITY_ANDROID
        //        dir.x = Input.acceleration.x;
        //        dir.y = Input.acceleration.y;
        //#endif
        //        Physics2D.gravity = dir + Vector2.down;
        //        gravity = Physics2D.gravity;
    }
}
