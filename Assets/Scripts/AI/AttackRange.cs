using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public bool playerInRange = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
