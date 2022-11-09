using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour
{
    public float moveSpeedForward;
    public float moveSpeedBackwards;
    public float turnSpeed;
    public Vector3 boatTurning;
    public float moveMaxSpeed = 30f;
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
        turnSpeed = 50f; // Change this for turning speed
        boatTurning = new Vector3(0, turnSpeed, 0);
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w")) // Registers the input
            {
            if(moveSpeedForward <= moveMaxSpeed)
            {
                moveSpeedForward += 5f; // moveSpeed is multiplied over time to the cap
            }
            float force = Input.GetAxis("Vertical");
            boatBody.AddForce(transform.forward * moveSpeedForward * force);
        }
        else
        {
            if (moveSpeedForward != 0f)
            {
                moveSpeedForward -= 5f; // moveSpeed is divided over time to zero
            } 
        }

        if (Input.GetKey("s")) // Registers the input
        {
            if (moveSpeedBackwards <= moveMaxSpeed)
            {
                moveSpeedBackwards += 5f; // moveSpeed is multiplied over time to the cap
            }
            float force = Input.GetAxis("Vertical");
            boatBody.AddForce(transform.forward * moveSpeedForward * force);
        }
        else
        {
            if (moveSpeedBackwards != 0f)
            {
                moveSpeedBackwards -= 5f; // moveSpeed is divided over time to zero
            }
        }

        if (Input.GetKey("a")) // Registers the input
        {
            Quaternion boatRotation = Quaternion.Euler(-boatTurning * Time.fixedDeltaTime);
            boatBody.MoveRotation(boatBody.rotation * boatRotation);
        }

        if (Input.GetKey("d")) // Registers the input
        {
            Quaternion boatRotation = Quaternion.Euler(boatTurning * Time.fixedDeltaTime);
            boatBody.MoveRotation(boatBody.rotation * boatRotation);
        }

        cameraTransform.position = transform.position + transform.rotation * new Vector3(0, cameraHeight, -cameraDistanceFromPlayer);
        cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}
