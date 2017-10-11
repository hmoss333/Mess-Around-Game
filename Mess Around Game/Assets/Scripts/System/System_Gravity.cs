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

    // Use this for initialization
    void Start () {
        //localRotation = transform.rotation;

        target = GameObject.Find("Main Camera");
        localRotation = target.transform.rotation;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		//smoothRot = Quaternion.Slerp (localRotation, Quaternion.AngleAxis (Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward), smooth * Time.deltaTime);
#if UNITY_EDITOR
		smoothRot = Quaternion.Slerp (localRotation, Quaternion.AngleAxis (Input.GetAxisRaw("Horizontal") * Mathf.Rad2Deg, Vector3.forward), smooth);
		//newDir = new Vector2(Input.GetAxisRaw("Horizontal"), -1f);
        newDir = smoothRot * Vector2.up;
#elif UNITY_ANDROID
		smoothRot = Quaternion.Slerp (localRotation, Quaternion.AngleAxis (Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward), smooth);
		//newDir = new Vector2(Input.acceleration.x, -1f);
        newDir = smoothRot * Vector2.up;
#endif

        //Physics2D.gravity = newDir * force;
        //transform.rotation = smoothRot;

        if (newDir.sqrMagnitude > 1)
            newDir.Normalize();

        //transform.Rotate (new Vector3(0, 0, newDir.x));
        target.transform.Rotate(new Vector3(0, 0, newDir.x));
    }

    void Update()
    {
        //localRotation.z += Input.acceleration.x * Time.deltaTime * speed;

        //transform.rotation = localRotation;
        //transform.rotation = Quaternion.AngleAxis(-Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward);
    }
}
