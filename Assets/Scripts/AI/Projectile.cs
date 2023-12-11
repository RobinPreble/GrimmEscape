using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Collider myCollider;
    UnityEngine.AI.NavMeshAgent agent;
    public GameObject player;
    void Start()
    {
        myCollider = GetComponent<Collider>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
        player = GameObject.Find("Player");
    }

    void Update() {
        if (agent.pathPending) {

        } else {
            agent.destination = player.transform.position;
        }
    }

    void OnCollisionEnter(Collision other) {
        print(other.collider.gameObject.CompareTag("Player"));
        print(other.collider.gameObject.name);
        if (other.collider.gameObject.CompareTag("Player")) {
            player.GetComponent<Player>().TakeDamage(2);
            Destroy(gameObject);
        } else {
            Invoke("Delete", 4);
        }
    }

    void Delete() {
        Destroy(gameObject);
    }
}
