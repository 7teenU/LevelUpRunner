using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAI : MonoBehaviour
{
    [Header("MOVEMENT")] 
    [SerializeField] private float speed = 15;
    [SerializeField] private float rotateSpeed = 95;
    
    [Header("REFERENCES")] 
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Transform target;
    [SerializeField] private GameObject explosionPrefab;
    
    [Header("PREDICTION")] 
    private Vector3 _standardPrediction, _deviatedPrediction;
    
    void Start()
    {
        Destroy(gameObject,3);
    }

    private void FixedUpdate() 
    {
        rb.velocity = transform.forward * speed;

        RotateBullet();
    }
    

    private void RotateBullet() 
    {
        var heading = Player.instance.transform.position - transform.position;

        var rotation = Quaternion.LookRotation(heading);
        rb.MoveRotation(rotation);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
