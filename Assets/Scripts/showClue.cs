using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showClue : Interactable
{
    // Start is called before the first frame update
    public GameObject clue;

    void Start()
    {
        interact = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(interact){
            clue.GetComponent<Keypad>().ShowPanel();
            clue.GetComponent<Keypad>().Invoke("HidePanel", 3);
            interact = false;
        }
    }
}
