using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_LineControl : MonoBehaviour {

    public Vector2 leftPoint;
    public Vector2 rightPoint;
    private LineRenderer line; //reference to LineRenderer component
    public Material material; //assign a material to the Line Renderer in the 

    public Slider leftSlider;
    public Slider rightSlider;
    float scaler;
    public float offset = 0.01f;

    System_GameManager gm;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindObjectOfType<System_GameManager>();
        scaler = gm.height;

        leftSlider = GameObject.Find("LeftSlider").GetComponent<Slider>();
        rightSlider = GameObject.Find("RightSlider").GetComponent<Slider>();
        leftSlider.gameObject.SetActive(false);
        rightSlider.gameObject.SetActive(false);

        leftSlider.value = 0f;
        rightSlider.value = 0f;
        leftPoint = new Vector2(-5, 0);
        rightPoint = new Vector2(5, 0);

        //Make a new line
        if (line == null)
        {
            //create the line
            //create a new empty gameobject and line renderer component
            line = new GameObject("Line").AddComponent<LineRenderer>();
            line.gameObject.tag = "Line";
            line.gameObject.layer = 11; //set to laser level
                                        //assign the material to the line
            line.material = material;
            //set the number of points to the line
            line.positionCount = 2;
            //set the width
            line.startWidth = 0.5f;
            line.endWidth = 0.5f;
            //render line to the world origin and not to the object's position
            line.useWorldSpace = false;
        }

        line.SetPosition(0, leftPoint);
        line.SetPosition(1, rightPoint);

        Coroutine co = StartCoroutine(ResetControls());
    }
	
	// Update is called once per frame
	void Update () {
        if (!gm.gameOver)
        {
            if (leftSlider.value < rightSlider.value + offset && leftSlider.value > rightSlider.value - offset)
            {
                leftPoint = new Vector2(leftPoint.x, leftSlider.value * scaler); //.Set(leftPoint.x, leftSlider.value * scaler);
                line.SetPosition(0, leftPoint);
            }
            else
            {
                leftSlider.value = leftPoint.y / scaler;
                line.SetPosition(0, leftPoint);
            }

            if (rightSlider.value < leftSlider.value + offset && rightSlider.value > leftSlider.value - offset)
            {
                rightPoint = new Vector2(rightPoint.x, rightSlider.value * scaler);
                line.SetPosition(1, rightPoint);
            }
            else
            {
                rightSlider.value = rightPoint.y / scaler;
                line.SetPosition(1, rightPoint);
            }

            AddBoxColliderToLine(line.gameObject, leftPoint, rightPoint);
        }
        else
        {
            leftSlider.gameObject.SetActive(false);
            rightSlider.gameObject.SetActive(false);
        }
    }

    void AddBoxColliderToLine(GameObject lineObject, Vector3 startPos, Vector3 endPos)
    {
        BoxCollider2D col;
        if (GameObject.Find("Collider") == null)
        {
            Debug.Log("Added Collider");
            col = new GameObject("Collider").AddComponent<BoxCollider2D>();
            col.transform.parent = lineObject.transform; // Collider is added as child object of line
            col.gameObject.tag = "Line";
        }

        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        GameObject.Find("Collider").GetComponent<BoxCollider2D>().size = new Vector3(lineLength, line.startWidth, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (startPos + endPos) / 2;
        GameObject.Find("Collider").GetComponent<BoxCollider2D>().transform.position = midPoint; // setting position of collider object

        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x)); //problem line
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle); //adjusts angle set speed
        GameObject.Find("Collider").GetComponent<BoxCollider2D>().transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator ResetControls ()
    {
        yield return new WaitForSeconds(0.01f);
        leftSlider.gameObject.SetActive(true);
        rightSlider.gameObject.SetActive(true);
    }
}
