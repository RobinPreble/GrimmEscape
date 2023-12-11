using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableAlert : MonoBehaviour
{
    GameObject skelly;
    // Start is called before the first frame update
    void Start()
    {
        skelly = GameObject.Find("Skeleton");
    }

    void OnTriggerEnter(Collider thing) {
        if (thing.GetComponent<Collider>().gameObject.CompareTag("Player")) {
            if (skelly.GetComponent<Skel1>().alerted == false) {
                skelly.GetComponent<Skel1>().GetComponent<Animator>().SetTrigger("Alert");
                Invoke("Alert", 1.2f);
            }
        }
    }

    void Alert() {
        skelly.GetComponent<Skel1>().alerted = true;
    }
}
