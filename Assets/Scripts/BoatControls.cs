using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public float moveMaxSpeed = 10f;
    public float turnMaxSpeed = 5f;
    public float cameraDistanceFromPlayer = 5f;
    public float cameraHeight = 5f;
    public Rigidbody boatBody;

    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        boatBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        moveSpeed = 0f;
        turnSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("s")) // Registers the input
            {
            if(moveSpeed <= moveMaxSpeed)
            {
                moveSpeed += 2f * Time.deltaTime; // moveSpeed is multiplied over time to the cap
            }
        }
        else
        {
            if (moveSpeed != 0f)
            {
                moveSpeed -= moveSpeed * Time.deltaTime; // moveSpeed is divided over time to zero
            } 
        }

        if (Input.GetKey("a") || Input.GetKey("d")) // Registers the input
        {
            if (turnSpeed <= turnMaxSpeed)
            {
                turnSpeed += 2f * Time.deltaTime; // turnSpeed is multiplied over time to the cap
            }
        }
        else
        {
            if (turnSpeed != 0f)
            {
                turnSpeed -= turnSpeed * Time.deltaTime; // turnSpeed is divided over time to zero
            }
        }

        transform.position += transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime, 0);

        cameraTransform.position = transform.position + transform.rotation * new Vector3(0, cameraHeight, -cameraDistanceFromPlayer);
        cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}
