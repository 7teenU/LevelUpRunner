using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DollarBill : MonoBehaviour 
{
    public GameObject model;
    public GameObject plusCanvas;
    public static UnityAction dollarCollected;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(plusCanvas,gameObject.transform.position + new Vector3(0,0,2),Quaternion.identity);
            model.SetActive(false);
            dollarCollected.Invoke();
        }
    }
}
