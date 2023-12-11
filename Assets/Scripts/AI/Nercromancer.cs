using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public float rayDistance = 10f;
    Collider myCollider;
    Animator anim;
    private UnityEngine.AI.NavMeshAgent agent;
    public GameObject player;
    public bool playerSpotted = false;
    public bool playerInRange = false;
    public int damage = 6;
    public GameObject projectilePrefab;
    public AudioClip projectileSFX;
    public AudioClip meleeSFX;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myCollider = GetComponent<Collider>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
        InvokeRepeating("StateMachine", 0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {   
    }

    void StateMachine() {
        anim.SetBool("Ranged", false);
        anim.SetBool("Melee", false);
        anim.SetBool("Pursuing", false);
        agent.isStopped = false;
        var rnd = new System.Random();
        if (playerSpotted) {
            if (playerInRange) {
                // Melee
                 Stop();
                 Invoke("Melee", 2);
            } else {
                // Either pursue or Blast
                int action = rnd.Next(1,2);
                if (action == 1) {
                    // Pursue
                    Pursue();
                } else {
                    // Blast
                    print("Blasting");
                    Stop();
                    Ranged();

                }
            }
        } else {
            // Move
            int direction = rnd.Next(1,5);
            Move(direction);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Collider>().CompareTag("Player")) {
            player = other.GetComponent<Collider>().gameObject;
            playerSpotted = true;
        }
    }

    // Move in the direction specified by 'state' in the necro state machine
    void Move(int state) {
        agent.isStopped = false;
        Direction direction = new Direction(state);
        Vector3 newPos = transform.position;
        switch(direction.dir) {
            case DirectionState.Forward:
                newPos.z -= 2;
                break;
            case DirectionState.Backward:
                newPos.z +=2;
                break;
            case DirectionState.Left:
                newPos.x += 2;
                break;
            case DirectionState.Right:
                newPos.x -= 2;
                break;
        }
        agent.destination = newPos;
    }

    void Pursue() {
        var rnd = new System.Random();
        Vector3 newPos = player.transform.position;
        newPos.x += rnd.Next(-1,1);
        newPos.z += rnd.Next(-1,1);
        agent.destination = newPos;
        anim.SetBool("Pursuing", true);
    }

    void Melee() {
        GetComponent<AudioSource>().PlayOneShot(meleeSFX);
        player.GetComponent<Player>().TakeDamage(6);
        return;
    }

    void Stop() {
        agent.isStopped = true;
        anim.SetBool("Pursuing", false);
    }

    void Ranged() {
        //BUG:
        Vector3 spawnPos = transform.position;
        spawnPos.z -= 3.0f;
        GameObject blast = Instantiate(projectilePrefab, spawnPos, transform.rotation);
        Vector3 target = player.transform.position;
        anim.SetBool("Ranged", true);
        GetComponent<AudioSource>().PlayOneShot(projectileSFX);
        blast.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target;
        GetComponent<AudioSource>().PlayOneShot(projectileSFX);
    }

    void Defend() {
        //TODO
    }

    public void InvertInRange() {
        if (playerInRange == true) {
            playerInRange = false;
        } else {
            playerInRange = true;
        }
    } 
}

enum DirectionState {
    Forward = 1,
    Backward = 2,
    Left = 3,
    Right = 4,
}

class Direction {
    public DirectionState dir;

    public Direction(int direction) {
        switch(direction) {
            case 1:
                dir = DirectionState.Forward;
                break;
            case 2:
                dir = DirectionState.Backward;
                break;
            case 3:
                dir = DirectionState.Left;
                break;
            case 4:
                dir = DirectionState.Right;
                break;
        }
    }
}
