using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedButtons : Interactable
{
    // Start is called before the first frame update


    public GameObject previous;
    public GameObject next;
    public GameObject chest;
    //public TextMeshPro text;
    Animator anim;
    bool pressed;
    

    void Start()
    {
        interact = false;
        pressed = false;
        anim = gameObject.GetComponent<Animator>();
        //text.color = new Color32(0, 0, 0, 255);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (interact){
            //text.color = new Color32(255, 255, 255, 255);
            anim.SetFloat("trigger", 1);
            pressed = true;
            print("Button pressed");
            if(check()){
                if(next == null){
                    // do something to succed 
                    chest.GetComponent<Animator>().SetFloat("trigger", 1);
                    chest.GetComponent<InteractableAnim>().GetComponent<Collider>().enabled = true;
                    print("finished");
                }
                print("on the right track");
            } else {
                reset();
                print("reset buttons");
            }
            print("pressed" + pressed);
            interact = false;
        }
    }

    void reset(){
        pressed = false;
        anim.SetFloat("trigger", -1);
        //text.color = new Color32(0, 0, 0, 255);
        if(previous != null){
            previous.GetComponent<timedButtons>().reset();
        }
        
    }

    bool check(){
        if(previous != null){
            return pressed && previous.GetComponent<timedButtons>().check();
        } else {
            return pressed;
        }
    }
}
