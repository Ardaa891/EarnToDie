using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Current;
    Rigidbody rb;
    
    public float speed;
    public float yPos;
    public GameObject car;

    public float generalSpeed;
    public bool onGround;

    public GameObject pitBar, rocket, tireSpikes, windowBars;

    public float distance;
   
    
    
    void Start()
    {
        Current = this;
        rb = GetComponent<Rigidbody>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

   


    void FixedUpdate()
    {
        yPos = transform.position.y;
        generalSpeed = rb.velocity.z;

        Physics.gravity = new Vector3(0, -15, 0);
        distance = (transform.position.z / 2);

        if (GameManager.Current.isGameActive && yPos < 2 && generalSpeed <=20)
        {
            

            if (Input.GetMouseButton(0))
            {
                rb.AddRelativeForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
   
            }

        }
        else
        {
            return;
        }




    }

    

    




}
