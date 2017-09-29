using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Obstacle : MonoBehaviour {

    public float speed;

    float bottomConstraint = 960.0f;
    float buffer = 1.0f; // set this so the spaceship disappears offscreen before re-appearing on other side
    Camera cam;
    float distanceZ;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(0, -speed * Time.deltaTime, 0);

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

    IEnumerator ColorChange (SpriteRenderer target)
    {
        target.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        target.color = Color.green;
    }
}
