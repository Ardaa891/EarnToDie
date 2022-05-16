using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem Current;

    public TextMeshProUGUI healthPriceText;
    public TextMeshProUGUI gasPriceText;
    public TextMeshProUGUI incomePriceText;

    public Button healthButton;
    public Button gasButton;
    public Button incomeButton;

    public static bool clicked;

    private void Awake()
    {
        Current = this;
    }


    void Start()
    {
        CheckPrefs();
        CheckHealthButton();
        CheckGasButton();
        CheckIncomeButton();
        //PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("currentscore"));
        
    }

    // Update is called once per frame
    void Update()
    {
        //CheckButtons();
    }

    public void CheckPrefs()
    {
        if(PlayerPrefs.GetInt("HealthPrice") <= 0)
        {
            PlayerPrefs.SetInt("HealthPrice", 800);
        }

        if(PlayerPrefs.GetInt("GasPrice") <= 0)
        {
            PlayerPrefs.SetInt("GasPrice", 600);
        }

        if(PlayerPrefs.GetInt("IncomePrice") <= 0)
        {
            PlayerPrefs.SetInt("IncomePrice", 600);
        }
    }


    public void CheckHealthButton()
    {
        if(CarController.Current.score >= PlayerPrefs.GetInt("HealthPrice"))
        {
            healthButton.interactable = true;
        }
        else
        {
            healthButton.interactable = false;
        }


       

        healthPriceText.text = PlayerPrefs.GetInt("HealthPrice").ToString();
       
       

       // CarController.Current.scoreText.text = "" + PlayerPrefs.GetInt("Money");
    }

    public void CheckGasButton()
    {

        if (CarController.Current.score >= PlayerPrefs.GetInt("GasPrice"))
        {
            gasButton.interactable = true;
        }
        else
        {
            gasButton.interactable = false;
        }

        gasPriceText.text = PlayerPrefs.GetInt("GasPrice").ToString();
    }

    public void CheckIncomeButton()
    {
        if (CarController.Current.score >= PlayerPrefs.GetInt("IncomePrice"))
        {
            incomeButton.interactable = true;
        }
        else
        {
            incomeButton.interactable = false;
        }

        incomePriceText.text = PlayerPrefs.GetInt("IncomePrice").ToString();
    }

    public void BuyHealth()
    {
        if (CarController.Current.score >= PlayerPrefs.GetInt("HealthPrice"))
        {
            //PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - PlayerPrefs.GetInt("HealthPrice"));
            CarController.Current.ChangeScore(-PlayerPrefs.GetInt("HealthPrice"));

            //PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("currentscore") - PlayerPrefs.GetInt("HealthPrice"));

            PlayerPrefs.SetInt("HealthPrice", PlayerPrefs.GetInt("HealthPrice") + 200);

            HealthBar.Current.UpdateHealth(5);
            
        }
        CheckHealthButton();
    }

    public void BuyGas()
    {
        if (CarController.Current.score >= PlayerPrefs.GetInt("GasPrice"))
        {
            //PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - PlayerPrefs.GetInt("GasPrice"));
            CarController.Current.ChangeScore(-PlayerPrefs.GetInt("GasPrice"));
            //PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("currentscore") - PlayerPrefs.GetInt("GasPrice"));


            PlayerPrefs.SetInt("GasPrice", PlayerPrefs.GetInt("GasPrice") + 200);

            GasBar.Current.UpdateGas(5);
        }
        CheckGasButton();
    }

    public void BuyIncome()
    {
        if (CarController.Current.score >= PlayerPrefs.GetInt("IncomePrice"))
        {
            //PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - PlayerPrefs.GetInt("IncomePrice"));
            CarController.Current.ChangeScore(-PlayerPrefs.GetInt("IncomePrice"));
            //PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("currentscore") - PlayerPrefs.GetInt("IncomePrice"));
            PlayerPrefs.SetInt("IncomePrice", PlayerPrefs.GetInt("IncomePrice") + 200);

            HitPlace.Current.UpdateDamageAmount(1);
            clicked = true;
        }
        CheckIncomeButton();
    }
}
