using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool interact;
    public bool locked;
    public GameObject key;
    public GameObject contents; // optional item inside locked object


    // Start is called before the first frame update
    void Start()
    {
        interact = false;
        if (contents != null)
        {
            contents.GetComponent<Collider>().enabled = false; // prevents you from picking up objects inside locked chests
        }
    }

    public void SetLock(bool locked)
    {
        this.locked = locked;
    }
}