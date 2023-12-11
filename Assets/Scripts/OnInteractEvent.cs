using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnInteractEvent : Interactable
{
    public UnityEvent onInteract;
    
    // if object is interacted with, the event will happen. (edit event in unity editor)
    void Update()
    {
        if (interact && !locked) {
            interact = false;
            onInteract.Invoke();
            Debug.Log(gameObject.name + " interacted with");
        }
    }
}
