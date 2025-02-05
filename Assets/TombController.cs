using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TombController : MonoBehaviour
{
    public GameObject tombStonePieces;
    public bool hitted;
    public float zPos;
    public float carPos;
    public float distance;

    private void Start()
    {
        tombStonePieces = GameObject.FindGameObjectWithTag("FracturedPieces");
        //gameObject.GetComponent<Rigidbody>().drag = 20;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = true;
        

        
    }

    private void FixedUpdate()
    {
        zPos = transform.position.z;
        carPos = CarController.Current.distance * 4;
        distance = (zPos - carPos);

        if (distance <= 15)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        if (collision.gameObject.CompareTag("HitPlace"))
        {


            gameObject.GetComponent<Rigidbody>().drag = 0;

        }
    }

    void TurnOffText()
    {
        tombStonePieces.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(10).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(12).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(13).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(14).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(15).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(16).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(17).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(18).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(19).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(20).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(21).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        tombStonePieces.transform.GetChild(22).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
       
    }

    
}
