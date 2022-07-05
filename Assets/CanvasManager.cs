using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public int moneyAmount;
    public TextMeshProUGUI moneyAmountText;
    public GameObject deathCanvas;
    public GameObject gameCanvas;
    public GameObject handIntro;
    
    public static CanvasManager instance;

    private void OnEnable()
    {
        DollarBill.dollarCollected += IncreaseMoney;
    }
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

    public void IncreaseMoney()
    {
        moneyAmount += 1;

        moneyAmountText.text = moneyAmount.ToString();
    }
}
