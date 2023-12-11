using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Transform[] rungs;
    public GameObject player;
    public bool climbable;
    public int numRungs = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rungs = gameObject.GetComponentsInChildren<Transform>(true);
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact() {
        if (climbable) {
            ClimbLadder();
        } else {
            if (player.GetComponent<PlayerInteractions>().UseRung()) {
                AddRung();
            } else {
                Debug.Log("not enough rungs");
            }
        }
    }

    private void AddRung() {
        Debug.Log(transform.childCount);
        foreach (var rung in rungs) {
            if (!rung.gameObject.activeSelf) {
                rung.gameObject.SetActive(true);
                numRungs++;
                if (numRungs == transform.childCount) climbable = true;
                return;
            }
        }
        Interact();
    }

    private void ClimbLadder() {
        player.transform.position = new Vector3(30.6f, 9.3f, -6.5f);
    }
}
