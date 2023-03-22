using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_LineControl : MonoBehaviour {

    public Vector2 leftPoint;
    public Vector2 rightPoint;
    public LineRenderer line; //reference to LineRenderer component
    public Material material; //assign a material to the Line Renderer

    Slider leftSlider;
    Slider rightSlider;
    [SerializeField] float leftValue;
    [SerializeField] float rightValue;
    [SerializeField] bool leftMatch;
    [SerializeField] bool rightMatch;
    float tempValLeft;
    float tempValRight;

    float scaler;
    public float offset = 1f;//0.05f;
    float leftRate = 0f;
    float rightRate = 0f;
    [SerializeField] float speed = 1f;

    BoxCollider2D col;

    System_GameManager gm;
    Camera cam;

    //points control the ends of the line
    //values corespond to the slider values

    // Use this for initialization
    void Start () {
        gm = GameObject.FindObjectOfType<System_GameManager>();
        cam = Camera.main;
        scaler = gm.height;

        leftSlider = GameObject.Find("LeftSlider").GetComponent<Slider>();
        rightSlider = GameObject.Find("RightSlider").GetComponent<Slider>();
        leftSlider.gameObject.SetActive(false);
        rightSlider.gameObject.SetActive(false);

        leftSlider.value = 0f;
        rightSlider.value = 0f;
        leftSlider.maxValue = scaler;
        rightSlider.maxValue = scaler;

        tempValLeft = leftValue;
        tempValRight = rightValue;

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

        if (GameObject.Find("Collider") == null)
        {
            Debug.Log("Added Collider");
            col = new GameObject("Collider").AddComponent<BoxCollider2D>();
            col.transform.parent = line.gameObject.transform; // Collider is added as child object of line
            col.gameObject.tag = "Line";
        }

        Coroutine co = StartCoroutine(ResetControls());
    }

    void Update()
    {
        //These lines are in Update because they need to be managed on each frame
        //and are not important to the physics engine

        //Values set equal to slider values
        leftValue = leftSlider.value;
        rightValue = rightSlider.value;

        //Round out value to improve response
        leftValue = Mathf.Round(leftValue * 100f) / 100f;
        rightValue = Mathf.Round(rightValue * 100f) / 100f;

        //This handles the left slider values to make sure that they 
        //cannot be moved beyond where the line can currently reach
        //Currently broken
        //if (leftValue > rightValue + offset)
        //{
        //    if (rightMatch)
        //    {
        //        leftSlider.maxValue = leftValue;
        //        leftSlider.minValue = rightValue - offset;
        //    }
        //}
        //else if (leftValue < rightValue - offset)
        //{
        //    if (rightMatch)
        //    {
        //        leftSlider.maxValue = rightValue + offset;
        //        leftSlider.minValue = leftValue;
        //    }
        //}

        //This handles the right slider values to make sure that they
        //cannot be moved beyond where the line can currently reach
        //Currently broken
        //if (rightValue > leftValue + offset)
        //{
        //    if (leftMatch)
        //    {
        //        rightSlider.maxValue = leftValue;
        //        rightSlider.minValue = rightValue - offset;
        //    }
        //}
        //else if (rightValue < leftValue - offset)
        //{
        //    if (leftMatch)
        //    {
        //        rightSlider.maxValue = rightValue + offset;
        //        rightSlider.minValue = leftValue;
        //    }
        //}


        //Experiment; Technically works, but is very buggy/not intuitive
        //if (leftValue > 0)
        //    leftSlider.minValue = rightValue - offset;
        //else
        //    leftSlider.minValue = 0f;
        //leftSlider.maxValue = rightValue + offset;

        //if (rightValue > 0)
        //    rightSlider.minValue = leftValue - offset;
        //else
        //    rightSlider.minValue = 0f;
        //rightSlider.maxValue = leftValue + offset;
    }

    void FixedUpdate ()
    {   
        //If game is running...
        if (!gm.gameOver)
        {
            //This sets left point movement based on relation to left slider
            if (leftRate == 0f)
            {
                if (!leftMatch)
                {
                    if (leftPoint.y < leftValue)
                        leftRate = 1f;
                    else if (leftPoint.y > leftValue)
                        leftRate = -1f;
                    else
                        leftRate = 0f;
                    tempValLeft = leftValue;
                }
                else
                {
                    if (tempValLeft != leftValue)
                        leftMatch = false;
                }
            }
            else if (leftRate == 1f)
            {
                if (leftPoint.y > leftValue)
                {
                    leftRate = 0f;
                    leftMatch = true;
                }
            }
            else if (leftRate == -1f)
            {
                if (leftPoint.y < leftValue)
                {
                    leftRate = 0f;
                    leftMatch = true;
                }
            }

            //This sets right point movement based on relation to right slider
            if (rightRate == 0f)
            {
                if (!rightMatch)
                {
                    if (rightPoint.y < rightValue)
                        rightRate = 1f;
                    else if (rightPoint.y > rightValue)
                        rightRate = -1f;
                    else
                        rightRate = 0f;
                    tempValRight = rightValue;
                }
                else
                {
                    if (tempValRight != rightValue)
                        rightMatch = false;
                }
            }
            else if (rightRate == 1f)
            {
                if (rightPoint.y > rightValue)
                {
                    rightRate = 0f;
                    rightMatch = true;
                }
            }
            else if (rightRate == -1f)
            {
                if (rightPoint.y < rightValue)
                {
                    rightRate = 0f;
                    rightMatch = true;
                }
            }

            //Left and right point updated every frame
            leftPoint.y += leftRate * speed * Time.deltaTime;
            rightPoint.y += rightRate * speed * Time.deltaTime;

            //Round out value to improve response
            leftPoint.y = Mathf.Round(leftPoint.y * 100f) / 100f;
            rightPoint.y = Mathf.Round(rightPoint.y * 100f) / 100f;

            //Set line position/collider
            line.SetPosition(0, leftPoint);
            line.SetPosition(1, rightPoint);
            UpdateCollider(line.gameObject, leftPoint, rightPoint);

            CheckScreenPosition(leftPoint, rightPoint);
        }
        //Turn off sliders if game is over
        else
        {
            leftSlider.gameObject.SetActive(false);
            rightSlider.gameObject.SetActive(false);
        }
    }

    void UpdateCollider(GameObject lineObject, Vector3 startPos, Vector3 endPos)
    {
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

    void CheckScreenPosition (Vector3 left, Vector3 right)
    {
        float checkHeight = Screen.height / 2;
        float leftHeight = cam.WorldToScreenPoint(left).y;
        float rightHeight = cam.WorldToScreenPoint(right).y;
        
        if (leftHeight >= checkHeight)
        {
            Debug.Log("Left at max");
            leftRate = 0;
        }
        else if (rightHeight >= checkHeight)
        {
            Debug.Log("Right at max");
            rightRate = 0;
        }
    }

    void RotateStick ()
    {
        
    }
}
