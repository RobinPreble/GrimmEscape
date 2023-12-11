using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalPuzzle : Interactable
{
    // Start is called before the first frame update


    public GameObject prismKey1;
    public GameObject prismKey2;
    public GameObject prismKey3;
    public GameObject prismKey4;
    public GameObject prismReveal1;
    public GameObject prismReveal2;
    public GameObject prismReveal3;
    public GameObject prismReveal4;
    public GameObject lever;
    public GameObject Necro;




    void Start()
    {
        prismReveal1.SetActive(false);
        prismReveal2.SetActive(false);
        prismReveal3.SetActive(false);
        prismReveal4.SetActive(false);
        interact = false;
        locked = false;
        lever.GetComponent<OnInteractEvent>().locked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(interact ){
            if(!prismKey1.activeSelf){
                prismReveal1.SetActive(true);
            }
            if(!prismKey2.activeSelf){
                prismReveal2.SetActive(true);
            }
            if(!prismKey3.activeSelf){
                prismReveal3.SetActive(true);
            }
            if(!prismKey4.activeSelf){
                prismReveal4.SetActive(true);
            }
            if(prismReveal1.activeSelf && prismReveal2.activeSelf && prismReveal3.activeSelf && prismReveal4.activeSelf){
                lever.GetComponent<Interactable>().locked = false;
                //do something else to show that there is a good thing

                Invoke("LastStand", 5);
            }
            interact = false;
        }
    }

    void LastStand() {
        Transform spawn = GameObject.Find("NecroSpawn").transform;
        Instantiate(Necro, spawn);
        StartCoroutine(WaitForDeath());
    }

    private IEnumerator WaitForDeath() {
        while (GameObject.Find("MagePurple(Clone)").GetComponent<Enemy>().health > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("EndingCutscene");
    }
}
