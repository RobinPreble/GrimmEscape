using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneDialogue : Interactable
{
    public String[] dialogue;

    private int currentLine = 0;
    public GameObject dialogueBox;
    public TextMeshProUGUI words;
    public TextMeshProUGUI nameLabel;
    public bool areaTrigger;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        Display();
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void Display() {
        dialogueBox.SetActive(true);
        if (currentLine == dialogue.Length) {
            CloseDialogue();
            return;
        }
        if (dialogue[currentLine][0] == '[') {
            //DialogueEvent();
        } else {
            words.text = dialogue[currentLine];
        }
        currentLine++;
    }

    public void CloseDialogue() {
        dialogueBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
