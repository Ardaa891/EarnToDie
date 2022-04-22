using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CarController : MonoBehaviour
{
    public static CarController Current;

    public WheelCollider[] wheels;
    public WheelCollider frontLeft, frontRight, backLeft, backRight;
    
    public float motorPower = 100f;
    public float breakPower = 100f;
    public float yPos;
    public float generalSpeed;
    public float distance;
    public int maxHealth;
    public int currentHealth;
    public int _distance;
    public int score;
    public int currentScore;
    
    public  Vector3 centerOfMass;

    public Transform road1, road2, road3, road4;
    public Transform tombstone;

    public Text distanceText;
    public Text highScoreText;
    public Text scoreText;
    
    public  Rigidbody rb;
    int input = 0;
    int _tire;
    int _nitro;
    int _doorbar;
    int _windowbar;

    public bool isBreaking = false;
    public bool onGround = false;
    public static bool isTireSpike;
    public static bool isDoorBar;
    public static bool isWindowBar;
    public static bool isNitro;

    public GameObject car;
    public GameObject crashedCar;
    public GameObject tireSpike, doorBar, windowBar, nitro;
    public GameObject _tombstone;
    public GameObject nitro1, nitro2;

    private void Awake()
    {
        _tire = PlayerPrefs.GetInt("tire", 0);
        if (_tire == 1)
        {
            tireSpike.SetActive(true);
        }
        _nitro = PlayerPrefs.GetInt("nitro", 0);
        if (_nitro ==1)
        {
            nitro.SetActive(true);
            motorPower = 15000.0f;
        }
        _doorbar = PlayerPrefs.GetInt("doorbar", 0);
        if (_doorbar == 1)
        {
            maxHealth = 150;
            doorBar.SetActive(true);
            HealthBar.Current.SetMaxHealth(maxHealth);
            if (isWindowBar)
            {
                maxHealth = 200;
            }
        }
        _windowbar = PlayerPrefs.GetInt("windowbar", 0);
        if (_windowbar ==1)
        {
            maxHealth = 150;
            windowBar.SetActive(true);
            HealthBar.Current.SetMaxHealth(maxHealth);

            if (isDoorBar)
            {
                maxHealth = 200;
            }
        }
    }
    private void Start()
    {
        
        Current = this;
        rb = GetComponent<Rigidbody>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        maxHealth = 100;
        currentHealth = maxHealth;
        score = PlayerPrefs.GetInt("currentscore");
        //scoreText.text = PlayerPrefs.GetInt("MaxScore").ToString();
        scoreText.text = PlayerPrefs.GetInt("currentscore").ToString();
        rb.centerOfMass = centerOfMass;
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        

        HealthBar.Current.SetMaxHealth(maxHealth);

       
        
        



    }

    private void Update()
    {
        

        if (GameManager.Current.isGameActive)
        {
            yPos = transform.position.y;
            generalSpeed = rb.velocity.z;
            Physics.gravity = new Vector3(0, -20, 0);
            distance = (transform.position.z / 4);
            _distance = Mathf.RoundToInt(distance);
            distanceText.text = _distance.ToString();
            scoreText.text = score.ToString();


            PlayerPrefs.SetInt("MaxScore", score);


            if (_distance > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", _distance);
                highScoreText.text = _distance.ToString();
            }

            
            

            if (rb.velocity.z < 0 && isBreaking)
            {
                rb.velocity = new Vector3(0,rb.velocity.y, 0);
            }

            if (rb.velocity.z <= 20)
            {
                motorPower = 7000.0f;
                breakPower = 1000.0f;
            }
            if (rb.velocity.z > 20 && rb.velocity.z <= 40)
            {
                motorPower = 4000.0f;
                breakPower = 1500.0f;
            }
            if (rb.velocity.z > 40)
            {
                motorPower = 3000.0f;
                breakPower = 2000.0f;
            }
        }
        else
        {
            return;
        }
        
        if(currentHealth <= 0)
        {
            crashedCar.SetActive(true);
            car.SetActive(false);
            GameManager.Current.GameOver();
            rb.velocity = Vector3.zero;
            

        }


    }



    private void FixedUpdate()
    {
        
        
        if (GameManager.Current.isGameActive && yPos <= 10)
        {
            /*foreach (var wheel in wheels)
            {
                wheel.motorTorque = motorPower * input;
            }*/

            frontLeft.motorTorque = motorPower * input;
            frontRight.motorTorque = motorPower * input;


            if (Input.GetMouseButtonDown(0))
            {
                input = 1;
                isBreaking = false;

                frontRight.brakeTorque = 0;
                frontLeft.brakeTorque = 0;
                backLeft.brakeTorque = 0;
                backRight.brakeTorque = 0;

              

                /*foreach (var wheel in wheels)
                {
                    wheel.brakeTorque = 0;


                }*/

            }
            if (Input.GetMouseButtonUp(0))
            {
                input = 0;

                isBreaking = true;

                backLeft.brakeTorque = breakPower;
                backRight.brakeTorque = breakPower;



                /*foreach (var wheel in wheels)
                {
                    wheel.brakeTorque = breakPower;


                }*/

               

            }

  
            
        }
        else
        {
            return;
        }

      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("road1"))
        {
            road2.position = new Vector3(road2.position.x, road2.position.y, road1.position.z + 500.0f);
        }
        if (other.CompareTag("road2"))
        {
            road3.position = new Vector3(road3.position.x, road3.position.y, road2.position.z + 500.0f);
        }
        if (other.CompareTag("road3"))
        {
            road4.position = new Vector3(road4.position.x, road4.position.y, road3.position.z + 500.0f);
        }
        if (other.CompareTag("road4"))
        {
            road1.position = new Vector3(road1.position.x, road1.position.y, road4.position.z + 500.0f);
        }
    }

    

    public void ChangeScore(int value)
    {
        score += value;
    }

    public void ChangeHealth(int value)
    {
        currentHealth += value;
        HealthBar.Current.SetHealth(currentHealth);
    }

    public void Tire()
    {
        isTireSpike = true;
        //tireSpike.SetActive(true);
        ChangeScore(-5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("MaxScore") - 5);
        _tire = 1;
        PlayerPrefs.SetInt("tire", _tire);

    }
    public void Nitro()
    {
        isNitro = true;
       // nitro.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ChangeScore(-5);
        PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("MaxScore") - 5);
        _nitro = 1;
        PlayerPrefs.SetInt("nitro", _nitro);

    }
    public void DoorBar()
    {
        isDoorBar = true;
       // doorBar.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ChangeScore(-5);
        PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("MaxScore") - 5);
        _doorbar = 1;
        PlayerPrefs.SetInt("doorbar", _doorbar);
    }
    public void WindowBar()
    {
        isWindowBar = true;
        //windowBar.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ChangeScore(-5);
        PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("MaxScore") - 5);
        _windowbar = 1;
        PlayerPrefs.SetInt("windowbar", _windowbar);
    }

    public void SetMotorPower()
    {
        motorPower = 7000.0f;
    }

    

}
