using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPuzzle : MonoBehaviour
{
    public GameObject puzzleCam;
    public GameObject mainCam;
    public GameObject chest;
    public GameObject agarricus;
    public GameObject cantharellace;
    public GameObject macroleplota;
    public GameObject agaricales;
    public GameObject player;

    public void StartPuzzle() {
        mainCam.SetActive(false);
        puzzleCam.SetActive(true);
        GetComponent<BoxCollider>().enabled = false;
        player.SetActive(false);

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ClosePuzzle() {
        mainCam.SetActive(true);
        puzzleCam.SetActive(false);
        GetComponent<BoxCollider>().enabled = true;
        player.SetActive(true);

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.GetComponent<MushroomPuzzle>().enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ClosePuzzle();
        }
        TestOrder();
    }

    private void TestOrder() {
        if (macroleplota.transform.position.z < agaricales.transform.position.z &&
            agaricales.transform.position.z < agarricus.transform.position.z &&
            agarricus.transform.position.z < cantharellace.transform.position.z) {
                Debug.Log("success");
                ClosePuzzle();
                GetComponent<BoxCollider>().enabled = false;
                chest.GetComponent<Interactable>().locked = false;
                chest.GetComponent<Interactable>().interact = true;
                gameObject.tag = "Untagged";
                gameObject.GetComponent<MushroomPuzzle>().enabled = false;
            }
    }
}
