using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Gravity : MonoBehaviour {

    float force = 9.81f; //apply gravity
    public Vector2 newDir;

    private Quaternion localRotation; // 
    public float smooth = 1.0f; // ajustable speed from Inspector in Unity editor
	private Quaternion smoothRot;

    // Use this for initialization
    void Start () {
        localRotation = transform.rotation;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		//smoothRot = Quaternion.Slerp (localRotation, Quaternion.AngleAxis (Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward), smooth * Time.deltaTime);
#if UNITY_EDITOR
		smoothRot = Quaternion.Slerp (localRotation, Quaternion.AngleAxis (Input.GetAxisRaw("Horizontal") * Mathf.Rad2Deg, Vector3.forward), smooth * Time.deltaTime);
		newDir = new Vector2(Input.GetAxisRaw("Horizontal"), -1f);
#elif UNITY_ANDROID
		smoothRot = Quaternion.Slerp (localRotation, Quaternion.AngleAxis (Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward), smooth * Time.deltaTime);
		newDir = smoothRot * -Vector2.up;
#endif

		Physics2D.gravity = newDir * force;
		transform.rotation = smoothRot;
		//transform.Rotate (new Vector3(0, 0, newDir.x));

		localRotation = transform.rotation;
    }

    void Update()
    {
        //localRotation.z += Input.acceleration.x * Time.deltaTime * speed;

        //transform.rotation = localRotation;
        //transform.rotation = Quaternion.AngleAxis(-Input.acceleration.x * Mathf.Rad2Deg, Vector3.forward);
    }
}
