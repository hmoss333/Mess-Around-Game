using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Gravity : MonoBehaviour {

    public static System_Gravity instance;

    public float force = 9.81f; //apply gravity
    public Vector2 newDir;

    Quaternion localRotation; // 
    public float smooth = 1.0f; // ajustable speed from Inspector in Unity editor
	Quaternion smoothRot;

    GameObject target;
    float inputSpeed;

    Quaternion rotationMin;
    Quaternion rotationMax;
    Quaternion rotation;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start () {
        //localRotation = transform.rotation;

        target = GameObject.Find("Main Camera");
        localRotation = target.transform.rotation;

        rotationMin = Quaternion.Euler(new Vector3(0f, 0f, -10f));
        rotationMax = Quaternion.Euler(new Vector3(0f, 0f, 10f));

        rotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update()
    {
#if UNITY_EDITOR
        inputSpeed = Input.GetAxisRaw("Horizontal");
#elif UNITY_ANDROID
		inputSpeed = Input.acceleration.normalized.x;            
#endif

        if (inputSpeed >= 1f)
            inputSpeed = 1f;
        else if (inputSpeed <= -1)
            inputSpeed = -1f;

        smoothRot = Quaternion.Slerp(localRotation, Quaternion.AngleAxis(inputSpeed * Mathf.Rad2Deg, Vector3.forward), smooth);
        newDir = smoothRot * -Vector2.up;

        if (newDir.sqrMagnitude > 1)
            newDir.Normalize();

        Physics2D.gravity = newDir * force;

        if (inputSpeed > 0 )
        {
            if (rotation.z < rotationMax.z)
            {
                rotation.z += Quaternion.Euler(new Vector3(0f, 0f, inputSpeed * force * Time.deltaTime)).z;
            }
        }

        if (inputSpeed < 0)
        {
            if (rotation.z > rotationMin.z)
            {
                rotation.z -= Quaternion.Euler(new Vector3(0f, 0f, -inputSpeed * force * Time.deltaTime)).z;
            }
        }

        transform.rotation = rotation;
    }
}
