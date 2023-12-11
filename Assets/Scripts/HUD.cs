using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject[] keys;
    public int numKeys = 0;
    public GameObject[] rungs;
    public int numRungs = 0;
    
    public void AddKey() {
        keys[numKeys].SetActive(true);
        numKeys++;
    }

    public void AddRung() {
        rungs[numRungs].SetActive(true);
        numRungs++;
    }

    public void UseKey() {
        numKeys--;
        keys[numKeys].SetActive(false);
    }

    public void UseRung() {
        numRungs--;
        rungs[numRungs].SetActive(false);
    }
}
