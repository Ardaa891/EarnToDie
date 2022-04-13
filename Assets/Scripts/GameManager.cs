using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Current;

    public bool isGameActive = false;

    public GameObject gameStartMenu, gameOverMenu;
   
    void Start()
    {
        Current = this;
    }

    
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameStartMenu.SetActive(false);


        isGameActive = true;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);

        isGameActive = false;
    }
}
