using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MoreMountains.NiceVibrations;
using DG.Tweening;
using UnityEngine.UI;



public class EnemyController : MonoBehaviour
{
    public static EnemyController Current;
    public List<Collider> RagdollParts = new List<Collider>();

    Rigidbody rb;
    Animator anim;

    public  int xForce, yForce, zForce;
    public int forceAmount = 500;
    public bool onGround;
    
    public float power = 10.0f;
    public float radius = 5.0f;
    Vector3 explosionPos;
    public Animator zombieAnim;
    public Text scoreText;
    public float speed = 5;
    public CapsuleCollider capsule;

    public float slidervalue;
    

    private void Awake()
    {
        SetRagdollParts();
        Current = this;

        /*if(CarController.Current._frontbar4 == 4)
        {
            GetComponent<CapsuleCollider>().enabled = false;
        }*/
    }
    /*private void OnEnable()
    {
        anim.enabled = true;
    }*/

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        xForce = Random.Range(1, 10);
        yForce = Random.Range(15, 25);
        zForce = Random.Range(15, 30);

        explosionPos = transform.position;
        zombieAnim = GetComponent<Animator>();
        speed = 5;
        capsule = GetComponent<CapsuleCollider>();
        slidervalue = HealthBar.Current.slider.value;

       
    }

    private void FixedUpdate()
    {
        Vector3 newpos = transform.position;

        if (Input.GetMouseButton(0))
        {
            zombieAnim.SetBool("walk", true);
            zombieAnim.SetBool("idle", false);

            transform.Translate(0, 0, 0.5f * Time.fixedDeltaTime);
            

        }else if (Input.GetMouseButtonUp(0))
        {
            zombieAnim.SetBool("walk", false);
            zombieAnim.SetBool("idle", true);
            transform.Translate(0, 0, 0 * Time.fixedDeltaTime);
        }

        
    }

    public void SetRagdollParts()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(Collider c in colliders)
        {
            if(c.gameObject != gameObject)
            {
                c.isTrigger = true;
                c.GetComponent<Rigidbody>().useGravity = false;
                RagdollParts.Add(c);
            }
            
            
            
        }

    }

   public void TurnOnRagdoll()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        capsule.enabled = false;

        rb.useGravity = false;
        
        anim.enabled = false;
        anim.avatar = null;

        foreach(Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.GetComponent<Rigidbody>().useGravity = true;
            c.GetComponent<Rigidbody>().AddForce(xForce, yForce, zForce, ForceMode.Impulse);
            //c.attachedRigidbody.velocity = Vector3.zero;
        }
        //MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        StartCoroutine(Die());
    }

   public void TurnOnRagdollv2()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        capsule.enabled = false;

        rb.useGravity = false;

        anim.enabled = false;
        anim.avatar = null;
       
        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.GetComponent<Rigidbody>().useGravity = true;
            c.GetComponent<Rigidbody>().AddForce(1, 1, 2, ForceMode.Impulse);
            //c.attachedRigidbody.velocity = Vector3.zero;
        }
       // MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        StartCoroutine(Die());

        
    }

   private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("Shovel") )
        {
            TurnOnRagdoll();
            
            

        }*/
        /*if(collision.gameObject.CompareTag("Enemy"))
        {
            TurnOnRagdollv2();
            Debug.Log("hit");
        }*/
        /*if (collision.gameObject.CompareTag("Box"))
        {
            TurnOnRagdollv2();
        }*/

        
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitPlace"))
        {
            TurnOnRagdoll();
            //MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;

            if (CarController.Current._doorbar == 0)
            {
                CarController.Current.ChangeScore(5);
            }else if(CarController.Current._doorbar == 1)
            {
                CarController.Current.ChangeScore(10);
            }
            else if (CarController.Current._doorbar == 2)
            {
                CarController.Current.ChangeScore(15);
            }
            else if (CarController.Current._doorbar == 3)
            {
                CarController.Current.ChangeScore(20);
            }

            
            /*if (CarController.Current._frontbar == 0)
            {
                CarController.Current.ChangeHealth(-3);
                CarController.Current.ChangeBlendShape(3);
            }
            else if (CarController.Current._frontbar == 1)
            {
                CarController.Current.ChangeHealth(-2);
                CarController.Current.ChangeBlendShape(2);
            }
            else if(CarController.Current._frontbar == 1 && CarController.Current._frontbar2 == 2)
            {
                CarController.Current.ChangeHealth(-1.5f);
                CarController.Current.ChangeBlendShape(1.5f);
            }
            else if(CarController.Current._frontbar == 1 && CarController.Current._frontbar2 == 2 && CarController.Current._frontbar3 == 3)
            {
                CarController.Current.ChangeHealth(-0.75f);
                CarController.Current.ChangeBlendShape(-0.75f);
            }
            else if (CarController.Current._frontbar == 1 && CarController.Current._frontbar2 == 2 && CarController.Current._frontbar3 == 3 && CarController.Current._frontbar4 == 4)
            {
                CarController.Current.ChangeHealth(-0.30f);
                CarController.Current.ChangeBlendShape(-0.30f);
            }*/

            if (other.CompareTag("Ground"))
            {
                onGround = true;

                if (!onGround)
                {
                    gameObject.SetActive(false);
                }
            }


        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<CapsuleCollider>().enabled = false;
            TurnOnRagdollv2();
            Debug.Log("hit");
            //MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }



    }







}
