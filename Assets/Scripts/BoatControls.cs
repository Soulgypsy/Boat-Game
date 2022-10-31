using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 5f;
    public float cameraDistanceFromPlayer = 5f;
    public float cameraHeight = 5f;

    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime, 0);

        cameraTransform.position = transform.position + transform.rotation * new Vector3(0, cameraHeight, -cameraDistanceFromPlayer);
        cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}
