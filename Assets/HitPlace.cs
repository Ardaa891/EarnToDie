using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitPlace : MonoBehaviour
{
    public GameObject road1Prefab, road2Prefab, road3Prefab, road4Prefab;

    public Text scoreText;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("road1"))
        {
            //road2.position = new Vector3(road2.position.x, road2.position.y, road1.position.z + 500.0f);
            //isRoad1 = true;
            Instantiate(road2Prefab, new Vector3(other.transform.GetComponentInParent<Transform>().transform.position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));

        }
        if (other.CompareTag("road2"))
        {
            //road3.position = new Vector3(road3.position.x, road3.position.y, road2.position.z + 500.0f);
            //Destroy(zombies);
            //isRoad2 = true;
            Instantiate(road1Prefab, new Vector3(other.transform.GetComponentInParent<Transform>().position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));

           // Instantiate(road3Prefab, new Vector3(other.transform.GetComponentInParent<Transform>().transform.position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));

        }
        if (other.CompareTag("road3"))
        {
            //road4.position = new Vector3(road4.position.x, road4.position.y, road3.position.z + 500.0f);
            //isRoad3 = true;
            
            
            //Instantiate(road4Prefab, new Vector3(other.transform.GetComponentInParent<Transform>().position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));
        }
        if (other.CompareTag("road4"))
        {
            //road1.position = new Vector3(road1.position.x, road1.position.y, road4.position.z + 500.0f);
            /*Instantiate(newZombies, new Vector3(road1.position.x, road1.position.y, road1.position.z - 33), Quaternion.Euler(0, 0, 0));
            newZombies.transform.SetParent(parent);
            isRoad4 = true;*/
            
            
           // Instantiate(road1Prefab, new Vector3(other.transform.GetComponentInParent<Transform>().position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));

        }

        if (other.CompareTag("tombstone"))
        {
            
            //CarController.Current._tombstone.GetComponent<Rigidbody>().useGravity = true;
            
        }

       
    }
}
