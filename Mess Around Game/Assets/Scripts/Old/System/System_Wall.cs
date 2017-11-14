using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Wall : MonoBehaviour {

    public float speed;

    float bottomConstraint = 960.0f;
    float buffer = 0.5f;
    Camera cam;
    float distanceZ;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
    }

    // Update is called once per frame
    void Update()
    {
        //Update Transform
        transform.Translate(0, -speed * Time.deltaTime, 0);

        //Check if obstacle is off the bottom of the screen
        if (transform.position.y < bottomConstraint - buffer)
            Destroy(this.gameObject);
    }
}
