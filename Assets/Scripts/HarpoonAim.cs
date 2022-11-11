using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class HarpoonAim : MonoBehaviour
{
    [Header("Aiming")]
    public Transform facing;

    [Header("Firing")]
    public GameObject harpoonPrefab;
    [SerializeField] private GameObject currentHarpoon;
    public Transform spawnPoint;
    public float speed = 10f;
    public bool readyToFire = true;
    public float fireTimer;

    [Header("Moving Towards Harpoon")]
    [SerializeField] private bool hit;
    public HarpoonBoatMovement movement;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & readyToFire == true)
        {
            Fire();
            readyToFire = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) & readyToFire == false)
        {
            currentHarpoon.GetComponent<Harpoon>().ReturnToBoat();
        }

        if (readyToFire == false)
        {
            transform.LookAt(transform.position -(currentHarpoon.transform.position - transform.position));

            /*if (fireTimer < 10)
            {
                fireTimer += Time.deltaTime;
            }
            else if (fireTimer >= 10)
            {
                fireTimer = 0f;
                readyToFire = true;
            }*/
        }
        else if (readyToFire == true)
        {
            transform.LookAt(new Vector3(facing.position.x, transform.position.y, facing.position.z));
        }

    }

    public void Fire()
    {
        var harpoon = Instantiate(harpoonPrefab, spawnPoint.position, spawnPoint.rotation);
        harpoon.GetComponent<Rigidbody>().velocity = spawnPoint.up * speed;
        currentHarpoon = harpoon;
    }

    public void HarpoonDead()
    {
        readyToFire = true;
        fireTimer = 0;
    }

    public void HarpoonHit()
    {
        Debug.Log("HarpoonHit called");
        movement.Actived(currentHarpoon);
    }

}
