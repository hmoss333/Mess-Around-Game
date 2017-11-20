using UnityEngine;

public class DualJoystickPlayerController : MonoBehaviour
{
    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    public RightJoystick rightJoystick; // the game object containing the RightJoystick script
    public float moveSpeed = 6.0f; // movement speed of the player character
    //public int rotationSpeed = 8; // rotation speed of the player character
    //public Transform rotationTarget; // the game object that will rotate to face the input direction
    //public Animator animator; // the animator controller of the player character
    private Vector3 leftJoystickInput; // holds the input of the Left Joystick
    private Vector3 rightJoystickInput; // hold the input of the Right Joystick
    private Rigidbody rigidBody; // rigid body component of the player character

    Player_LineControl lc;

    void Start()
    {
        //if (transform.GetComponent<Rigidbody>() == null)
        //{
        //    Debug.LogError("A RigidBody component is required on this game object.");
        //}
        //else
        //{
        //    rigidBody = transform.GetComponent<Rigidbody>();
        //}

        if (leftJoystick == null)
        {
            Debug.LogError("The left joystick is not attached.");
        }

        if (rightJoystick == null)
        {
            Debug.LogError("The right joystick is not attached.");
        }

        //if (rotationTarget == null)
        //{
        //    Debug.LogError("The target rotation game object is not attached.");
        //}

        lc = GameObject.FindObjectOfType<Player_LineControl>();
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        // get input from both joysticks
        leftJoystickInput = leftJoystick.GetInputDirection();
        rightJoystickInput = rightJoystick.GetInputDirection();

        //float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        float yMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

        //float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        float yMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02

        //// if there is no input on the left joystick
        //if (leftJoystickInput == Vector3.zero)
        //{
        //    animator.SetBool("isRunning", false);
        //}
        //// if there is no input on the right joystick
        //if (rightJoystickInput == Vector3.zero)
        //{
        //    animator.SetBool("isAttacking", false);
        //}
  
        // if there is only input from the left joystick
        if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero)
        {
            //// calculate the player's direction based on angle
            //float tempAngle = Mathf.Atan2(yMovementLeftJoystick, 0);// xMovementLeftJoystick);
            //xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
            //yMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

            leftJoystickInput = new Vector3(lc.line.GetPosition(0).x, yMovementLeftJoystick, 0);
            leftJoystickInput = transform.TransformDirection(leftJoystickInput);
            leftJoystickInput *= moveSpeed;

            // rotate the player to face the direction of input
            //Vector3 temp = transform.position;
            //temp.x += xMovementLeftJoystick;
            //temp.y += yMovementLeftJoystick;
            //Vector3 lookDirection = temp - transform.position;
            //if (lookDirection != Vector3.zero)
            //{
            //    rotationTarget.localRotation = Quaternion.Slerp(rotationTarget.localRotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
            //}
            //if (animator != null)
            //{
            //    animator.SetBool("isRunning", true);
            //}

            // move the player
            //rigidBody.transform.Translate(leftJoystickInput * Time.fixedDeltaTime);
            if (leftJoystickInput.y > rightJoystickInput.y)
            {
                if (leftJoystickInput.y < rightJoystickInput.y + lc.offset)
                    lc.leftPoint.y += leftJoystickInput.y * Time.deltaTime;
            }
        }

        // if there is only input from the right joystick
        if (leftJoystickInput == Vector3.zero && rightJoystickInput != Vector3.zero)
        {
            // calculate the player's direction based on angle
            //float tempAngle = Mathf.Atan2(yMovementRightJoystick, 0);
            //xMovementRightJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
            //yMovementRightJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

            rightJoystickInput = new Vector3(lc.line.GetPosition(1).x, yMovementRightJoystick, 0);
            rightJoystickInput = transform.TransformDirection(rightJoystickInput);
            rightJoystickInput *= moveSpeed;

            // rotate the player to face the direction of input
            //Vector3 temp = transform.position;
            //temp.x += xMovementRightJoystick;
            //temp.y += yMovementRightJoystick;
            //Vector3 lookDirection = temp - transform.position;
            //if (lookDirection != Vector3.zero)
            //{
            //    rotationTarget.localRotation = Quaternion.Slerp(rotationTarget.localRotation, Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, 45f, 0), rotationSpeed * Time.deltaTime);
            //}

            //animator.SetBool("isAttacking", true);
            if (rightJoystickInput.y > leftJoystickInput.y)
            {
                if (rightJoystickInput.y < leftJoystickInput.y + lc.offset)
                    lc.rightPoint.y += rightJoystickInput.y * Time.deltaTime;
            }
        }

        // if there is input from both joysticks (Left And Right)
        if (leftJoystickInput != Vector3.zero && rightJoystickInput != Vector3.zero)
        {
            leftJoystickInput = new Vector3(lc.line.GetPosition(0).x, yMovementLeftJoystick, 0);
            leftJoystickInput = transform.TransformDirection(leftJoystickInput);
            leftJoystickInput *= moveSpeed;

            rightJoystickInput = new Vector3(lc.line.GetPosition(1).x, yMovementRightJoystick, 0);
            rightJoystickInput = transform.TransformDirection(rightJoystickInput);
            rightJoystickInput *= moveSpeed;

            //if (animator != null)
            //{
            //    animator.SetBool("isRunning", true);
            //}

            // move the player
            //rigidBody.transform.Translate(leftJoystickInput * Time.fixedDeltaTime);
            if (leftJoystickInput.y > rightJoystickInput.y)
            {
                if (leftJoystickInput.y < rightJoystickInput.y + lc.offset)
                    lc.line.SetPosition(0, leftJoystickInput);
                    //lc.leftPoint.y += leftJoystickInput.y * Time.deltaTime;
            }
            if (rightJoystickInput.y > leftJoystickInput.y)
            {
                if (rightJoystickInput.y < leftJoystickInput.y + lc.offset)
                    lc.line.SetPosition(1, rightJoystickInput);
                    //lc.rightPoint.y += rightJoystickInput.y * Time.deltaTime;
            }
        }
    }
}