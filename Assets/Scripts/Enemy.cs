using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //Variables
    public int level;
    public int health = 3;
    public bool isCanvasOn = true;
    public  bool isDead = false;
    
    //Canvas
    public GameObject canvas;
    public TextMeshProUGUI levelText;
    public Color clearColor;
    public Color greenColor;
    public Color redColor;
    public Image levelBG;
    
    //References
    public AudioSource audioSource;
    public AudioClip audioClip;
    public SkinnedMeshRenderer meshRenderer;
    public Material deadMat;
    void Start()
    {
        if (isCanvasOn)
        {
            canvas.SetActive(true);
            levelText.text = level + " <size=60%>level";
        }
        else
        {
            canvas.SetActive(false);
        }
    }
    void Update()
    {
        ChangeCanvasBGColor();
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            GetComponentInChildren<Animator>().SetTrigger("death");
            gameObject.layer = 7;
            audioSource.PlayOneShot(audioClip);
            isDead = true;
            canvas.SetActive(false);
            Player.instance.LevelUp(level);
            //Destroy(gameObject);
            ChangeMatToDead();
        }
    }

    public void ChangeMatToDead()
    {
        Material[] matArray = meshRenderer.materials;
        matArray[0] = deadMat;
        meshRenderer.materials = matArray;
    }

    public void ChangeCanvasBGColor()
    {
        if (level == 1)
        {
            levelBG.color = clearColor;
        }
        
        else if (Player.instance.level >= level)
        {
            levelBG.color = greenColor;
        }
        
        else
        {
            levelBG.color = redColor;
        }
    }
}
