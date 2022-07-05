using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextMeshProFade : MonoBehaviour
{
    public float duration;
    public float delay;
    public TextMeshProUGUI text;

    public bool from = false;
    void Start()
    {
        if (from)
        {
            text.DOFade(1, duration).SetDelay(delay).From();
        }
        
        else
        {
            text.DOFade(0, duration).SetDelay(delay);
        }
    }

    void Update()
    {
        
    }
}
