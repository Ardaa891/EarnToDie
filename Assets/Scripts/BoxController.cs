using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MoreMountains.NiceVibrations;

public class BoxController : MonoBehaviour
{

    public List<Collider> BoxParts = new List<Collider>();
    Rigidbody rb;

    public int xForce, yForce, zForce;

    





    void Start()
    {
        rb = GetComponent<Rigidbody>();
        xForce = Random.Range(1, 10);
        yForce = Random.Range(20, 35);
        zForce = Random.Range(15, 30);
    }

    
    void Update()
    {
        
    }

   

    

    

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            ClickOrTapToExplode.current.StartExplosion();
        }
        
        
    }*/
}
