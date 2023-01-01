
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;
using System;

public class Boss : MonoBehaviour
{
    // NavMeshAgent and GameObject references
    public bool towardBoss = false;
    public NavMeshAgent agent;
    private GameObject master;
    private GameObject shield;

    // Transform and LayerMask variables
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Health variables and events
    private int maxHealth;
    public float health;
    public event Action<float> CurrentHealth = delegate { };
    public event Action<float> OnHealthPctChanged = delegate { };

    // Patroling variables
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    // Attacking variables
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public bool waiting;
    public GameObject projectile;
    public float MaxThrowForce = 25;
    public float ForceRatio = 0;
    private CharacterController PlayerCharacterController;
    private bool canAttack = true;

    // State variables
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float SpherecastRadius = 0.5f;

    // Other variables
    private static int maxHeight = 5;
    private bool onGround;
    public bool damageRock;
    public GameObject arrowPrefab;
    public float bulletSpeed = 10000f;
    public Transform attackRangeSphere;
    public float attackDelay = 2;
    public Transform target;
    public Transform attackPos;
    public bool showAttackRange = true;
    private float currAttackDelay = 0;
    public bool staticRock = true;
    public Animator animator;
    public bool thisIsTheBegining = true;
    private bool alreadyMoved;
    private GameObject rock;
    public float gravity = 9.81f;
    private float velocity = 0.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        master = transform.GetChild(0).gameObject;
        shield = master.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (health <= 0 && master.transform.position.y < 1)
        {
            Dye();
        }

        if (master.transform.position.y < maxHeight && !onGround)
        {
            flying();
        }

        if (master.transform.position.y > 0 && onGround)
        {
            falling();
        }

        if (master.transform.position.y < 1)
        {
            StartCoroutine(onFloor());
        }

        if (health <= 100 && !alreadyMoved && master.transform.position.y > 4.5 && canAttack)
        {
            agent.transform.position = RandomNavmeshLocation(100f);
            alreadyMoved = true;
            Invoke(nameof(ResetMove), 20);
        }

        if (master.transform.position.y > 3)
        {
            master.GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
        }

        //if (damageRock)
        //{
        //    // Wait for a short time before making the boss fall to the ground
        //    StartCoroutine(WaitForSecondsCoroutine());

        //    // Set onGround to true and call the falling function
        //    onGround = true;
        //    falling();
        //}

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!alreadyAttacked && canAttack && canHit())
        {
            StartCoroutine(AttackPlayer());
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 15);
        }

        Debug.Log("rock in update " + rock);

        if (rock)
        {
            if (rock.GetComponent<rock>().getDamage())
            {
                towardBoss = true;
                damageRock = true;
            }
        }

        currAttackDelay -= Time.deltaTime;
        Vector3 aim = player.position - transform.position;
        aim.y = 0;
        transform.forward = aim;
    }



    private IEnumerator onFloor()
    {
        animator.SetBool("hurtOnFlorr", true);
        //Debug.Log(onGround);
        yield return new WaitForSeconds(10);
        onGround = false;
        canAttack = true;
        shield.SetActive(true);
        animator.SetBool("hurtOnFlorr", false);
        animator.SetBool("fly", true);
    }

    IEnumerator WaitForSecondsCoroutine()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // The code below this line will be executed after 0.5 seconds
        //Debug.Log("Waited for 0.5 seconds");
    }

    IEnumerator Wait(int seconds)
    {
        //Debug.Log("inini");
        yield return new WaitForSeconds(seconds); // wait for 5 sec

    }
    private void fall()
    {
        //Debug.Log("ininnnicnsjcnajkcas");
        animator.SetBool("fly", false);
        animator.SetBool("fall", true);
        canAttack = false;
        onGround = true;
        towardBoss = false;
    }

    private bool canHit()
    {

        if (master.transform.position.y > maxHeight - 0.5)
        {
            return true;
        }
        return false;
    }


    private IEnumerator AttackPlayer()
    {
        staticRock = true;

        // Make sure enemy doesn't move
        // agent.SetDestination(transform.position);

        transform.LookAt(player);

        // Attack code here
        Vector3 position = new Vector3(master.transform.position.x, master.transform.position.y + 10, master.transform.position.z);
        rock = Instantiate(projectile, position + transform.forward, transform.rotation);
        Rigidbody rb = rock.GetComponent<Rigidbody>();
        rock.transform.SetParent(master.transform);
        damageRock = false;
        yield return new WaitForSecondsRealtime(2);
        if (rock)
        {
            rock.transform.SetParent(null);
            staticRock = false;
            if (towardBoss)
            {
                shield.SetActive(false);
                rb.velocity = new Vector3(0, -1, 0) * 10;
            }
            else
            {
                Vector3 moveDirection = (player.transform.position - rock.transform.position).normalized * 30;
                rb.velocity = new Vector3(moveDirection.x, moveDirection.y + 1.5f, moveDirection.z);
            }
        }
    }

    private void falling()
    {
        master.transform.position -= new Vector3(0, 0.1f, 0);
    }
    private void flying()
    {
        animator.SetBool("fly", true);
        animator.SetBool("fall", false);
        animator.SetBool("hurtOnFlorr", false);

        master.transform.position += new Vector3(0, 0.01f, 0);

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void ResetMove()
    {
        alreadyMoved = false;
    }
    private void ResetWait()
    {
        waiting = false;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void Dye()
    {
        // Update the player's position based on their velocity
        transform.position += Vector3.down * velocity * Time.deltaTime;

        // Apply a downward force to the player
        velocity += gravity * Time.deltaTime;
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }


    public void decreaseHealth(int damage)
    {
        if(health - damage >= 0)
            health -= damage;
        else
            health = 0;


        CurrentHealth(health);
    }
    public void CollisionDetected(Collision collision)
    {
        //Debug.Log("in CollisionDetected");
        //Debug.Log("CompareTag" + collision.gameObject.CompareTag("rock"));
        //Debug.Log("damageRock" + damageRock);

        if (collision.gameObject.CompareTag("rock") && damageRock)
        {
            Debug.Log("rock collision");
            decreaseHealth(40);
            fall();
        }
        else if (collision.gameObject.CompareTag("sword") && !shield.activeInHierarchy)
        {
            Debug.Log("sword collision");
            decreaseHealth(10);
        }
        else if (collision.gameObject.CompareTag("bomb") && !shield.activeInHierarchy)
        {
            Debug.Log("bomb collision");
            decreaseHealth(10);
        }
        else if (collision.gameObject.CompareTag("arrow") && !shield.activeInHierarchy)
        {
            Debug.Log("arrow collision");
            decreaseHealth(5);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            // The player has touched the ground
            Debug.Log("Player has touched the ground");

            // Stop the player's movement
            velocity = 0.0f;
        }
    }


}
