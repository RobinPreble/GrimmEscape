using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSecretButton : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anim;
    Animator b0;
    Animator b1;
    Animator b2;
    Animator b3;
    Animator b4;
    Animator b5;
    Animator b6;
    Animator b7;


    void Start()
    {
        GameObject g0 = GameObject.Find("SwitchSecret_Button");
        GameObject g1 = GameObject.Find("SwitchSecret_Button (1)");
        GameObject g2 = GameObject.Find("SwitchSecret_Button (2)");
        GameObject g3 = GameObject.Find("SwitchSecret_Button (3)");
        GameObject g4 = GameObject.Find("SwitchSecret_Button (4)");
        GameObject g5 = GameObject.Find("SwitchSecret_Button (5)");
        GameObject g6 = GameObject.Find("SwitchSecret_Button (6)");
        GameObject g7 = GameObject.Find("SwitchSecret_Button (7)");
        b0 = g0.GetComponent<Animator>();
        b1 = g1.GetComponent<Animator>();
        b2 = g2.GetComponent<Animator>();
        b3 = g3.GetComponent<Animator>();
        b4 = g4.GetComponent<Animator>();
        b5 = g5.GetComponent<Animator>();
        b6 = g6.GetComponent<Animator>();
        b7 = g7.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(b0.GetFloat("trigger") >= 1 && b2.GetFloat("trigger") >= 1 && b3.GetFloat("trigger") >= 1 && b4.GetFloat("trigger") >= 1 &&
        b1.GetFloat("trigger") < 1 && b5.GetFloat("trigger") < 1&& b6.GetFloat("trigger") < 1 && b7.GetFloat("trigger") < 1) {
            anim.SetFloat("trigger", 1);
        }
        
    }
}
