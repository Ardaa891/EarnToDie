using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipController : MonoBehaviour
{
    public static FlipController Current;

    public bool flipped;

    private void Awake()
    {
        Current = this;
    }

    private void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GameManager.Current.GameOver();
            GameManager.Current.gameOverMenu.SetActive(true);
            GameManager.Current.isGameActive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity = Vector3.zero;
            CarController.Current.crashedCar.SetActive(true);
            CarController.Current.ChangeBlendShape(100);
            flipped = true;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Current.GameOver();
            GameManager.Current.gameOverMenu.SetActive(true);
            GameManager.Current.isGameActive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity = Vector3.zero;
            flipped = true;
        }
    }*/


}
