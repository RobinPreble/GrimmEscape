using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroMeleeHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider thing) {
        print("INRANGE");
        parent.GetComponent<Necromancer>().InvertInRange();
    }

    void OnTriggerExit(Collider thing) {
        print("OUTRANGE");
        parent.GetComponent<Necromancer>().InvertInRange();
    }
}
