using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_CameraFollow : MonoBehaviour {

    // The target we are following
    public Transform target;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we 
    public float heightDamping = 2.0f;

    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target) return;

        // Calculate the current rotation angles
        float wantedHeight = target.position.y + height;

        float currentHeight = transform.position.y;

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, -10f);
    }
}
