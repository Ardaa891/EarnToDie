using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DinoFracture;

public class SetParent : MonoBehaviour
{
    public static SetParent current;
    public Transform parent;
    Rigidbody rb;

    public bool smashedTomb;
    
    
    void Start()
    {
        current = this;
        parent = GameObject.FindGameObjectWithTag("FracturedPieces").transform;
        gameObject.transform.SetParent(parent);
        gameObject.GetComponent<PreFracturedGeometry>().GeneratedPieces.transform.SetParent(parent);
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().material = null;
           
           smashedTomb = true;
        }
    }
}
