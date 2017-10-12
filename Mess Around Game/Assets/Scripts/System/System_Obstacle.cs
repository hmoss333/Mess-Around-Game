using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Obstacle : MonoBehaviour {

    public float speed;

    float bottomConstraint = 960.0f;
    float buffer = 1.0f; // set this so the obstacle disappears offscreen before re-appearing on other side
    Camera cam;
    float distanceZ;

    System_Gravity sgrav;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        sgrav = GameObject.FindObjectOfType<System_Gravity>();

        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(0, speed * sgrav.newDir.y * Time.deltaTime, 0);
        transform.rotation = sgrav.transform.rotation;

        //Check if obstacle is off the bottom of the screen
        if (transform.position.y < bottomConstraint - buffer)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Hit");
            StartCoroutine(ColorChange(col.GetComponent<SpriteRenderer>()));
        }
    }

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.tag == "PlayArea")
    //    {
    //        Debug.Log("Left area");
    //        Destroy(this.gameObject);
    //    }
    //}

    IEnumerator ColorChange (SpriteRenderer target)
    {
        target.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        target.color = Color.green;
    }
}
