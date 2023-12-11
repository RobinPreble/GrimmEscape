using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interact == true){
            gameObject.SetActive(false);
        }
    }
}
