using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimController : MonoBehaviour
{
    public Animator anim;

    public static CamAnimController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
