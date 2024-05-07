using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 100f; // Speed variable
    public float jumpspeed = 100f; // Speed variable
    public Rigidbody rb; // Set the variable 'rb' as Rigibody
    public Vector3 movement; // Set the variable 'movement' as a Vector3 (x,y,z)
    public Vector3 slopeMovement; // Set the variable 'movement' as a Vector3 (x,y,z)
    public Vector3 worldInputMovement; // Set the variable 'movement' as a Vector3 (x,y,z)
    public float playerXrotation;
    private GameObject leg1target;
    private GameObject leg2target;
    private GameObject leg3target;
    private GameObject leg4target;
    private float leg1Ycoord;
    private float leg2Ycoord;
    private float leg3Ycoord;
    private float leg4Ycoord;
    private float leftLegYDiff;
    private float rightLegYDiff;
    private float totalLegYDiff;
    private bool onGround = false;
    private bool jump = false;
    float timer = 0;
    public Vector3 velocity;
    public CinemachineFreeLook cineCamera;



    // 'Start' Method run once at start for initialisation purposes
    void Start()
    {
        // find the Rigidbody of this game object and add it to the variable 'rb'
        rb = this.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        leg1target = GameObject.Find("Legs/Leg1_target");
        leg2target = GameObject.Find("Legs/Leg2_target");
        leg3target = GameObject.Find("Legs/Leg3_target");
        leg4target = GameObject.Find("Legs/Leg4_target");
    }



    // 'Update' Method is called once per frame
    void Update()
    {
        // In Update we get the Input for left, right, up and down and put it in the variable 'movement'...
        // We only get the input of x and z, y is left at 0 as it's not required
        // 'Normalized' diagonals to prevent faster movement when two inputs are used together
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        calculateLegAngle();
        //slopeMovement = Vector3.ProjectOnPlane(movement, )
        velocity = rb.velocity;
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            playerJump();
        }
        worldInputMovement = transform.TransformDirection(movement);
    }



    // 'FixedUpdate' Method is used for Physics movements
    void FixedUpdate()
    {
        moveCharacter(worldInputMovement); // We call the function 'moveCharacter' in FixedUpdate for Physics movement
        float XRotation = cineCamera.m_XAxis.Value;
        transform.eulerAngles = new Vector3(0, XRotation, 0);

    }



    // 'moveCharacter' Function for moving the game object
    void moveCharacter(Vector3 direction)
    {
        // We multiply the 'speed' variable to the Rigidbody's velocity...
        // and also multiply 'Time.fixedDeltaTime' to keep the movement consistant on all devices
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

    }

    void calculateLegAngle()
    {
        leg1Ycoord = leg1target.transform.position.y;
        leg2Ycoord = leg2target.transform.position.y;
        leg3Ycoord = leg3target.transform.position.y;
        leg4Ycoord = leg4target.transform.position.y;

        leftLegYDiff = Mathf.Abs(leg2Ycoord - leg1Ycoord);
        Vector3 targetDir = leg1target.transform.position - leg2target.transform.position;
        Vector3 forward = transform.up;
        float angle = Mathf.Abs(Vector3.SignedAngle(targetDir, forward, Vector3.forward)) - 90;
        Debug.Log("Angle: " + angle);

        /*if (leg2Ycoord != leg1Ycoord)
        {
            leftLegYDiff = Mathf.Abs(leg2Ycoord - leg1Ycoord);
            Vector3 targetDir = leg1target.transform.position - leg2target.transform.position;
            Vector3 forward = transform.forward;
            float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
            Debug.Log("Angle: " + angle);
        }
        if (leg3Ycoord != leg4Ycoord)
        {
            rightLegYDiff = Mathf.Abs(leg3Ycoord - leg4Ycoord);
        }
        totalLegYDiff = (leftLegYDiff + rightLegYDiff) / 2; */
    }

    void playerJump()
    {
        rb.AddForce(Vector3.up * jumpspeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            onGround = false;
        }
    }
}
