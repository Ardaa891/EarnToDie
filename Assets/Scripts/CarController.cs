using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class CarController : MonoBehaviour
{
    public static CarController Current;

    public WheelCollider[] wheels;
    public WheelCollider frontLeft, frontRight, backLeft, backRight;
    public Transform frontLeftT, frontRightT, backLeftT, backRightT;
    public Transform parent;
    
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
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI deadHighScoreText;
    public TextMeshProUGUI gasHighScoreText;
    public TextMeshProUGUI flipHighScoreText;
    public Text scoreText;
    
    public  Rigidbody rb;
    int input = 0;
    public int _tire;
    public int _nitro;
    public int _doorbar;
    public int _windowbar;
    public int _frontbar;
    public int _frontbar2;
    public int _frontbar3;
    public int _frontbar4;
    public int armor;

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
    public bool isRoad1, isRoad2, isRoad3, isRoad4;
    public bool hitGround;
    


    public GameObject car;
    public GameObject crashedCar;
    public GameObject tireSpike, doorBar, windowBar, nitro,frontBar,frontbar2,frontbar3, frontleftTire, backLeftTire, backRightTire, frontbar4;
    public GameObject _tombstone;
    //public GameObject nitro1, nitro2;
    public GameObject leftWheel, rightWheel;
    public GameObject fracturedParent;
    public GameObject road1Prefab, road2Prefab, road3Prefab, road4Prefab;
    public GameObject zombies;
    public GameObject newZombies;
    public GameObject carTop;
    
    
    
    public TrailRenderer leftTrail, rightTrail;
    

    public Button nitroUpgradeButton, nitroUpgradeButton2, nitroUpgradeButton3;
    public Button frontBarUpgradeButton, frontBarUpgradeButton2, frontBarUpgradeButton3, frontBarUpgradeButton4;
    public Button tireUpgradeButton;
    public Button doorUpgradeButton, doorUpgradeButton2, doorUpgradeButton3;
    public Button windowUpgradeButton;

    

    private void Awake()
    {
        maxHealth = 100;
        maxGas = 100;
        currentHealth = maxHealth;
        currentGas = maxGas;
        motorPower = 7000;
        breakPower = 1000;
        hitGround = true;
        highScoreText = _tombstone.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        highScoreText.text = (PlayerPrefs.GetInt("maxdistance") + "m").ToString();
        /*_tire = PlayerPrefs.GetInt("tire", 0);
        if (_tire == 1)
        {
            tireSpike.SetActive(true);
            tireUpgradeButton.transform.gameObject.SetActive(false);
        }*/
        _nitro = PlayerPrefs.GetInt("nitro", 0);
        if (_nitro ==1)
        {
            //nitro.SetActive(true);
            isNitro = true;
            hasNitro = true;
            //motorPower = 15000;
            nitroUpgradeButton.transform.gameObject.SetActive(false);
        }
        if (_nitro == 2)
        {
            //nitro.SetActive(true);
            isNitro = true;
            hasNitro = true;
            //motorPower = 15000;
            nitroUpgradeButton2.transform.gameObject.SetActive(false);
            nitroUpgradeButton.transform.gameObject.SetActive(false);
        }
        if (_nitro == 3)
        {
            //nitro.SetActive(true);
            isNitro = true;
            hasNitro = true;
            //motorPower = 15000;
            nitroUpgradeButton3.transform.gameObject.SetActive(false);
            nitroUpgradeButton2.transform.gameObject.SetActive(false);
            nitroUpgradeButton.transform.gameObject.SetActive(false);
        }

        /* _windowbar = PlayerPrefs.GetInt("window", 0);
         if (_windowbar ==1)
         {

             windowBar.SetActive(true);
             windowUpgradeButton.transform.gameObject.SetActive(false);

         }*/

        _frontbar = PlayerPrefs.GetInt("frontbar", 0);
        _frontbar2 = PlayerPrefs.GetInt("frontbar2", 0);
        _frontbar3 = PlayerPrefs.GetInt("frontbar3", 0);
        _frontbar4 = PlayerPrefs.GetInt("frontbar4", 0);
        armor = PlayerPrefs.GetInt("armor",0);
        if(_frontbar == 1)
        {
            frontBar.SetActive(true);
            frontBarUpgradeButton.transform.gameObject.SetActive(false);

            
        }
        if (_frontbar2 == 2)
        {
            frontbar2.SetActive(true);
            frontBarUpgradeButton2.transform.gameObject.SetActive(false);
            frontBarUpgradeButton.transform.gameObject.SetActive(false);


        }
        if (_frontbar3 == 3)
        {
            frontbar3.SetActive(true);
            frontBarUpgradeButton3.transform.gameObject.SetActive(false);
            frontBarUpgradeButton2.transform.gameObject.SetActive(false);
            frontBarUpgradeButton.transform.gameObject.SetActive(false);
            frontleftTire.SetActive(true);
            backLeftTire.SetActive(true);
            backRightTire.SetActive(true);


        }
        if (_frontbar4 == 4)
        {
            frontbar4.SetActive(true);
            frontBarUpgradeButton4.transform.gameObject.SetActive(false);
            frontBarUpgradeButton.transform.gameObject.SetActive(false);
            frontBarUpgradeButton2.transform.gameObject.SetActive(false);
            frontBarUpgradeButton3.transform.gameObject.SetActive(false);


        }

        Instantiate(_tombstone, new Vector3(0, PlayerPrefs.GetFloat("lastypos"), PlayerPrefs.GetInt("maxdistance") *4), Quaternion.Euler(PlayerPrefs.GetFloat("lastxrot"),0,0));
        _tombstone.SetActive(true);
        


    }
    private void Start()
    {
        
        Current = this;
        currentGas = maxGas;
        rb = GetComponent<Rigidbody>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        score = PlayerPrefs.GetInt("currentscore");
        parent = GameObject.FindGameObjectWithTag("Level").transform.GetChild(0).transform;
        rb.centerOfMass = centerOfMass;
        HealthBar.Current.SetMaxHealth(maxHealth);
        GasBar.Current.SetMaxGas(maxGas);

        

        _doorbar = PlayerPrefs.GetInt("doorbar", 0);
        if (_doorbar == 1)
        {
            isDoorBar = true;
            hasDoorBar = true;
            //doorBar.SetActive(true);
            doorUpgradeButton.transform.gameObject.SetActive(false);

        }
        if (_doorbar == 2)
        {
            isDoorBar = true;
            hasDoorBar = true;
            //doorBar.SetActive(true);
            doorUpgradeButton2.transform.gameObject.SetActive(false);
            doorUpgradeButton.transform.gameObject.SetActive(false);

        }
        if (_doorbar == 3)
        {
            isDoorBar = true;
            hasDoorBar = true;
            //doorBar.SetActive(true);
            doorUpgradeButton3.transform.gameObject.SetActive(false);
            doorUpgradeButton2.transform.gameObject.SetActive(false);
            doorUpgradeButton.transform.gameObject.SetActive(false);

        }




        if (score < 100)
        {
            nitroUpgradeButton.interactable = false;
            frontBarUpgradeButton.interactable = false;
            //tireUpgradeButton.interactable = false;
            //windowUpgradeButton.interactable = false;
            doorUpgradeButton.interactable = false;


        }
        if (score < 200)
        {
            nitroUpgradeButton2.interactable = false;
            frontBarUpgradeButton2.interactable = false;
            //tireUpgradeButton.interactable = false;
            //windowUpgradeButton.interactable = false;
            doorUpgradeButton2.interactable = false;


        }
        if (score < 300)
        {
            nitroUpgradeButton3.interactable = false;
            frontBarUpgradeButton3.interactable = false;
            //tireUpgradeButton.interactable = false;
            //windowUpgradeButton.interactable = false;
            doorUpgradeButton3.interactable = false;


        }
        if (score < 400)
        {
            frontBarUpgradeButton3.interactable = false;
            frontBarUpgradeButton4.interactable = false;
            


        }

        if (_nitro ==1)
        {
            
            nitroUpgradeButton.transform.gameObject.SetActive(false);
            nitroUpgradeButton2.transform.gameObject.SetActive(true);
        }

        if (_nitro == 2)
        {

            nitroUpgradeButton2.transform.gameObject.SetActive(false);
            nitroUpgradeButton3.transform.gameObject.SetActive(true);
        }
        if (_nitro == 3)
        {

            
            nitroUpgradeButton3.transform.gameObject.SetActive(false);
        }

        if (_frontbar == 1)
        {
            
            frontBarUpgradeButton.transform.gameObject.SetActive(false);
            frontBarUpgradeButton2.transform.gameObject.SetActive(true);
        }
        if (_frontbar == 2)
        {

            frontBarUpgradeButton2.transform.gameObject.SetActive(false);
            frontBarUpgradeButton3.transform.gameObject.SetActive(true);
        }
        if (_frontbar == 3)
        {

            
            frontBarUpgradeButton3.transform.gameObject.SetActive(false);
        }
        if (_frontbar == 4)
        {


            frontBarUpgradeButton4.transform.gameObject.SetActive(false);
        }
        if (_doorbar == 1)
        {

            doorUpgradeButton.transform.gameObject.SetActive(false);
            doorUpgradeButton2.transform.gameObject.SetActive(true);
        }
        if (_doorbar == 2)
        {

            doorUpgradeButton2.transform.gameObject.SetActive(false);
            doorUpgradeButton3.transform.gameObject.SetActive(true);
        }
        if (_doorbar == 3)
        {

            
            doorUpgradeButton3.transform.gameObject.SetActive(false);
        }
        /*if(_tire == 1)
        {
            tireUpgradeButton.interactable = false;
            tireUpgradeButton.transform.gameObject.SetActive(false);
        }*/



    }

    private void Update()
    {
        if (score < 100)
        {
            nitroUpgradeButton.interactable = false;
            frontBarUpgradeButton.interactable = false;
            //tireUpgradeButton.interactable = false;
            //windowUpgradeButton.interactable = false;
            doorUpgradeButton.interactable = false;


        }
        if (score < 250)
        {
            //nitroUpgradeButton2.interactable = false;
            frontBarUpgradeButton2.interactable = false;
            //tireUpgradeButton.interactable = false;
            //windowUpgradeButton.interactable = false;
            //doorUpgradeButton2.interactable = false;


        }
        if (score < 300)
        {
            nitroUpgradeButton2.interactable = false;
            frontBarUpgradeButton2.interactable = false;
            //tireUpgradeButton.interactable = false;
            //windowUpgradeButton.interactable = false;
            doorUpgradeButton2.interactable = false;


        }
        if(score < 500)
        {
            nitroUpgradeButton3.interactable = false;
            doorUpgradeButton3.interactable = false;
            frontBarUpgradeButton4.interactable = false;
        }
        if (score < 600)
        {

            frontBarUpgradeButton4.interactable = false;



        }
        if (_nitro == 1)
        {

            nitroUpgradeButton.transform.gameObject.SetActive(false);
            nitroUpgradeButton2.transform.gameObject.SetActive(true);
        }

        if (_nitro == 2)
        {

            nitroUpgradeButton2.transform.gameObject.SetActive(false);
            nitroUpgradeButton3.transform.gameObject.SetActive(true);
        }
        if (_nitro == 3)
        {


            nitroUpgradeButton3.transform.gameObject.SetActive(false);
        }

        if (_frontbar == 1)
        {

            frontBarUpgradeButton.transform.gameObject.SetActive(false);
            frontBarUpgradeButton2.transform.gameObject.SetActive(true);
        }
        if (_frontbar2 == 2)
        {

            frontBarUpgradeButton2.transform.gameObject.SetActive(false);
            frontBarUpgradeButton3.transform.gameObject.SetActive(true);
        }
        if (_frontbar3 == 3)
        {


            frontBarUpgradeButton4.transform.gameObject.SetActive(true);
            frontBarUpgradeButton3.transform.gameObject.SetActive(false);
        }
        if (_frontbar4 == 4)
        {


            frontBarUpgradeButton4.transform.gameObject.SetActive(false);
        }
        if (_doorbar == 1)
        {

            doorUpgradeButton.transform.gameObject.SetActive(false);
            doorUpgradeButton2.transform.gameObject.SetActive(true);
        }
        if (_doorbar == 2)
        {

            doorUpgradeButton2.transform.gameObject.SetActive(false);
            doorUpgradeButton3.transform.gameObject.SetActive(true);
        }
        if (_doorbar == 3)
        {


            doorUpgradeButton3.transform.gameObject.SetActive(false);
        }


        if (GameManager.Current.isGameActive)
        {
            yPos = transform.position.y;
            generalSpeed = rb.velocity.z;
            Physics.gravity = new Vector3(0, -20, 0);
            distance = (transform.position.z / 4);
            _distance = Mathf.RoundToInt(distance);
            PlayerPrefs.SetInt("HighScore", _distance);


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
            frontLeft.motorTorque = motorPower * input;
            frontRight.motorTorque = motorPower * input;
            UpdateWheelPoses();

            if (leftWheel.GetComponent<TrailScript>().onGround)
            {
                leftTrail.emitting = true;
            }
            else
            {
                leftTrail.emitting = false;
            }

            if (rightWheel.GetComponent<TrailScript>().onGround)
            {
                rightTrail.emitting = true;
            }
            else
            {
                rightTrail.emitting = false;
            }


            if (Input.GetMouseButton(0))
            {
                input = 1;
                isBreaking = false;

                frontRight.brakeTorque = 0;
                frontLeft.brakeTorque = 0; 
                backLeft.brakeTorque = 0;
                backRight.brakeTorque = 0;

                zombieRun = true;
                if(_nitro == 0)
                {
                    ChangeGas(-0.1f);
                }else if(_nitro == 1)
                {
                    ChangeGas(-0.05f);
                }else if (_nitro == 2)
                {
                    ChangeGas(-0.025f);
                }else if (_nitro == 3)
                {
                    ChangeGas(-0.0125f);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                input = 0;

                isBreaking = true;

                backLeft.brakeTorque = breakPower;
                backRight.brakeTorque = breakPower;

                zombieRun = false;


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
                //car.SetActive(false);
                GameManager.Current.GameOver();
                GameManager.Current.gameOverMenu.SetActive(true);
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                PlayerPrefs.SetInt("maxdistance", _distance);
                PlayerPrefs.SetFloat("lastypos", yPos);
                PlayerPrefs.SetFloat("lastxrot", lastXRot);

                GameManager.Current.gameOverMenu.SetActive(true);
                deadHighScoreText.text = _distance.ToString() + " m";
                


            }else if(currentGas <= 0)
            {
                GameManager.Current.GameOver();
                GameManager.Current.gasGameOverMenu.SetActive(true);
                //GameManager.Current.isGameActive = false;
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                PlayerPrefs.SetInt("maxdistance", _distance);
                PlayerPrefs.SetFloat("lastypos", yPos);
                PlayerPrefs.SetFloat("lastxrot", lastXRot);
                GameManager.Current.gasGameOverMenu.SetActive(true);
                gasHighScoreText.text = _distance.ToString() + " m";
            }

            

           
            /*if (hasNitro)
            {
                motorPower = 10000;
            }else if (!hasNitro && generalSpeed <= 20)
            {
                //SetMotorPower();
            }*/

            

            if (SetParent.current.smashedTomb)
            {
                StartCoroutine(DestroyTomb());
                highScoreText = null;
            }

            if (hitGround)
            {
                if(transform.rotation.eulerAngles.x > 80 && transform.rotation.eulerAngles.x < 91)
                {
                    GameManager.Current.GameOver();
                    GameManager.Current.gameOverMenu.SetActive(true);
                    GameManager.Current.isGameActive = false;
                   rb.velocity = Vector3.zero;
                    rb.isKinematic = true;
                    crashedCar.SetActive(true);
                   ChangeBlendShape(100);

                }
            }



        }
        else
        {
            return;
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
        HealthBar.Current.SetHealth(currentHealth);
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

    /*public void Tire()
    {
        isTireSpike = true;
        hasTireSpike = true;
        tireSpike.SetActive(true);
        GameManager.Current.StartGame();
        _tire = 1;
        PlayerPrefs.SetInt("tire", _tire);
        ChangeScore(-50);

        clickedTire = true;
    }*/
    public void Nitro()
    {
        isNitro = true;
        hasNitro = true;
        //nitro.SetActive(true);
        //GameManager.Current.StartGame();
        _nitro = 1;
        PlayerPrefs.SetInt("nitro", _nitro);
        ChangeScore(-100);

        clickedNitro = true;
    }

    public void Nitro2()
    {
        isNitro = true;
        hasNitro = true;
       // nitro.SetActive(true);
        //GameManager.Current.StartGame();
        _nitro = 2;
        PlayerPrefs.SetInt("nitro", _nitro);
        ChangeScore(-300);

        clickedNitro = true;
    }

    public void Nitro3()
    {
        isNitro = true;
        hasNitro = true;
        //nitro.SetActive(true);
       // GameManager.Current.StartGame();
        _nitro = 3;
        PlayerPrefs.SetInt("nitro", _nitro);
        ChangeScore(-500);

        clickedNitro = true;
    }

    public void FrontBar()
    {
        isFrontBar = true;
        hasFrontBar = true;
        frontBar.SetActive(true);
        //GameManager.Current.StartGame();
        _frontbar = 1;
        armor = 1;
        PlayerPrefs.SetInt("armor", armor);
        PlayerPrefs.SetInt("frontbar", _frontbar);
        ChangeScore(-100);

        clickedFrontBar = true;
    }

    public void FrontBar2()
    {
        isFrontBar = true;
        hasFrontBar = true;
        frontbar2.SetActive(true);
        //GameManager.Current.StartGame();
        _frontbar2 = 2;
        armor = 2;
        PlayerPrefs.SetInt("armor", armor);
        PlayerPrefs.SetInt("frontbar2", _frontbar2);
        ChangeScore(-250);

        clickedFrontBar = true;
    }

    public void FrontBar3()
    {
        isFrontBar = true;
        hasFrontBar = true;
        frontbar3.SetActive(true);
        frontleftTire.SetActive(true);
        backLeftTire.SetActive(true);
        backRightTire.SetActive(true);
        //GameManager.Current.StartGame();
        _frontbar3 = 3;
        armor = 3;
        PlayerPrefs.SetInt("armor", armor);
        PlayerPrefs.SetInt("frontbar3", _frontbar3);
        ChangeScore(-400);

        clickedFrontBar = true;
    }
    public void FrontBar4()
    {
        isFrontBar = true;
        hasFrontBar = true;
        frontbar4.SetActive(true);
       // GameManager.Current.StartGame();
        _frontbar4 = 4;
        armor = 4;
        PlayerPrefs.SetInt("armor", armor);
        PlayerPrefs.SetInt("frontbar4", _frontbar4);
        ChangeScore(-600);

        clickedFrontBar = true;
    }
    public void DoorBar()
    {
        isDoorBar = true;
        hasDoorBar = true;
        //doorBar.SetActive(true);
       // GameManager.Current.StartGame();
        _doorbar = 1;
        PlayerPrefs.SetInt("doorbar", _doorbar);
        ChangeScore(-100);

        clickedDoorBar = true;

    }

    public void DoorBar2()
    {
        isDoorBar = true;
        hasDoorBar = true;
        //doorBar.SetActive(true);
        //GameManager.Current.StartGame();
        _doorbar = 2;
        PlayerPrefs.SetInt("doorbar", _doorbar);
        ChangeScore(-300);

        clickedDoorBar = true;

    }

    public void DoorBar3()
    {
        isDoorBar = true;
        hasDoorBar = true;
        //doorBar.SetActive(true);
        //GameManager.Current.StartGame();
        _doorbar = 3;
        PlayerPrefs.SetInt("doorbar", _doorbar);
        ChangeScore(-500);

        clickedDoorBar = true;

    }
    /* public void WindowBar()
     {
         isWindowBar = true;
         hasWindowBar = true;
         windowBar.SetActive(true);
         GameManager.Current.StartGame();
         _windowbar = 1;
         PlayerPrefs.SetInt("window", _windowbar);
         ChangeScore(-50);

         clickedWindowBar = true;

     }*/

    /*public void SetMotorPower()
    {
        motorPower = 7000.0f;
        hasNitro = false;
    }*/

    void UpdateWheelPoses()
    {
        SetBackRightPose(backRight, backRightT);
        SetBacktLeftPose(backLeft, backLeftT);
        SetFrontLeftPose(frontLeft, frontLeftT);
        SetFrontRightPose(frontRight, frontRightT);
    }


    void SetFrontLeftPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = new Vector3(1, _transform.position.y, -1.5f);
        Quaternion _quat = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;

    }
    void SetFrontRightPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = new Vector3(-1, _transform.position.y, -1.5f);
        Quaternion _quat = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    void SetBacktLeftPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = new Vector3(1, _transform.position.y, 1.5f);
        Quaternion _quat = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    void SetBackRightPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = new Vector3(-1, _transform.position.y, 1.5f);
        Quaternion _quat = Quaternion.Euler(_transform.rotation.x, 0, _transform.rotation.z);
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }


    IEnumerator DestroyTomb()
    {
        yield return new WaitForSecondsRealtime(1.2f);

        fracturedParent.SetActive(false);

    }





}
