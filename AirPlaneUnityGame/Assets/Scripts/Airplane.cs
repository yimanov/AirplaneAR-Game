using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Airplane : MonoBehaviour
{


    Rigidbody airplane;
    private float forceLiftUp;  // force for lift up helicopter

    //values to move helicopter forward
    public float movementForwardSpeed = 7;
    private float xValueAirplane = 0;
    private float velocityForward;

    //values to rotate helicopter
    private float wantedYRotation;
    private float yValueAirplane;
    private float rotateAmoutByKeys = 1.5f;
    private float yRotationVelocity;

    // Use this for initialization
    void Start()
    {
        airplane = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        LiftUpDownProcess();
        MoveProcess();
        RotationProcess();
        MaxSpeedLimit();
        airplane.AddRelativeForce(Vector3.up * forceLiftUp); // Lift up/down
        airplane.rotation = Quaternion.Euler(new Vector3(xValueAirplane, yValueAirplane, airplane.rotation.z));  //rotate the helicopter
    }


    //methode for Liftup
    void LiftUpDownProcess()
    {
        if (Input.GetKey(KeyCode.I) || CrossPlatformInputManager.GetButton("Liftup"))
        {
            forceLiftUp = 260;
        }

        else if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K))
        {
            forceLiftUp = 190;
        }
    }

    //methode move forward
    void MoveProcess()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            airplane.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
            xValueAirplane = Mathf.SmoothDamp(xValueAirplane, 20 * Input.GetAxis("Vertical"), ref velocityForward, 0.1f);
        }

        if (CrossPlatformInputManager.GetAxis("Vertical") != 0 || CrossPlatformInputManager.GetAxis("Horizontal") != 0)
        {
            airplane.AddRelativeForce(Vector3.forward * (CrossPlatformInputManager.GetAxis("Vertical")) * movementForwardSpeed);
            xValueAirplane = Mathf.SmoothDamp(xValueAirplane, 20 * (CrossPlatformInputManager.GetAxis("Vertical")), ref velocityForward, 0.1f);
        }

    }

    //methode rotateHelicopter
    void RotationProcess()
    {
        if (Input.GetKey(KeyCode.J))
        {
            wantedYRotation -= rotateAmoutByKeys;
        }


        if (CrossPlatformInputManager.GetAxis("Horizontal") == 1f || CrossPlatformInputManager.GetAxis("Vertical") == -1f)
        {
            wantedYRotation += rotateAmoutByKeys;
        }


        if (Input.GetKey(KeyCode.L))
        {
            wantedYRotation += rotateAmoutByKeys;

        }

        if (CrossPlatformInputManager.GetAxis("Horizontal") == -1f || CrossPlatformInputManager.GetAxis("Vertical") == -1f)
        {
            wantedYRotation -= rotateAmoutByKeys;
        }




        yValueAirplane = Mathf.SmoothDamp(yValueAirplane, wantedYRotation, ref yRotationVelocity, 0.15f);
    }


    // limiting speed
    private Vector3 velocityToSmoothDampToZero;
    void MaxSpeedLimit()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            airplane.velocity = Vector3.ClampMagnitude(airplane.velocity, Mathf.Lerp(airplane.velocity.magnitude, 10.0f, Time.deltaTime * 0.4f));
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            airplane.velocity = Vector3.ClampMagnitude(airplane.velocity, Mathf.Lerp(airplane.velocity.magnitude, 10.0f, Time.deltaTime * 0.4f));
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            airplane.velocity = Vector3.ClampMagnitude(airplane.velocity, Mathf.Lerp(airplane.velocity.magnitude, 5.0f, Time.deltaTime * 0.4f));
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            airplane.velocity = Vector3.SmoothDamp(airplane.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.5f);
        }

    }


    public void Update()
    {

        //print ("vertical" + CrossPlatformInputManager.GetAxis ("Vertical"));
        //print("horizontal " + CrossPlatformInputManager.GetAxis ("Horizontal"));


        //print (CrossPlatformInputManager.GetButton ("Jump"));
    }
}




