using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAnim : Interactable
{
    // Start is called before the first frame update
    Animator anim;
    float triggerCounter;


    void Start()
    {
        interact = false;
        anim = gameObject.GetComponent<Animator>();
        triggerCounter = 1;

        if (contents != null)
        {
            contents.GetComponent<Collider>().enabled = false; // prevents you from picking up objects inside locked chests
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interact && !locked)
        {
            anim.SetFloat("trigger", triggerCounter);
            if (contents != null)
            {
                contents.GetComponent<Collider>().enabled = true; // prevents you from picking up objects inside locked chests
            }
            if (triggerCounter >= 1)
            {
                triggerCounter = -1;
            }
            else
            {
                triggerCounter = 1;
            }
            interact = false;
        }
    }

    public void open(){
        anim.SetFloat("trigger", 1);
        contents.GetComponent<Collider>().enabled = true;
    }


}