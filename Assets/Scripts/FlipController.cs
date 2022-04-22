using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            GameManager.Current.GameOver();

            GameManager.Current.isGameActive = false;
        }
    }
}
