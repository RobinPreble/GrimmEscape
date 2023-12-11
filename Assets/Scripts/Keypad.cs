using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    public String keycode;
    public TMP_InputField textInput;
    public GameObject codePanel;
    private String attempt = "";
    public UnityEvent onCorrectCode;

    void Start() {
        codePanel.SetActive(false);
    }
    
    public void ShowPanel() {
        codePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HidePanel() {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        codePanel.SetActive(false);
    }

    public void ButtonPressed(String button) {
        attempt += button;
        if (attempt.Length == keycode.Length) {
            if (attempt.Equals(keycode)) {
                Success();
            } else {
                WrongCode();
            }
        }
    }

    private void Success() {
        Debug.Log("correct code");
        attempt = "";
        onCorrectCode.Invoke();
        HidePanel();
    }

    private void WrongCode() {
        Debug.Log("wrong code");
        attempt = "";
        HidePanel();
    }

    public void TestInput() {
        Debug.Log(textInput.text);
        if (textInput.text.Equals(keycode)) {
            Success();
        } else {
            WrongCode();
        }
    }

}
