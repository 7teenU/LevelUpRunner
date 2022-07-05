using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("MOVEMENT")] 
    [SerializeField] private float speed = 15;
    [SerializeField] private float rotateSpeed = 95;
    
    [Header("REFERENCES")] 
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Transform target;
    [SerializeField] private GameObject explosionPrefab;
    private Enemy _enemy;
    
    [Header("PREDICTION")] 
    private Vector3 _standardPrediction, _deviatedPrediction;
    
    void Start()
    {
        _enemy = target.GetComponent<Enemy>();
        Destroy(gameObject,3);
    }

    private void FixedUpdate() 
    {
        rb.velocity = transform.forward * speed;

        if(target != null)
            RotateBullet();

        if (_enemy.isDead)
        {
            target = null;
        }
    }
    

    private void RotateBullet() 
    {
        var heading = target.transform.position - transform.position;

        var rotation = Quaternion.LookRotation(heading);
        rb.MoveRotation(rotation);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<Enemy>().DecreaseHealth(1);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
