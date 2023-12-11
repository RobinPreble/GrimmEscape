using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skel2 : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    private int destPoint = 0;
    public Transform[] points;
    public GameObject player;
    public float fieldOfViewAngle = 110f;
    public float patrollingSpeed;
    public float pursuingSpeed;
    private bool playerInSight = false;
    private RaycastHit hit;
    private AttackRange attackRange;
    private bool attackCoolDown;
    public Transform spawn;
    private Enemy enemyScript;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        attackRange = GetComponentInChildren<AttackRange>();
        enemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().curHealth <= 0) {
            Reset();
        } else if (enemyScript.health <= 0) {
            agent.speed = 0; // prevent skeleton from continuing to move during death animation
            agent.angularSpeed = 0;
        } else if (playerInSight) {
            if (attackRange.playerInRange) {
                Attack();
            } else {
            agent.SetDestination(player.transform.position);
            agent.speed = pursuingSpeed;
            anim.SetBool("Pursuing", true);
            }
        } else {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToNextPoint();
            }
        }

        anim.SetFloat("Forward", agent.velocity.sqrMagnitude);
    }

    private void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    private void OnTriggerStay(Collider other)
    {   
        // Determine if player is in sight
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 enemyToPlayer = other.gameObject.transform.position - transform.position;
            float angleToPlayer = Vector3.Angle(enemyToPlayer, transform.forward);
            bool isAngleUnderHalfAngleOfView = angleToPlayer < fieldOfViewAngle * 0.5f;

            if (isAngleUnderHalfAngleOfView && Physics.Raycast(transform.position + transform.up, 
                enemyToPlayer.normalized, out hit, 8))
            {
                playerInSight = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInSight = false;
            agent.speed = patrollingSpeed;
            anim.SetBool("Pursuing", false);
            // print("Persuing is false");
        }
    }

    void Attack() {
        if (attackCoolDown == false) {
            anim.SetTrigger("Attack");
            player.GetComponent<Player>().TakeDamage(4);
            attackCoolDown = true;
            Invoke("ResetAttackCooldown", 2);
        }
    }

    void ResetAttackCooldown() {
        attackCoolDown = false;
    }

    void Reset() {
        print("Reset!");
        GetComponent<Enemy>().TakeDamage(-(10 - GetComponent<Enemy>().health));
        transform.position = spawn.transform.position;
        playerInSight = false;
    }
}