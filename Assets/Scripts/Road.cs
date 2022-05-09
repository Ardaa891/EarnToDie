using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
  
    void Start()
    {
        if(RoadSpawner.Current.pool[0] != transform.gameObject && RoadSpawner.Current.pool[1] != transform.gameObject)
       {
           gameObject.SetActive(false);

       }
        
        
        
        
        
       





        
    }

  
  
}
