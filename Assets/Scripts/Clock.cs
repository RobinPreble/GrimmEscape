using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Interactable
{
    // Start is called before the first frame update

    public GameObject ui;
    public GameObject door_key;
    void Start()
    {
        interact = false;
        door_key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(interact){
            ui.GetComponent<Keypad>().ShowPanel();
            interact = false;
        }
    }
}
