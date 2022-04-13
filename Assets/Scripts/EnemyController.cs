using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Collider> RagdollParts = new List<Collider>();

    Rigidbody rb;
    Animator anim;

    private void Awake()
    {
        SetRagdollParts();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void SetRagdollParts()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(Collider c in colliders)
        {
            if(c.gameObject != gameObject)
            {
                c.isTrigger = true;
                RagdollParts.Add(c);
            }
            
            
            
        }

    }

    void TurnOnRagdoll()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;

        rb.useGravity = false;

        anim.enabled = false;
        anim.avatar = null;

        foreach(Collider c in RagdollParts)
        {
            c.isTrigger = false;
            //c.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TurnOnRagdoll();
            

        }
    }

}
