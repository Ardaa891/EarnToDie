using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Collider> RagdollParts = new List<Collider>();

    Rigidbody rb;
    Animator anim;

    public  int xForce, yForce, zForce;
    public int forceAmount = 500;

    public float power = 10.0f;
    public float radius = 5.0f;
    Vector3 explosionPos;

    private void Awake()
    {
        SetRagdollParts();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        xForce = Random.Range(1, 10);
        yForce = Random.Range(20, 35);
        zForce = Random.Range(15, 30);

        explosionPos = transform.position;
    }

    void SetRagdollParts()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(Collider c in colliders)
        {
            if(c.gameObject != gameObject)
            {
                c.isTrigger = true;
                c.GetComponent<Rigidbody>().useGravity = false;
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
            c.GetComponent<Rigidbody>().useGravity = true;
            c.GetComponent<Rigidbody>().AddForce(xForce, yForce, zForce, ForceMode.Impulse);
            //c.attachedRigidbody.velocity = Vector3.zero;
        }

        Die();
    }

    void TurnOnRagdollv2()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;

        rb.useGravity = false;

        anim.enabled = false;
        anim.avatar = null;

        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.GetComponent<Rigidbody>().useGravity = true;
            c.GetComponent<Rigidbody>().AddForce(1, 1, 2, ForceMode.Impulse);
            //c.attachedRigidbody.velocity = Vector3.zero;
        }

        Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            TurnOnRagdoll();
            

        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TurnOnRagdollv2();
            
        }

        
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
    }

}
