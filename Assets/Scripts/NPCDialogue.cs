using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NPCDialogue : Interactable
{
    public String[] dialogue;
    public String npcName;

    private int currentLine = 0;
    public GameObject dialogueBox;
    public TextMeshProUGUI words;
    public TextMeshProUGUI nameLabel;
    public bool areaTrigger;

    public UnityEvent dialogueEvent;
    
    public void SayLine() {
        dialogueBox.SetActive(true);
        if (currentLine == dialogue.Length) {
            CloseDialogue();
            return;
        }
        if (dialogue[currentLine][0] == '[') {
            DialogueEvent();
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

    private void StartDialogue() {
        Debug.Log("interacted with " + npcName);
        dialogueBox.SetActive(true);
        nameLabel.text = npcName;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        SayLine();
    }

    private void DialogueEvent() {
        switch (dialogue[currentLine])
        {
            case "[Event]":
            {
                dialogueBox.SetActive(false);
                dialogueEvent.Invoke();
                break;
            }
        }
    }

    void OnTriggerEnter() {
        if (areaTrigger) {
            StartDialogue();
        }
    }
}
