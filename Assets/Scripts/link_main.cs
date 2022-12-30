using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class link_main : MonoBehaviour
{
    public GameObject link_hand;
    public GameObject attackPoint;
    public float attackRange = 0.35f;
    public LayerMask enemyLayer;
    public bool invincible;
    
    Animator animator;
    bool shielded;
    int maxHealth = 24;
    int currentHealth;
    bool dead;
    [HideInInspector]
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && currentHealth <= 0)
            Die();

        else if (Input.GetKeyDown("j"))
            WakeUp(); // for testing purposes

        if (Input.GetKeyDown("i"))
            invincible = !invincible;
        if (Input.GetKeyDown("h"))
            Heal(10);
        if (Input.GetMouseButton(1))
            ShieldUp();
        else if (Input.GetMouseButtonUp(1))
            ShieldDown();
        else if (Input.GetMouseButtonDown(0))
            SwordAttack();
    }
    void SwordAttack()
    {
        animator.SetTrigger("Sword Attack");
        Collider[] hits = Physics.OverlapSphere(attackPoint.transform.position, attackRange, enemyLayer);

        foreach(Collider enemy in hits)
        {
            enemy.GetComponent<enemy_agent>().TakeDamage(10);
        }
    }
    public void TakeDamage(int x)
    {
        if (!shielded & !invincible)
        {
            currentHealth = currentHealth-x > 0? currentHealth - x : 0;
        }
        Debug.Log(gameObject.name + "'s Health: " + currentHealth);
    }

    void ShieldUp()
    {
        shielded = true;
        animator.SetBool("Shield", true);
    }

    void ShieldDown()
    {
        shielded = false;
        animator.SetBool("Shield", false);
    }
    void Die()
    {
        animator.SetTrigger("Die");
        dead = true;
    }
    void WakeUp()
    {
        currentHealth = maxHealth;
        animator.SetTrigger("WakeUp");
        dead = false;
    }

    void Heal(int x)
    {
        currentHealth = currentHealth + x < maxHealth ? currentHealth + x : maxHealth;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
