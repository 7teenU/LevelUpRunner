using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    public Transform rayPos;
    public LayerMask layerMask;
    public ParticleSystem muzzleParticle;
    public ParticleSystem shellParticle;
    public GameObject bulletPrefab;
    public GameObject firePos;
    public AudioSource fireSoundSource;
    public AudioClip fireSound;

    private GameObject target;

    public float fireRate = 1f;
    private float nextFire = 0f;
    private float distance = 8;

    private bool canShoot = true;
    void Start()
    {
        
    }

    void Update()
    {
        if(!canShoot) return;
        
        RaycastHit hit;
        if (Physics.Raycast(rayPos.position, Vector3.forward, out hit, distance, layerMask))
        {
            target = hit.transform.gameObject;
            Timer();
        }

        else
        {
            target = null;
        }
        
        Debug.DrawRay(rayPos.position, Vector3.forward * distance, Color.red);
        
    }

    public void CanShootOnOff(bool onOff)
    {
        canShoot = onOff;
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
        fireSoundSource.PlayOneShot(fireSound);
        var randVol = Random.Range(-.2f, .2f);
        fireSoundSource.volume = 1.5f + randVol;
        
        GameObject bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, firePos.transform.position,firePos.transform.rotation);
        //bulletInstance.GetComponent<Rigidbody>().AddForce(firePos.transform.forward * Random.Range(1500, 1800));
        bulletInstance.GetComponent<Missile>().target = target.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            //distance = 4;
        }
    }
}
