using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Gravity : MonoBehaviour {

    float force = 9.81f; //apply gravity
    public Vector2 newDir;

    //private Quaternion localRotation; // 
    //public float speed = 1.0f; // ajustable speed from Inspector in Unity editor

    // Use this for initialization
    void Start () {
        //localRotation = transform.rotation;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
#if UNITY_EDITOR
        newDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
#elif UNITY_ANDROID
        newDir = Quaternion.AngleAxis(Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward) * -Vector2.up;
#endif

        Physics2D.gravity = newDir * force;
    }

    void Update()
    {
        //localRotation.z += Input.acceleration.x * Time.deltaTime * speed;

        //transform.rotation = localRotation;
        //transform.rotation = Quaternion.AngleAxis(-Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward);
    }
}
