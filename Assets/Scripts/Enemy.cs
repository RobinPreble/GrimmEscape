using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public AudioClip takeDamageSFX;
    public AudioClip deathSFX;
    private Animator anim;
    void Start() {
        anim = gameObject.GetComponent<Animator>();
    }
    public void TakeDamage(int amount) {
        health -= amount;
        print(health + " " + amount);
        if (health <= 0) {
            // Die!
            // Temp die
            anim.SetBool("Dead", true);
            anim.SetTrigger("Death");
            GetComponent<AudioSource>().PlayOneShot(deathSFX);
            Invoke("Goodbye", 2);
        } else
        {
            GetComponent<AudioSource>().PlayOneShot(takeDamageSFX);
        }
    }

    void Goodbye() {
        Destroy(this.gameObject);
    }
}
