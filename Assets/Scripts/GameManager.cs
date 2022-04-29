using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager Current;

    public bool isGameActive = false;

    public GameObject gameStartMenu, gameOverMenu, gasGameOverMenu, inGameMenu;
    public Button windowUpgradeButton, doorUpgradeButton, tireUpgradeButton, nitroUpgradeButton;
    public GameObject player;

    private void Awake()
    {
        player.GetComponent<CarController>().enabled = true;
    }

    void Start()
    {
        Current = this;
        GasBar.Current.SetMaxGas(CarController.Current.maxGas);
        if (CarController.Current._doorbar == 1)
        {
            //CarController.Current.doorBar.SetActive(true);
            CarController.Current.doorUpgradeButton.transform.gameObject.SetActive(false);
        }
        if (CarController.Current._doorbar == 2)
        {
            //CarController.Current.doorBar.SetActive(true);
            CarController.Current.doorUpgradeButton2.transform.gameObject.SetActive(false);
        }
        if (CarController.Current._doorbar == 3)
        {
            //CarController.Current.doorBar.SetActive(true);
            CarController.Current.doorUpgradeButton3.transform.gameObject.SetActive(false);
        }

        /*if(CarController.Current._windowbar == 1)
        {
            CarController.Current.windowUpgradeButton.transform.gameObject.SetActive(false);
        }*/

    }

    
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameStartMenu.SetActive(false);
        //inGameMenu.SetActive(true);

        isGameActive = true;

        

        
        
    }

    public void GameOver()
    {
        //gameOverMenu.SetActive(true);
        inGameMenu.SetActive(false);
        isGameActive = false;

       

    }


    
}
