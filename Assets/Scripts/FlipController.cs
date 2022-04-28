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

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GameManager.Current.GameOver();
            //GameManager.Current.gameOverMenu.SetActive(true);
            GameManager.Current.isGameActive = false;
            flipped = true;
        }
    }
}
