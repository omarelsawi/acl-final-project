using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemy_agent : MonoBehaviour
{
    public Transform target;
    public GameObject link;
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
    float maxHealth = 20f;
    float currentHealth;
    bool dead;
    bool stop;
    bool attacked = false;

    [SerializeField] private Image healthbarSprite;
    private void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (gameObject.CompareTag("Moblin")) maxHealth = 30f;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && currentHealth <= 0f)
            Die();
        if (link.GetComponent<link_main>().dead)
            StopChasing();
        if (!dead && !stop)
        {
            CheckDistance();
            if (chase)
            {
                Chase();
            }

            else if (attack)
            {
                Attack();
            }

        }
    }
    void UpdateHealthBar()
    {
        healthbarSprite.fillAmount = currentHealth / maxHealth;
    }

    void StopChasing()
    {
        navMeshAgent.SetDestination(transform.position);
        chase = false;
        alert = false;
        attack = false;
        stop = true;
    }
    public void TakeDamage(int x)
    {
        campArea.GetComponent<camp_area>().alert = true;
        currentHealth = currentHealth - x > 0f ? currentHealth - x : 0f;
        UpdateHealthBar();
        animator.SetTrigger("GetHit");
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

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!attacked && (stateInfo.IsName("Vertical Attack") || stateInfo.IsName("Horizontal Attack"))
            && stateInfo.normalizedTime <= 0.6f && stateInfo.normalizedTime >= 0.4f
               && Vector3.Distance(link.transform.position, animator.transform.position) < attackArea)
        {
            attacked = true;

            if (stateInfo.IsName("Vertical Attack"))
                link.GetComponent<link_main>().TakeDamage((animator.tag == "Moblin") ? 4 : 3);
            else
                link.GetComponent<link_main>().TakeDamage((animator.tag == "Moblin") ? 2 : 1);

        }
        if (stateInfo.normalizedTime > 0.7f)
            attacked = false;

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
