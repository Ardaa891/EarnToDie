using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager Current;

    public bool isGameActive = false;

    public GameObject gameStartMenu, gameOverMenu, inGameMenu;
    public Button windowUpgradeButton, doorUpgradeButton, tireUpgradeButton, nitroUpgradeButton;
    
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
        inGameMenu.SetActive(true);

        isGameActive = true;
        
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        inGameMenu.SetActive(false);
        isGameActive = false;

        if (CarController.isTireSpike)
        {
            tireUpgradeButton.interactable = false;
        }

        if (CarController.isDoorBar)
        {
            doorUpgradeButton.interactable = false;
        }

        if (CarController.isNitro)
        {
            nitroUpgradeButton.interactable = false;
        }

        if (CarController.isWindowBar)
        {
            windowUpgradeButton.interactable = false;
        }

    }


    
}
