using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skel1 : MonoBehaviour
{
    public bool alerted = false;
    bool playerInRange = false;
    private UnityEngine.AI.NavMeshAgent agent;
    Collider myCollider;
    Animator anim;
    public GameObject player;
    public GameObject spawn;
    private bool attackCoolDown;
    private Enemy enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
        spawn = GameObject.Find("SkellySpawn");
        enemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update() {
        if (player.GetComponent<Player>().curHealth <= 0) {
            Reset();
        } else if (enemyScript.health <= 0) {
            agent.speed = 0; // prevent skeleton from continuing to move during death animation
            agent.angularSpeed = 0;
        } else if (alerted && !playerInRange) {
            Pursue();
        } else if (alerted && playerInRange) {
            // Attack
            agent.isStopped = true;
            Attack();
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
            agent.isStopped = false;
        }

    }

    void Attack() {
        if (attackCoolDown == false) {
            anim.SetTrigger("Attack");
            player.GetComponent<Player>().TakeDamage(4);
            attackCoolDown = true;
            if (player.GetComponent<Player>().curHealth <= 0) {
            Invoke("Reset", 2);
            }
            Invoke("ResetAttackCooldown", 2);
        }
    }
    void ResetAttackCooldown() {
        attackCoolDown = false;
    }

    // Reset upon player death
    void Reset() {
        print("Reset!");
        GetComponent<Enemy>().TakeDamage(-(10 - GetComponent<Enemy>().health));
        print(GetComponent<Enemy>().health);
        alerted = false;
        transform.position = spawn.transform.position;
        agent.destination = spawn.transform.position;
        anim.SetFloat("Forward", 0);
    }


    // Advance toward the player
    void Pursue() {
        agent.destination = player.transform.position;
        anim.SetFloat("Forward", agent.velocity.sqrMagnitude);
    }

    public void Alert() {
        alerted = true;
    }
}
