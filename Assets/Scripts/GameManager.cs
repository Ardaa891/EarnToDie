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
    


    void Start()
    {
        Current = this;
        GasBar.Current.SetMaxGas(CarController.Current.maxGas);
        if (CarController.Current._doorbar == 1)
        {
            CarController.Current.doorBar.SetActive(true);
            CarController.Current.doorUpgradeButton.transform.gameObject.SetActive(false);
        }

        if(CarController.Current._windowbar == 1)
        {
            CarController.Current.windowUpgradeButton.transform.gameObject.SetActive(false);
        }

    }

    
    void Update()
    {
        if (CarController.Current.isRoad1)
        {

        }
        if (CarController.Current.isRoad2)
        {
            Destroy(CarController.Current.zombies);

        }
        if (CarController.Current.isRoad3)
        {

        }
        if (CarController.Current.isRoad4)
        {
            /*Instantiate(CarController.Current.newZombies, new Vector3(CarController.Current.road1.position.x,CarController.Current.road1.position.y, CarController.Current.road1.position.z - 33), Quaternion.Euler(0, 0, 0));
            CarController.Current.newZombies.transform.SetParent(CarController.Current.parent);*/
        }
    }

    public void StartGame()
    {
        gameStartMenu.SetActive(false);
        //inGameMenu.SetActive(true);

        isGameActive = true;

        player.GetComponent<CarController>().enabled = true;

        
        
    }

    public void GameOver()
    {
        //gameOverMenu.SetActive(true);
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
