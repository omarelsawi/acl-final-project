
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
    private bool timeFinish;
    private bool notFirstTime;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        master = transform.GetChild(0).gameObject;
        shield = master.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (canAttack)
        {
            // Make the boss look toward the player
            transform.LookAt(player);
        }
       
        if (rock && towardBoss)
        {
            // Check if the master is currently flying
            if (master.transform.position.y < maxHeight)
            {
                // Enable the shield
                shield.SetActive(true);
            }
            else
            {
                // Disable the shield
                shield.SetActive(false);
            }
        }

        Debug.Log("health"+health);
        if (health <= 0)
        {
            Dye();
        }

        if (master && master.transform.position.y < maxHeight && !onGround && health > 0)
        {
            flying();
        }

        if (master &&  master.transform.position.y > 1 && onGround && health > 0 )
        {
            falling();
        }

        if (master && master.transform.position.y < 1 && health > 0)
        {
            Invoke(nameof(onFloor), 0.5f);
        }

        if (master &&  health <= 100 && !alreadyMoved && master.transform.position.y > 4.5 && canAttack && health > 0)
        {
            agent.transform.position = RandomNavmeshLocation(100f);
            alreadyMoved = true;
            Invoke(nameof(ResetMove), 20);
        }

        if (master && master.transform.position.y > 3 && health > 0)
        {
            master.GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
        }
        if(notFirstTime == false)
            notFirstTime = true;



        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!alreadyAttacked && canAttack && canHit() && health > 0)
        {
            StartCoroutine(AttackPlayer());
            alreadyAttacked = true;
            int wait = notFirstTime ? 20 : 30;
            Invoke(nameof(ResetAttack), wait);
        }

        Debug.Log("rock123 " + rock);
        if (rock)
        {
            if (rock.GetComponent<rock>().getDamage())
            {
                Debug.Log("rock damageddddd ");

                towardBoss = true;
                damageRock = true;
            }
        }

        if (canAttack)
        {
            currAttackDelay -= Time.deltaTime;
            Vector3 aim = player.position - transform.position;
            aim.y = 0;
            transform.forward = aim;
        }
        else
        {
            transform.forward = new Vector3(0, 0, 0);

        }
    }



    private void onFloor()
    {
        transform.LookAt(null);

        onGround = true;
        animator.SetBool("hurtOnFlorr", true);
        Debug.Log("onFloor");
        damageRock = false;
        int wait = notFirstTime ? 6 : 10;

        Invoke(nameof(EndOnFloor), wait);
    }

    private void EndOnFloor()
    {
        //transform.LookAt(player);

        onGround = false;
        canAttack = true;
        shield.SetActive(true);
        towardBoss = false;
        animator.SetBool("hurtOnFlorr", false);
        animator.SetBool("fly", true);
        animator.SetBool("fall", false);
    }

    private void fall()
    {
        Debug.Log("ininnnicnsjcnajkcas");
        animator.SetBool("fly", false);
        animator.SetBool("fall", true);
        canAttack = false;
        onGround = true;
        while(master.transform.position.y > 0) //&& onGround
            falling();
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
        rock = null;
        damageRock = false;
        onGround = false;
        staticRock = true;
        timeFinish = false;

        // Make sure enemy doesn't move
        // agent.SetDestination(transform.position);

        transform.LookAt(player);

        // Attack code here
        Vector3 position = new Vector3(master.transform.position.x, master.transform.position.y + 10, master.transform.position.z);
        rock = Instantiate(projectile, position + transform.forward, transform.rotation);
        Rigidbody rb = rock.GetComponent<Rigidbody>();
        rock.transform.SetParent(master.transform);
        damageRock = false;
        int wait = notFirstTime ? 5 : 15;

        Invoke(nameof(timeOut), wait);

        while (!towardBoss && !timeFinish)
        {
            yield return null;
        }
        if (rock)
                {
            rock.transform.SetParent(null);
            staticRock = false;
            if (towardBoss)
            {

                shield.SetActive(false);
                Debug.Log("shield is active " + shield.activeInHierarchy);
                rb.velocity = new Vector3(0, -1, 0) * 10;
            }
            else
            {
                animator.SetTrigger("throwRock");

                Vector3 moveDirection = (player.transform.position - rock.transform.position).normalized * 30;
                rb.velocity = new Vector3(moveDirection.x, moveDirection.y + 1.5f, moveDirection.z);
            }
        }
    }

    private void timeOut()
    {
        timeFinish = true;
    }

    private void falling()
    {
        master.transform.position -= new Vector3(0, 0.01f, 0);
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
    
 
  
    public void disapear() {
        //Time.timeScale = 0;
        Destroy(master);
    }



    private void Dye()
    {
        float velocity = 5f * Time.deltaTime;

        // Check if the object is already touching the ground

        if (master)
        {
            if (master.transform.position.y > 1.5)
            {
                // If not, move the object downwar  d with the fall velocity
                master.transform.position += Vector3.down * velocity;
                if (master.transform.position.y < 4)
                {
                    ///Sound When an enemy dies
                    animator.SetTrigger("die");
                }

            }
            if (master.transform.position.y < 1.5)
            {
                Vector3 position = master.transform.position;
                master.transform.position = new Vector3(position.x, -1, position.z);

            }
        }
        Invoke(nameof(disapear), 5);
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

        //Sound when hit

        //Debug.Log("in CollisionDetected");
        //Debug.Log("CompareTag" + collision.gameObject.CompareTag("rock"));
        //Debug.Log("damageRock" + damageRock);
        Debug.Log("damageRock in collison " + damageRock);
        if (collision.gameObject.CompareTag("rock") && damageRock)
        {
            canAttack = false;
            Debug.Log("rock collision");
            decreaseHealth(40);
            if(health > 0)
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
