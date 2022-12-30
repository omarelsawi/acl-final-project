using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_agent : MonoBehaviour
{
    public Transform target;
    public GameObject campArea;
    NavMeshAgent navMeshAgent;
    private bool chase;
    private bool attack;
    public float chaseArea;
    public float attackArea;
    public float attackFrequency;
    Animator animator;
    bool gotAttacked;
    bool alert;
    int maxHealth = 20;
    int currentHealth;
    float attackTimer = 0f;
    bool dead;
    private void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (gameObject.CompareTag("Moblin")) maxHealth = 30;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer = Time.deltaTime * Time.timeScale;
        if (!dead && currentHealth <= 0)
            Die();

        if (!dead)
        {
            CheckDistance();
            if (chase)
            {
                Chase();
            }

            else if (attack && attackTimer >= attackFrequency)
            {
                Attack();
            }

        }
    }

    public void TakeDamage(int x)
    {
        currentHealth = currentHealth - x > 0 ? currentHealth - x : 0;
        Debug.Log(gameObject.name + "'s Health: " + currentHealth);
    }
    void Die()
    {
        animator.SetTrigger("Die");
        dead = true;
        SetComponentsEnabled(false);
        StartCoroutine(Destroy());

    }
    void Attack()
    {
        int attackType = Random.Range(0, 3);
        // vertical attack supposedly has a 1 in 3 chance
        transform.LookAt(target);
        if (attackType == 2)
        {
            animator.SetTrigger("Attack Vertically");
        }
        else
        {
            animator.SetTrigger("Attack Horizontally");
        }
        attackTimer = 0f;
    }
    void Chase()
    {
        animator.SetTrigger("Chase");
        navMeshAgent.SetDestination(target.position);

    }
    void CheckDistance()
    {
        float distanceToPlayer = Vector3.Distance(target.position, this.transform.position);
        alert = campArea.GetComponent<camp_area>().alert;

        if (alert && !(distanceToPlayer < attackArea))
        {
            chase = true;
            attack = false;
        }

        else if (distanceToPlayer < attackArea)
        {
            chase = false;
            attack = true;
        }

    }

    void SetComponentsEnabled(bool x)
    {
        navMeshAgent.enabled = x;
        this.GetComponent<BoxCollider>().enabled = x;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

}
