using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public static float interactDistance = 0.5f;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public GameObject hud;
    private List<GameObject> interactable = new ();
    public AudioClip itemPickUpSFX;
    public AudioClip UseKeySFX;
    public AudioClip NoKeySFX;
    private GameObject lockedObjContents; // used as workaround for Invoke() not allowing arguments

    void Update()
    {
        // if there is an interactable in range then interact with it when E is pressed
        if (Input.GetKeyDown(KeyCode.E) && interactable.Count != 0)
        {
            Debug.Log("objects in range: " + interactable.Count);
            Interactable selectedObject = GetClosest().GetComponent<Interactable>();

            if (selectedObject.locked) TryUnlock(selectedObject);
            // if unlocked, reverse interact bool
            if (!selectedObject.locked) selectedObject.interact = !selectedObject.interact;

        }
    }

    private void TryUnlock(Interactable lockedObject)
    {
        // if there's no key, it can't be unlocked (use passcode instead)
        if (!lockedObject.key) return;

        if (inventory.ContainsKey(lockedObject.key.name))
        {
            Debug.Log("unlocked " + lockedObject.gameObject.name + " with " + lockedObject.key.name);
            lockedObject.locked = false;
            if (lockedObject.contents != null)
            {
                // wait 2 seconds before enabling collider and allowing object pick up so player has time to see item
                lockedObjContents = lockedObject.contents;
                Invoke("EnableCollider", 2);
            }
            GetComponent<AudioSource>().PlayOneShot(UseKeySFX);
            // remove key from inventory?
            inventory.Remove(lockedObject.key.name);
            hud.GetComponent<HUD>().UseKey();
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(NoKeySFX);
        }
    }

    public bool UseRung()
    {
        if (inventory.ContainsKey("Rung"))
        {
            inventory["Rung"]--;
            if (inventory["Rung"] == 0) inventory.Remove("Rung");
            hud.GetComponent<HUD>().UseRung();
            return true;
        }
        return false;
    }

    private GameObject GetClosest()
    {
        if (interactable.Count == 1)
            return interactable[0];
        GameObject closest = interactable[0];
        float dist = 50f;
        float tempDist;

        foreach (GameObject obj in interactable)
        {
            tempDist = Vector3.Distance(gameObject.transform.position, obj.transform.position);
            if (tempDist < dist)
            {
                dist = tempDist;
                closest = obj;
            }
        }
        Debug.Log("Closest: " + closest.name);
        return closest;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check for items to pick up, must have Item tag and attatched Item script
        if (other.gameObject.CompareTag("Item"))
        {
            String objectName = other.gameObject.GetComponent<Item>().itemName;
            if (objectName.Contains("Key") || objectName.Contains("key"))
            {
                hud.GetComponent<HUD>().AddKey();
            }
            if (objectName.Contains("Rung") || objectName.Contains("rung")
            || objectName.Contains("Prism") || objectName.Contains("prism"))
            {
                hud.GetComponent<HUD>().AddRung();
            }

            if (inventory.ContainsKey(objectName))
            {
                inventory[objectName] += 1;
            }
            else
            {
                inventory.Add(objectName, 1);
            }
            GetComponent<AudioSource>().PlayOneShot(itemPickUpSFX);
            other.gameObject.SetActive(false);
        }
        // this breaks if more than one interactable is in range!!!
        else if (other.gameObject.CompareTag("Interactable"))
        {
            interactable.Add(other.gameObject);
            Debug.Log("In Range: " + other.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // set interactable to null on exit
        if (other.gameObject.CompareTag("Interactable"))
        {
            interactable.Remove(other.gameObject);
        }
    }

    public void MakeNonInteractable(GameObject item)
    {
        item.tag = "Untagged";
        interactable.Remove(item);
    }

    // intended for objects inside locked chests, enabling collider allows player to pick them up
    private void EnableCollider()
    {
        lockedObjContents.GetComponent<Collider>().enabled = true;
        lockedObjContents = null;
    }
}
