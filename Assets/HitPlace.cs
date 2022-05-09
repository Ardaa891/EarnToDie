using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class HitPlace : MonoBehaviour
{

    
    public GameObject road1Prefab, road2Prefab, road3Prefab, road4Prefab;

    public Text scoreText;

     public int pointer;
     
     [Serializable]
     public struct Pool
    {
     public Queue<GameObject> pooledObjects;
     public GameObject objectPrefab;
     public int poolSize;
    }
    

    [SerializeField] private Pool[] pools = null;

    /*private void Awake()
    {
       

        for(int j = 0; j<pools.Length; j++)
        {
             pools[j].pooledObjects = new Queue<GameObject>();

             for(int i = 0; i <pools[j]. poolSize; i++)
             {
                 GameObject obj = Instantiate(pools[j].objectPrefab);
                 obj.SetActive(false);

                 pools[j].pooledObjects.Enqueue(obj);


             }

        }

    }*/

   public GameObject GetPooledObject(int ObjectType)
    {
        if(ObjectType >= pools.Length ){
            return null;
        }

        GameObject obj = pools[ObjectType].pooledObjects.Dequeue();
        obj.SetActive(true);

        pools[ObjectType].pooledObjects.Enqueue(obj);

        return null;

    }
    
    
    void Start()
    {
        pointer = 2;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ground"))
        {
            CarController.Current.hitGround = true;
        }
        if (other.CompareTag("road1"))
        {
            //road2.position = new Vector3(road2.position.x, road2.position.y, road1.position.z + 500.0f);
            //isRoad1 = true;
            //Instantiate(road2Prefab, new Vector3(other.transform.GetComponentInParent<Transform>().transform.position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));

            /*for(int j = 0; j<pools.Length; j++)
        {
             pools[j].pooledObjects = new Queue<GameObject>();

             

             for(int i = 0; i <pools[j]. poolSize; i++)
             {
                 GameObject obj = Instantiate(pools[j].objectPrefab2, new Vector3(other.transform.GetComponentInParent<Transform>().transform.position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));
                 obj.SetActive(true);

                 pools[j].pooledObjects.Enqueue(obj);


             }

             

        }*/

        RoadSpawner.Current.pool[pointer].gameObject.SetActive(true);
        if(pointer>=3 )
        {
            RoadSpawner.Current.pool[pointer-3].gameObject.SetActive(false);

        }
         
        pointer ++;
       

        }
        if (other.CompareTag("road2"))
        {
            //road3.position = new Vector3(road3.position.x, road3.position.y, road2.position.z + 500.0f);
            //Destroy(zombies);
            //isRoad2 = true;
            //Instantiate(road1Prefab, new Vector3(other.transform.GetComponentInParent<Transform>().position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));

           // Instantiate(road3Prefab, new Vector3(other.transform.GetComponentInParent<Transform>().transform.position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));

           /*for(int j = 0; j<pools.Length; j++)
        {
             pools[j].pooledObjects = new Queue<GameObject>();

             for(int i = 0; i <pools[j]. poolSize; i++)
             {
                 GameObject obj = Instantiate(pools[j].objectPrefab1, new Vector3(other.transform.GetComponentInParent<Transform>().position.x, other.transform.GetComponentInParent<Transform>().transform.position.y, other.transform.GetComponentInParent<Transform>().transform.position.z + 500), Quaternion.Euler(0, 180, 0));
                 obj.SetActive(true);

                 pools[j].pooledObjects.Enqueue(obj);


             }

        }*/

        RoadSpawner.Current.pool[pointer].gameObject.SetActive(true);
        if(pointer>=3 )
        {
            RoadSpawner.Current.pool[pointer-3].gameObject.SetActive(false);

        }
        pointer ++;

        

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

        if (other.CompareTag("Enemy"))
        {
            /*if (CarController.Current._frontbar == 0)
            {
                CarController.Current.ChangeHealth(-3);
                CarController.Current.ChangeBlendShape(3);
            }
            if (CarController.Current._frontbar == 1)
            {
                CarController.Current.ChangeHealth(-2);
                CarController.Current.ChangeBlendShape(2);
            }
            if (CarController.Current._frontbar == 1 && CarController.Current._frontbar2 == 2)
            {
                CarController.Current.ChangeHealth(-1.5f);
                CarController.Current.ChangeBlendShape(1.5f);
            }
            if (CarController.Current._frontbar == 1 && CarController.Current._frontbar2 == 2 && CarController.Current._frontbar3 == 3)
            {
                CarController.Current.ChangeHealth(-0.75f);
                CarController.Current.ChangeBlendShape(-0.75f);
            }
            if (CarController.Current._frontbar == 1 && CarController.Current._frontbar2 == 2 && CarController.Current._frontbar3 == 3 && CarController.Current._frontbar4 == 4)
            {
                CarController.Current.ChangeHealth(-0.30f);
                CarController.Current.ChangeBlendShape(-0.30f);
            }*/

            if (CarController.Current.armor == 0)
            {
                CarController.Current.ChangeHealth(-3);
                CarController.Current.ChangeBlendShape(3);
            }
            else if (CarController.Current.armor == 1)
            {
                CarController.Current.ChangeHealth(-2);
                CarController.Current.ChangeBlendShape(2);
            }
            else if (CarController.Current.armor == 2)
            {
                CarController.Current.ChangeHealth(-1.5f);
                CarController.Current.ChangeBlendShape(1.5f);
            }
            else if (CarController.Current.armor == 3)
            {
                CarController.Current.ChangeHealth(-0.75f);
                CarController.Current.ChangeBlendShape(-0.75f);
            }
            else if (CarController.Current.armor == 4)
            {
                CarController.Current.ChangeHealth(-0.30f);
                CarController.Current.ChangeBlendShape(-0.30f);
            }
        }

       

       
    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            CarController.Current.hitGround = false;
        }
    }
}
