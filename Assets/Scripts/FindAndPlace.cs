using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAndPlace : Interactable
{
    // Start is called before the first frame update
    //this is put on old object
    public GameObject newObject;
    public GameObject chest;
    

    void Start()
    {
        interact = false;
        locked = true; 
        newObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(interact && !locked){
            newObject.SetActive(true);
            chest.GetComponent<Animator>().SetFloat("trigger", 1);
            chest.GetComponent<InteractableAnim>().GetComponent<Collider>().enabled = true;
            interact = false;
        }
    }
}
