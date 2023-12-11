using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int loadLevel;

    public void ChangeLevel() {
        try
        {
            SceneManager.LoadScene("Level" + loadLevel);
        }
        catch (System.Exception)
        {
            Debug.Log("invalid level");
            throw;
        }
    }

    // while testing
    public void StartGame() {
        SceneManager.LoadScene("basement");
    }

    public void StartCutscene() {
        SceneManager.LoadScene("BeginningCutscene");
    }

    // void OnTriggerEnter(Collider other) {
    //     if (other.CompareTag("Player")) {
    //         Debug.Log("changing levels");
    //         ChangeLevel();
    //     }
    // }

    public void Exit() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
