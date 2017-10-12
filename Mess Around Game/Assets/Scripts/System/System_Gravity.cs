using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Gravity : MonoBehaviour {

    public float force = 9.81f; //apply gravity
    public Vector2 newDir;

    private Quaternion localRotation; // 
    public float smooth = 1.0f; // ajustable speed from Inspector in Unity editor
	private Quaternion smoothRot;

    GameObject target;
    float inputSpeed;

    // Use this for initialization
    void Start () {
        //localRotation = transform.rotation;

        target = GameObject.Find("Main Camera");
        localRotation = target.transform.rotation;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
#if UNITY_EDITOR
        inputSpeed = Input.GetAxisRaw("Horizontal");
#elif UNITY_ANDROID
		inputSpeed = Input.acceleration.x;            
#endif

        if (inputSpeed > 1f)
            inputSpeed = 1f;
        else if (inputSpeed < -1)
            inputSpeed = -1f;

        smoothRot = Quaternion.Slerp(localRotation, Quaternion.AngleAxis(inputSpeed * Mathf.Rad2Deg, Vector3.forward), smooth);
        newDir = smoothRot * -Vector2.up;

        Physics2D.gravity = newDir * force;
        //transform.rotation = smoothRot;

        if (newDir.sqrMagnitude > 1)
            newDir.Normalize();

        transform.Rotate (new Vector3(0, 0, newDir.x));
        //target.transform.Rotate(new Vector3(0, 0, newDir.x));
    }

    void Update()
    {
        //localRotation.z += Input.acceleration.x * Time.deltaTime * speed;

        //transform.rotation = localRotation;
        //transform.rotation = Quaternion.AngleAxis(-Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward);
    }
}
