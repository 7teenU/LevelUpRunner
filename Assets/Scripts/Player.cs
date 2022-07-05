using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed;
    public float swipeSpeed = 5;
    public ParticleSystem smokeParticle;
    public Animator anim;
    private Rigidbody rb;
    private float animVal;
    public bool canMove = true;
    public bool canSwipe = true;
    public bool isDead = false;
    public bool hitObstacle = false;
    public bool inFinish = false;
    private Vector3 Translation;

    public int level = 1;

    public TextMeshProUGUI levelText;
    public Shooting Shooting;

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        if(isDead) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            smokeParticle.Play();
            
            StartGame();
        }
        
        if (canMove)
        {
            Vector3 gopos;
            gopos = transform.position += Vector3.forward * (-speed * Time.deltaTime);
            rb.MovePosition(gopos);
        }

        SwipeSystem();
    }

    public void StartGame()
    {
        canMove = true;
        canSwipe = true;
        CamAnimController.instance.anim.SetTrigger("start");
        anim.SetTrigger("start");
        CanvasManager.instance.handIntro.SetActive(false);
    }

    public void LevelUp(int amount)
    {
        if (amount > level)
        {
            level = 1;
            
            levelText.text = level + " <size=60%>level";

            if (inFinish)
            {
                isDead = true;
                canMove = false;
                anim.SetTrigger("dead");
                smokeParticle.Stop();
                Invoke("TurnDeathCanvasOn",1);
            }
        }

        else 
        {
            level += amount;
            levelText.text = level + " <size=60%>level";
        }
    }

    public void TurnDeathCanvasOn()
    {
        CanvasManager.instance.deathCanvas.SetActive(true);
    }

    void SwipeSystem()
    {
        #if UNITY_EDITOR
        if (canSwipe)
        {
            Vector3 rotateVal;
            if (Input.GetMouseButton(0))
            {
                if (Input.GetAxis("Mouse X") != 0)
                {
                    //Input
                    Translation = new Vector3(Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * swipeSpeed;
                    transform.Translate(new Vector3(Translation.x, Translation.y, Translation.z), Space.Self);

                    //Right Left Clamp
                    Vector3 clampedXPos = transform.position;
                    clampedXPos.x = Mathf.Clamp(clampedXPos.x, -2.3f, 2.3f);
                    transform.position = clampedXPos;
                    rotateVal = clampedXPos;
                    //gameObject.transform.DORotate(new Vector3(0, rotateVal.x * 40, 0), 0);
                }
            }
            
        }

        #elif UNITY_IOS || UNITY_ANDROID
        if (canSwipe)
	   {
		if (Input.touchCount > 0)
           {
                Touch touch1 = Input.GetTouch(0);
                if (touch1.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(transform.position.x + touch1.deltaPosition.x * 0.02f, transform.position.y, transform.position.z);
                    Vector3 clampedXPos = transform.position;
                    clampedXPos.x = Mathf.Clamp(clampedXPos.x,-1.7f,1.7f);
                    transform.position = clampedXPos;
	            }
            } 
	    }
        #endif
    }

    IEnumerator ObstacleHit()
    {
        Shooting.CanShootOnOff(false);
        hitObstacle = true;
        canMove = false;
        GetComponent<Collider>().isTrigger = true;
        rb.AddForce(Vector3.back * 3, ForceMode.Impulse);
        anim.SetTrigger("hit");

        yield return new WaitForSeconds(1);

        Shooting.CanShootOnOff(true);
        canMove = true;
        hitObstacle = false;
        GetComponent<Collider>().isTrigger = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(ObstacleHit());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            inFinish = true;
            canSwipe = false;
            speed = -6;
            //UIManager.instance.finishPanel.SetActive(true);
            gameObject.transform.DOMoveX(0, 1);
        }

        if (other.gameObject.CompareTag("Barrier"))
        {
            anim.SetTrigger("roll");
        }
    }
    
    
}
