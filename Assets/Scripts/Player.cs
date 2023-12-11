using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static float moveSpeed = 2f;
    public float moveDrag;
    public float jumpForce;
    public float gravityScale = 2;
    public float rotationSpeed = 200f;
    public int maxHealth = 20;
    public int curHealth = 20;
    public static int damage = 5;
    public float attackRangeRadius;
    public float attackRangeHeight;
    public Vector3 jumpBoxSize;
    public float jumpBoxHeight;

    public Animator anim;
    public Transform playerTransform;
    public Transform orientation;
    public Slider healthBar;
    public GameObject spawn;

    public AudioClip attackSFX;
    public AudioClip damageSFX;
    public AudioClip deathSFX;

    private Rigidbody playerBody;
    private Collider playerCollider;
    Vector3 moveDirection;
    float horizontalInput;
    float verticalInput;
    private bool grounded;
    private bool groundedLastTick;
    private bool alreadyDead = false; // Used to prevent death sound effect/animation being triggered repeatedly in the period between health reaching 0 and respawning


    void Start()
    {
        playerCollider = GetComponent<Collider>();
        playerBody = GetComponent<Rigidbody>();
        healthBar.value = maxHealth;
        spawn = GameObject.Find("PlayerSpawn");

        damage = PlayerStats.damage;
        maxHealth = PlayerStats.health;
        moveSpeed = PlayerStats.speed;

    }

    void Update()
    {
        groundedLastTick = grounded;

        // ground check 
        grounded = Physics.BoxCast(transform.position, jumpBoxSize, -transform.up, transform.rotation, jumpBoxHeight);
        // print("grounded: " + grounded);
        // Only set trigger when you switch from being in the air to being grounded or vice versa
        if (grounded == false && groundedLastTick == true)
        {
            anim.SetTrigger("Jump");
        }
        else if (grounded == true && groundedLastTick == false)
        {
            anim.SetTrigger("Grounded");
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // jump
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            playerBody.AddForce(new Vector3(0, jumpForce, 0));
        }

        // prevent player from continuously accelerating
        SpeedControl();

        // handle drag
        if (grounded)
        {
            playerBody.drag = moveDrag;
        }
        else
        {
            playerBody.drag = 0;
        }

        // attack
        if (Input.GetMouseButtonDown(0))
        {
            MeleeAttack();
        }

        // check for death
        if (curHealth <= 0 && !alreadyDead)
        {
            anim.SetTrigger("Death");
            GetComponent<AudioSource>().PlayOneShot(deathSFX);
            alreadyDead = true;
            Invoke("Respawn", 2);
        }

        // set movement animations
        anim.SetFloat("Speed", Input.GetAxis("Vertical"));
        anim.SetFloat("Direction", Input.GetAxis("Horizontal"));
    }
    void FixedUpdate()
    {
        playerBody.AddForce(Physics.gravity * (gravityScale - 1) * playerBody.mass);
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.Normalize(); // Normalize the direction to prevent speeding up when moving diagonally

        // Check if the player is on the ground
        if (grounded)
        {
            // Apply forces only when grounded
            Vector3 targetVelocity = new Vector3(moveDirection.x * moveSpeed, playerBody.velocity.y, moveDirection.z * moveSpeed);
            Vector3 velocityChange = targetVelocity - playerBody.velocity;

            playerBody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Trigger movement animation
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void MeleeAttack()
    {
        GameObject enemy = EnemyInRange();

        if (enemy != null)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        anim.SetTrigger("Attack");
        GetComponent<AudioSource>().PlayOneShot(attackSFX);
    }

    public void TakeDamage(int amount)
    {
        GetComponent<AudioSource>().PlayOneShot(damageSFX);
        curHealth -= amount;
        healthBar.value = curHealth;
    }

    void Respawn()
    {
        playerTransform.position = spawn.transform.position;
        curHealth = maxHealth;
        healthBar.value = curHealth;
        alreadyDead = false;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerBody.velocity.x, 0f, playerBody.velocity.z);
        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            playerBody.velocity = new Vector3(limitedVel.x, playerBody.velocity.y, limitedVel.z);
        }
    }

    // If there is an enemy within range return the associated GameObject, otherwise return null
    private GameObject EnemyInRange()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRangeRadius, -transform.up, attackRangeHeight);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    // Draw box cast used for jumping
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * jumpBoxHeight, jumpBoxSize);
    }

    // upgrade values need testing to see what feels right
    public void UpgradeHealth()
    {
        maxHealth += 5;
        curHealth = maxHealth;
        PlayerStats.health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void UpgradeSpeed()
    {
        moveSpeed += 0.5f;
        curHealth = maxHealth;
        PlayerStats.speed = moveSpeed;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void UpgradeAttack()
    {
        damage += 2;
        curHealth = maxHealth;
        PlayerStats.damage = damage;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
}