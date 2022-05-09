using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    bool a;
    public string Scene_Name;
    // In this example we show how to invoke a coroutine and
    // continue executing the function in parallel.

    private IEnumerator coroutine;

    void Start()
    {
        a = true;
        coroutine = WaitAndPrint(3.0f);
        StartCoroutine(coroutine);
        Invoke("ChangeA",3);
    }

    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene_Name);
        while (a && asyncLoad.isDone == false)
        {   

             yield return null;
            // SceneManager.LoadSceneAsync(Scene_Name);
            
        }
    }

    void ChangeA()
    {
        a = false;
    }
}
