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
    Animator animator;
    bool gotAttacked;
    bool alert;
    private void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
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
}
