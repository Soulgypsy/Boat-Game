using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour
{
    public float moveSpeedForward;
    public float moveSpeedBackwards;
    public float turnSpeedLeft;
    public float turnSpeedRight;
    public Vector3 boatMovement;
    public float moveMaxSpeed = 10f;
    public float turnMaxSpeed = 0.5f; // For the speed of the rotation
    public float turnMaxRotation = 0.1f; // To clamp the rotation of the torque;
    public float cameraDistanceFromPlayer = 5f;
    public float cameraHeight = 5f;
    public Rigidbody boatBody;

    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        boatBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        moveSpeedForward = 0f;
        moveSpeedBackwards = 0f;
        turnSpeedLeft = 0f;
        turnSpeedRight = 0f;
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w")) // Registers the input
            {
            if(moveSpeedForward <= moveMaxSpeed)
            {
                moveSpeedForward += 2f * Time.deltaTime; // moveSpeed is multiplied over time to the cap
            }
            float force = Input.GetAxis("Vertical");
            boatBody.AddForce(transform.forward * moveSpeedForward * force);
        }
        else
        {
            if (moveSpeedForward != 0f)
            {
                moveSpeedForward -= Time.deltaTime; // moveSpeed is divided over time to zero
            } 
        }

        if (Input.GetKey("s")) // Registers the input
        {
            if (moveSpeedBackwards <= moveMaxSpeed)
            {
                moveSpeedBackwards += 2f * Time.deltaTime; // moveSpeed is multiplied over time to the cap
            }
            float force = Input.GetAxis("Vertical");
            boatBody.AddForce(transform.forward * moveSpeedForward * force);
        }
        else
        {
            if (moveSpeedBackwards != 0f)
            {
                moveSpeedBackwards -= Time.deltaTime; // moveSpeed is divided over time to zero
            }
        }

        if (Input.GetKey("a")) // Registers the input
        {
            if (turnSpeedLeft <= turnMaxSpeed)
            {
                turnSpeedLeft += 2f * Time.deltaTime; // turnSpeed is multiplied over time to the cap
            }
            float turn = Input.GetAxis("Horizontal");
            if (turn >= turnMaxRotation)
            {
                turn = turnMaxRotation;
            }
            boatBody.AddTorque(transform.up * turnSpeedLeft * turn);
        }
        else
        {
            if (turnSpeedLeft != 0f)
            {
                turnSpeedLeft -= Time.deltaTime; // turnSpeed is divided over time to zero
            }
        }

        if (Input.GetKey("d")) // Registers the input
        {
            if (turnSpeedRight <= turnMaxSpeed)
            {
                turnSpeedRight += 2f * Time.deltaTime; // turnSpeed is multiplied over time to the cap
            }
            float turn = Input.GetAxis("Horizontal");
            if(turn >= turnMaxRotation)
            {
                turn = turnMaxRotation;
            }
            boatBody.AddTorque(transform.up * turnSpeedRight * turn);
        }
        else
        {
            if (turnSpeedRight != 0f)
            {
                turnSpeedRight -= Time.deltaTime; // turnSpeed is divided over time to zero
            }
        }

        cameraTransform.position = transform.position + transform.rotation * new Vector3(0, cameraHeight, -cameraDistanceFromPlayer);
        cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}
