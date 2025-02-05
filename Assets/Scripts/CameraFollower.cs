using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public static CameraFollower Current;
    
    public Transform target;
    
    public float smoothSpeed = 0.125f;
    
    public Vector3 offset;
    
    
    private void Start()
    {
        Current = this;
    }
    
    
    public void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        
    }
}
