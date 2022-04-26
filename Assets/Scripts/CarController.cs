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
    public Transform frontLeftT, frontRightT, backLeftT, backRightT;
    
    public float motorPower;
    public float breakPower;
    public float yPos;
    public float generalSpeed;
    public float distance;
    public float maxHealth;
    public float currentHealth;
    public float maxGas;
    public float currentGas;
    public float lastYPos;
    public float lastXRot;
    public int _distance;
    public int score;
    
    public float blendvalue = 0;
    
    public  Vector3 centerOfMass;

    public Transform road1, road2, road3, road4;
    public Transform tombstone;

    public Text distanceText;
    public Text highScoreText;
    public Text scoreText;
    
    public  Rigidbody rb;
    int input = 0;
    public int _tire;
    public int _nitro;
    public int _doorbar;
    public int _windowbar;
    public int _frontbar;

    public bool isBreaking = false;
    public bool onGround;
    public bool zombieRun;
    public static bool isTireSpike;
    public static bool isDoorBar;
    public static bool isWindowBar;
    public static bool isNitro;
    public static bool isFrontBar;
    public bool hasNitro;
    public bool hasFrontBar;
    public bool hasDoorBar;
    public bool hasTireSpike;
    public bool hasWindowBar;
    public bool clickedNitro;
    public bool clickedFrontBar;
    public bool clickedDoorBar;
    public bool clickedWindowBar;
    public bool clickedTire;
    public bool gasUpgrade1;


    public GameObject car;
    public GameObject crashedCar;
    public GameObject tireSpike, doorBar, windowBar, nitro,frontBar;
    public GameObject _tombstone;
    public GameObject nitro1, nitro2;
    public GameObject trail1, trail2;
    

    public Button nitroUpgradeButton;
    public Button frontBarUpgradeButton;
    public Button tireUpgradeButton;
    public Button doorUpgradeButton;
    public Button windowUpgradeButton;

    

    private void Awake()
    {
        maxHealth = 100;
        maxGas = 100;
        currentHealth = maxHealth;
        currentGas = maxGas;
        motorPower = 7000;
        breakPower = 1000;
        

        _tire = PlayerPrefs.GetInt("tire", 0);
        if (_tire == 1)
        {
            tireSpike.SetActive(true);
            tireUpgradeButton.transform.gameObject.SetActive(false);
        }
        _nitro = PlayerPrefs.GetInt("nitro", 0);
        if (_nitro ==1)
        {
            nitro.SetActive(true);
            isNitro = true;
            hasNitro = true;
            motorPower = 15000;
            nitroUpgradeButton.transform.gameObject.SetActive(false);
        }
        /*_doorbar = PlayerPrefs.GetInt("doorbar", 0);
        if (_doorbar == 1)
        {
            isDoorBar = true;
            hasDoorBar = true;
            doorBar.SetActive(true);
            doorUpgradeButton.transform.gameObject.SetActive(false);
            
        }*/
        _windowbar = PlayerPrefs.GetInt("window", 0);
        if (_windowbar ==1)
        {
            
            windowBar.SetActive(true);
            windowUpgradeButton.transform.gameObject.SetActive(false);
           
        }

        _frontbar = PlayerPrefs.GetInt("frontbar", 0);
        if(_frontbar == 1)
        {
            frontBar.SetActive(true);
            frontBarUpgradeButton.transform.gameObject.SetActive(false);

            
        }

        Instantiate(_tombstone, new Vector3(0, PlayerPrefs.GetFloat("lastypos"), PlayerPrefs.GetInt("maxdistance") *4), Quaternion.Euler(PlayerPrefs.GetFloat("lastxrot"),0,0));

    }
    private void Start()
    {
        
        Current = this;
        currentGas = maxGas;
        rb = GetComponent<Rigidbody>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        score = PlayerPrefs.GetInt("currentscore");
        
        //scoreText.text = PlayerPrefs.GetInt("MaxScore").ToString();
        //scoreText.text = PlayerPrefs.GetInt("currentscore").ToString();
        rb.centerOfMass = centerOfMass;
        //highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        

        HealthBar.Current.SetMaxHealth(maxHealth);
        GasBar.Current.SetMaxGas(maxGas);

       

        _doorbar = PlayerPrefs.GetInt("doorbar", 0);
        if (_doorbar == 1)
        {
            isDoorBar = true;
            hasDoorBar = true;
            doorBar.SetActive(true);
            doorUpgradeButton.transform.gameObject.SetActive(false);

        }



        if (score < 50)
        {
            nitroUpgradeButton.interactable = false;
            frontBarUpgradeButton.interactable = false;
            tireUpgradeButton.interactable = false;
            windowUpgradeButton.interactable = false;
            doorUpgradeButton.interactable = false;


        }

        if (_nitro ==1)
        {
            windowUpgradeButton.transform.gameObject.SetActive(true);
            nitroUpgradeButton.transform.gameObject.SetActive(false);
        }

        if(_windowbar == 1)
        {
            tireUpgradeButton.transform.gameObject.SetActive(true);
            windowUpgradeButton.interactable = false;
        }

        if(_frontbar == 1)
        {
            doorUpgradeButton.transform.gameObject.SetActive(true);
            frontBarUpgradeButton.transform.gameObject.SetActive(false);
        }
        if(_doorbar == 1)
        {
            doorUpgradeButton.interactable = false;
            doorBar.transform.gameObject.SetActive(false);
        }
        if(_tire == 1)
        {
            tireUpgradeButton.interactable = false;
            tireUpgradeButton.transform.gameObject.SetActive(false);
        }
        
        

       
        
        



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
            //distanceText.text = _distance.ToString();


            




            if (_distance > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", _distance);
                //highScoreText.text = _distance.ToString();
            }

            
            

            /*if (generalSpeed < 0 && isBreaking)
            {
                rb.velocity = new Vector3(0,rb.velocity.y, 0);
            }

            if (generalSpeed <= 20)
            {
                motorPower = 7000.0f;
                breakPower = 1000.0f;
            }
            if (generalSpeed > 20 && generalSpeed <= 40)
            {
                motorPower = 4000.0f;
                breakPower = 1500.0f;
            }
            if (generalSpeed > 40)
            {
                motorPower = 3000.0f;
                breakPower = 2000.0f;
            }*/
        }
        else
        {
            return;
        }
        
        

        


    }



    private void FixedUpdate()
    {
        scoreText.text = score.ToString();
        lastXRot = transform.rotation.x;

        if (GameManager.Current.isGameActive && yPos <= 20)
        {
            /*foreach (var wheel in wheels)
            {
                wheel.motorTorque = motorPower * input;
            }*/

            frontLeft.motorTorque = motorPower * input;
            frontRight.motorTorque = motorPower * input;
            UpdateWheelPoses();


            if (Input.GetMouseButton(0))
            {
                input = 1;
                isBreaking = false;

                frontRight.brakeTorque = 0;
                frontLeft.brakeTorque = 0; 
                backLeft.brakeTorque = 0;
                backRight.brakeTorque = 0;

                zombieRun = true;

                //trail1.GetComponent<TrailRenderer>().emitting = true;
                //trail2.GetComponent<TrailRenderer>().emitting = true;


                /*foreach (var wheel in wheels)
                {
                    wheel.brakeTorque = 0;


                }*/

                //ChangeGas(-0.25f);

               

            }
            if (Input.GetMouseButtonUp(0))
            {
                input = 0;

                isBreaking = true;

                backLeft.brakeTorque = breakPower;
                backRight.brakeTorque = breakPower;

                zombieRun = false;


                /*foreach (var wheel in wheels)
                {
                    wheel.brakeTorque = breakPower;


                }*/



            }

            if (generalSpeed < 0 && isBreaking)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }

            if (generalSpeed <= 20)
            {
                motorPower = 7000.0f;
                breakPower = 1000.0f;
            }
            if (generalSpeed > 20 && generalSpeed <= 40)
            {
                motorPower = 4000.0f;
                breakPower = 1500.0f;
            }
            if (generalSpeed > 40)
            {
                motorPower = 3000.0f;
                breakPower = 2000.0f;
            }
            if (currentHealth <= 0)
            {
                crashedCar.SetActive(true);
                car.SetActive(false);
                GameManager.Current.GameOver();
                rb.velocity = Vector3.zero;
                PlayerPrefs.SetInt("maxdistance", _distance);
                PlayerPrefs.SetFloat("lastypos", yPos);
                PlayerPrefs.SetFloat("lastxrot", lastXRot);

                GameManager.Current.gameOverMenu.SetActive(true);
                


            }else if(currentGas <= 0)
            {
                GameManager.Current.GameOver();
                //GameManager.Current.isGameActive = false;
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                PlayerPrefs.SetInt("maxdistance", _distance);
                PlayerPrefs.SetFloat("lastypos", yPos);
                PlayerPrefs.SetFloat("lastxrot", lastXRot);
                GameManager.Current.gasGameOverMenu.SetActive(true);
            }
            if (hasNitro)
            {
                motorPower = 15000;
            }else if (!hasNitro && generalSpeed <= 20)
            {
                SetMotorPower();
            }

            /*if (currentGas <= 0)
            {
                GameManager.Current.GameOver();
                //GameManager.Current.isGameActive = false;
                rb.velocity = Vector3.zero;
                PlayerPrefs.SetInt("maxdistance", _distance);
                GameManager.Current.gasGameOverMenu.SetActive(true);
            }*/



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
            Debug.Log("road1");
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
        PlayerPrefs.SetInt("currentscore", score);
        
    }

    public void ChangeHealth(float value)
    {
        currentHealth += value;
        //blendvalue += value;
        HealthBar.Current.SetHealth(currentHealth);
        //car.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendvalue);
    }

    public void ChangeGas(float value)
    {
        currentGas += value;
        GasBar.Current.SetGas(currentGas);

        
    }

    public void ChangeBlendShape(float value)
    {
        blendvalue += value ;
        car.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, blendvalue);
    }

    public void Tire()
    {
        /*isTireSpike = true;
        //tireSpike.SetActive(true);
        ChangeScore(-5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("MaxScore") - 5);
        _tire = 1;
        PlayerPrefs.SetInt("tire", _tire);*/

        isTireSpike = true;
        hasTireSpike = true;
        tireSpike.SetActive(true);
        GameManager.Current.StartGame();
        _tire = 1;
        PlayerPrefs.SetInt("tire", _tire);
        ChangeScore(-50);

        clickedTire = true;
       // tireUpgradeButton.transform.gameObject.SetActive(false);


    }
    public void Nitro()
    {
        isNitro = true;
        hasNitro = true;
        nitro.SetActive(true);
       // nitro.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //ChangeScore(-5);
        //PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("MaxScore") - 50);
        GameManager.Current.StartGame();
        _nitro = 1;
        PlayerPrefs.SetInt("nitro", _nitro);
        ChangeScore(-50);

        clickedNitro = true;
       // nitroUpgradeButton.transform.gameObject.SetActive(false);
       // windowUpgradeButton.transform.gameObject.SetActive(true);


    }

    public void FrontBar()
    {
        isFrontBar = true;
        hasFrontBar = true;
        frontBar.SetActive(true);
        GameManager.Current.StartGame();
        _frontbar = 1;
        PlayerPrefs.SetInt("frontbar", _frontbar);
        ChangeScore(-50);

        clickedFrontBar = true;

        //frontBarUpgradeButton.transform.gameObject.SetActive(false);
        //doorUpgradeButton.transform.gameObject.SetActive(true);
        
        
        
    }
    public void DoorBar()
    {
        /*isDoorBar = true;
       // doorBar.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ChangeScore(-5);
        PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("MaxScore") - 5);
        _doorbar = 1;
        PlayerPrefs.SetInt("doorbar", _doorbar);*/

        isDoorBar = true;
        hasDoorBar = true;
        doorBar.SetActive(true);
        GameManager.Current.StartGame();
        _doorbar = 1;
        PlayerPrefs.SetInt("doorbar", _doorbar);
        ChangeScore(-50);

        clickedDoorBar = true;

        //doorUpgradeButton.transform.gameObject.SetActive(false);
    }
    public void WindowBar()
    {
        /*isWindowBar = true;
        //windowBar.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ChangeScore(-5);
        PlayerPrefs.SetInt("currentscore", PlayerPrefs.GetInt("MaxScore") - 5);
        _windowbar = 1;
        PlayerPrefs.SetInt("windowbar", _windowbar);*/

        isWindowBar = true;
        hasWindowBar = true;
        windowBar.SetActive(true);
        GameManager.Current.StartGame();
        _windowbar = 1;
        PlayerPrefs.SetInt("window", _windowbar);
        ChangeScore(-50);

        clickedWindowBar = true;

        
    }

    public void SetMotorPower()
    {
        motorPower = 7000.0f;
        hasNitro = false;
    }

    void UpdateWheelPoses()
    {
        SetBackRightPose(backRight, backRightT);
        SetBacktLeftPose(backLeft, backLeftT);
        SetFrontLeftPose(frontLeft, frontLeftT);
        SetFrontRightPose(frontRight, frontRightT);
    }

    /*void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = Quaternion.Euler( _transform.rotation.x, 180,  _transform.rotation.z);

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation =  _quat;

      
    }*/

    void SetFrontLeftPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = new Vector3(1, _transform.position.y, -1.5f);
        Quaternion _quat = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);

        //_collider.transform.position = _pos;
        //_collider.transform.rotation = _quat;
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;

    }
    void SetFrontRightPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = new Vector3(-1, _transform.position.y, -1.5f);
        Quaternion _quat = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
        _collider.GetWorldPose(out _pos, out _quat);
        //_collider.transform.position = _pos;
        //_collider.transform.rotation = _quat;
        //_collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    void SetBacktLeftPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = new Vector3(1, _transform.position.y, 1.5f);
        Quaternion _quat = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
        _collider.GetWorldPose(out _pos, out _quat);
        //_collider.transform.position = _pos;
        //_collider.transform.rotation = _quat;

        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    void SetBackRightPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = new Vector3(-1, _transform.position.y, 1.5f);
        Quaternion _quat = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
        _collider.GetWorldPose(out _pos, out _quat);
        //_collider.transform.position = _pos;
        //_collider.transform.rotation = _quat;


        _transform.position = _pos;
        _transform.rotation = _quat;
    }






}
