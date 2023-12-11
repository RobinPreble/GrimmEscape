using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject infoScreen;
    public GameObject statsScreen;

    public bool isPaused = false;

    public GameObject attackVal;
    public GameObject speedVal;
    public GameObject healthVal;
    
    void Awake()
    {
        pauseScreen.SetActive(false);
        infoScreen.SetActive(false);
        statsScreen.SetActive(false);
        // needs to be tested
        DontDestroyOnLoad(this.gameObject);
        //DontDestroyOnLoad(GameObject.Find("InfoScreen"));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                UnPause();
            } else {
                Pause();
            }
        }
    }

    public void UnPause() {
        Debug.Log("unPause");
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        infoScreen.SetActive(false);
        statsScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Pause() {
        Debug.Log("Pause");
        UpdateStats();
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UpdateStats() {
        attackVal.GetComponent<TextMeshProUGUI>().text = "" + PlayerStats.damage;
        speedVal.GetComponent<TextMeshProUGUI>().text = "" + PlayerStats.speed;
        healthVal.GetComponent<TextMeshProUGUI>().text = "" + PlayerStats.health;
    }

    public void Exit() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
