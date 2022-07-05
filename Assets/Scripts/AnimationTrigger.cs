using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTrigger : MonoBehaviour
{
    public UnityEvent animEvent;
    public UnityEvent animEvent2;
    void Start()
    {
        
    }

    public void InvokeEvent()
    {
        animEvent.Invoke();
    }
    
    public void InvokeEvent2()
    {
        animEvent2.Invoke();
    }
}
