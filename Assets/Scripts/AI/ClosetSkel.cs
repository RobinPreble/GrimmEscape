using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetSkel : MonoBehaviour
{
    Collider myCollider;
    Animator anim;

    private GameObject player;
    public GameObject projectilePrefab;
    bool playerInRange = false;
    GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        door = GameObject.Find("Door");
        InvokeRepeating("StateMachine", 0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void StateMachine() {
        var rnd = new System.Random();
        bool open = door.GetComponent<InteractableAnim>().interact && door.GetComponent<InteractableAnim>().locked;
        if (open) {
            if (playerInRange) {
                Melee();
            } else {
                int action = rnd.Next(1, 11);
                if (action <= 6) {
                } else {
                    Ranged();
                }
            }
        }
        
    }

    void OnTriggerEnter(Collider thing) {
        if (thing.GetComponent<Collider>().CompareTag("Player")) {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider thing) {
         if (thing.GetComponent<Collider>().CompareTag("Player")) {
            playerInRange = false;
        }
    }

    void Melee() {
        anim.SetBool("Melee", true);
        player.GetComponent<Player>().TakeDamage(4);
    }

    void Pursue() {
        Vector3 newPos = player.transform.position;
        var noise = new System.Random();
        // newPos.x += noise.Next(-1,2);
        // newPos.z += noise.Next(-1,2);
       // anim.SetBool("Pursuing", true);
    }

    void Stop() {
        // agent.isStopped = true;
        // anim.SetBool("Pursuing", false);
    }

    void Ranged() {
        Vector3 spawnPos = transform.position;
        spawnPos.z -= 3.0f;
        GameObject blast = Instantiate(projectilePrefab, spawnPos, transform.rotation);
        Vector3 target = player.transform.position;
        anim.SetBool("Ranged", true);
        blast.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target;
    }
}
