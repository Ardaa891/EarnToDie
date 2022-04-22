using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Current;
    Rigidbody rb;
    
    public float speed;
    public float breakSpeed;
    public float yPos;
    public GameObject car;

    public float generalSpeed;
    public bool onGround;

    public GameObject pitBar, rocket, tireSpikes, windowBars;
    public WheelCollider frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;

    public Transform frontLeftWheelTransform, frontRightWheelTransform, backLeftWheelTransform, backRightWheelTransform;

    public float distance;

    public Transform road1, road2;

    private float horizontalInput;
    private float breakInput;
    public bool isBreaking;
    public float motorForce;
    public float currentBreakForce;
    
    
    void Start()
    {
        Current = this;
        rb = GetComponent<Rigidbody>();
        

        

       


       
    }

   


    void FixedUpdate()
    {
        yPos = transform.position.y;
        generalSpeed = rb.velocity.z;
        //rb.freezeRotation = true;
        Physics.gravity = new Vector3(0, -15, 0);
        
        
        if (GameManager.Current.isGameActive && yPos <= 10 )
        {
            

            if (Input.GetMouseButton(0))
            {
                isBreaking = false;

                if (!isBreaking)
                {

                    rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
                    Debug.Log("HIZZZZZ");

                    //frontLeftWheel.motorTorque = speed * Time.fixedDeltaTime;
                    //frontLeftWheel.motorTorque = speed * Time.fixedDeltaTime;
                    //GetInput();
                    //HandleMotor();
                }
                else
                {
                    return;
                }
                
               





            }
            
            
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("BREEEAAAAKKKK");
                isBreaking = true;

                if (isBreaking && generalSpeed > 0 )
                {
                    //ApplyBreaking();
                }
                else
                {

                    return;
                }

                

            }
            

        }
        else
        {
            return;
        }

        //GetInput();
        //HandleMotor();

        //UpdateWheels();








    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheel, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheel, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheel, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheel, backRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("road1"))
        {
            road2.position = new Vector3(road2.position.x, road2.position.y, road1.position.z + 398.0f);
        }
        if (other.CompareTag("road2"))
        {
            road1.position = new Vector3(road1.position.x, road1.position.y, road2.position.z + 278.0f);
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        breakInput = Input.GetAxis("Horizontal");
    }

    private void HandleMotor()
    {
        frontLeftWheel.motorTorque =  motorForce;
        frontLeftWheel.motorTorque =  motorForce;
        backLeftWheel.motorTorque = motorForce;
        backRightWheel.motorTorque = motorForce;
        //currentBreakForce = isBreaking ? currentBreakForce : 0f;


    }

    private void ApplyBreaking()
    {
        frontLeftWheel.motorTorque = -motorForce;
        frontLeftWheel.motorTorque = -motorForce;
        //backLeftWheel.motorTorque = -motorForce;
        //backRightWheel.motorTorque = -motorForce;
    }
}
