using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [Header("Returning To Boat")]
    [SerializeField] private float fireTimer;
    [SerializeField] private float timeAlive;
    [SerializeField] private GameObject launcher;
    public float speed = 1.5f;
    

    [Header("Hitting Rocks")]
    [SerializeField] private bool timerOn = true;
    private bool stopLooking;
    public Rigidbody rb;

    [Header("Collecting Iteams")]
    [SerializeField] bool collected;

    private void Awake()
    {
        FindBoat();
    }

    void timer()
    {
        //Timer that returns the harpoon after select amount of time.
        if (fireTimer < timeAlive & timerOn == true) 
        {
            fireTimer += Time.deltaTime;
        }
        else if (fireTimer >= timeAlive & timerOn == true)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, launcher.transform.position, speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        timer();
        transform.up = -(launcher.transform.position - transform.position);

        if (stopLooking == true)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, launcher.transform.position, speed * Time.deltaTime);
        }
    }

    public void FindBoat()
    {
        launcher = GameObject.FindGameObjectWithTag("HarpoonLauncher"); 
    }

    private void OnTriggerEnter(Collider other) //Hitiing the boats collection collider.
    {
        if (other.tag == "PlayerBoat" & fireTimer >= timeAlive || stopLooking == true)
        {
            DestroyHarpoon();
        }
    }

    private void OnCollisionEnter(Collision collision) //Hitting rocks and other harpoonable objects.
    {
        if (collision.gameObject.tag == "Hookable" & stopLooking == false)
        {
            timerOn = false;
            fireTimer = 0f;
            Destroy(rb);
            launcher.GetComponent<HarpoonAim>().HarpoonHit();
        }
        else if (collision.gameObject.tag == "Collectable" & collected == false)
        {
            timerOn = false;
            fireTimer = 0f;
            transform.SetParent(collision.transform);
        }
        
    }

    public void ReturnToBoat()
    {
        if (timerOn == false)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            stopLooking = true;
        }
        else
        {
            stopLooking = true;
        }
    }

    public void DestroyHarpoon()
    {
        launcher.GetComponent<HarpoonAim>().HarpoonDead();
        Destroy(gameObject);
    }
}
