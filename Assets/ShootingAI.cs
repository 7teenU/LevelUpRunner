using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAI : MonoBehaviour
{
    public Transform rayPos;
    public LayerMask layerMask;
    public ParticleSystem muzzleParticle;
    public ParticleSystem shellParticle;
    public GameObject bulletPrefab;
    public GameObject firePos;
    public AudioSource fireSoundSource;
    public AudioClip fireSound;
    public GameObject model;

    public Enemy enemy;

    private GameObject target;

    public float fireRate = 1f;
    private float nextFire = 0f;
    
    public  bool inRadius = false;

    void Start()
    {
        
    }

    void Update()
    {

        if (inRadius && !enemy.isDead)
        {
            target = Player.instance.transform.gameObject;
            Timer();
            RotateOnY();
        }
    }

    public void RotateOnY()
    {
        Vector3 v = new Vector3(0, Player.instance.transform.rotation.y, 0);
        transform.localRotation = Quaternion.Euler(v);
    }

    public void Timer()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        muzzleParticle.Play();
        shellParticle.Play();
        //fireSoundSource.PlayOneShot(fireSound);
        //var randVol = Random.Range(-.2f, .2f);
        //fireSoundSource.volume = 1.5f + randVol;
        
        GameObject bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, firePos.transform.position,firePos.transform.rotation);
        //bulletInstance.GetComponent<Rigidbody>().AddForce(firePos.transform.forward * Random.Range(1500, 1800));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRadius = false;
        }
    }
}
