using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public static RoadSpawner Current;
    public GameObject road1Prefab, road2Prefab, first,second;

    public GameObject [] pool;

    void Awake()
    
    {

        Current = this;
        pool = new GameObject[12];
        pool[0] = first;
        pool[1] = second;

        for(int i = 2; i<12; i++)
        {
            if(i % 2 == 0)
            {
                
                

               GameObject tmp = Instantiate(road1Prefab, new Vector3(pool[i-1].transform.position.x, pool[i-1].transform.position.y, pool[i-1].transform.position.z + 500), Quaternion.Euler(0, 180, 0));
               pool[i] = tmp;


            }else
            {
                GameObject tmp =Instantiate(road2Prefab, new Vector3(pool[i-1].transform.position.x, pool[i-1].transform.position.y, pool[i-1].transform.position.z + 500), Quaternion.Euler(0, 180, 0));
pool[i] = tmp;
            }

        }


    }    




    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
