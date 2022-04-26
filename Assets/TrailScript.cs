using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
    public bool onGround;
    public TrailRenderer trail;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    private void Update()
    {
        if (onGround)
        {
            trail.emitting = true;
        }else if (!onGround)
        {
            trail.emitting = false;
        }
    }
}
