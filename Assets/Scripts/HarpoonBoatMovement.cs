using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonBoatMovement : MonoBehaviour
{
    [SerializeField] private GameObject harpoon;
    [SerializeField] private float speed;
    public bool moving;
    
    void Update()
    {
        if (moving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, harpoon.transform.position, speed * Time.deltaTime); //*
        }
    }

    public void Actived(GameObject poon)
    {
        harpoon = poon;
        moving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NearHookable")
        {
            moving = false;
        }
    }
}
