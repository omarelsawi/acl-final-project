using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class link_main : MonoBehaviour
{
    public GameObject swordAttackPoint;
    public float swordRange = 0.35f;
    public LayerMask enemyLayer;
    public bool invincible;
    public float arrowForce;
    public Transform arrowPoint;
    public GameObject arrow;
    public GameObject bow;
    public GameObject sword;
    public GameObject shield;
    public GameObject playerFollowCam;
    public GameObject playerAimCam;
    Animator animator;
    bool shielded;
    bool melee = true;
    int maxHealth = 24;
    public static int currentHealth;
    [HideInInspector]
    public bool dead;
    public CharacterController controller;
    float shieldTimer = 0f;
    float shieldCoolDown = 5f;
    bool autoLowerShield;
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
        if (Input.GetKeyDown("i"))
            invincible = !invincible;
        if (Input.GetKeyDown("h"))
            Heal(10);
        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchWeapons();
        if (autoLowerShield)
            shieldCoolDown -= Time.deltaTime;
        if (shieldCoolDown <= 0.0f)
        {
            autoLowerShield = false;
            shieldCoolDown = 5f;
        }
        if (melee)
        {
            if (Input.GetMouseButton(1))
                ShieldUp();
            else if (Input.GetMouseButtonUp(1))
                ShieldDown();
            else if (Input.GetMouseButtonDown(0))
                SwordAttack();
        } else
        {
            if (Input.GetMouseButton(1))
            {
                DrawBow();
                if (Input.GetMouseButtonDown(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    ReleaseBow();
            }
            else if (Input.GetMouseButtonUp(1))
                UnDrawBow();
        }
    }
    void SwitchWeapons() 
    {
        melee = !melee;
        bow.SetActive(!melee);
        sword.SetActive(melee);
        shield.SetActive(melee);
    }
    void UnDrawBow()
    {
        animator.SetBool("DrawBow", false);
        playerAimCam.SetActive(false);
        playerFollowCam.SetActive(true);

    }
    void ReleaseBow()
    {
        animator.SetTrigger("ReleaseBow");
        playerAimCam.SetActive(false);
        playerFollowCam.SetActive(true);
    }
    void Shoot()
    {
        GameObject arrowRef = Instantiate(arrow, arrowPoint.position,
            transform.rotation);
        arrowRef.transform.RotateAround(arrowRef.transform.position, transform.right, 90f);
        arrowRef.GetComponent<Rigidbody>().AddForce(transform.forward * arrowForce, ForceMode.Impulse);
    }
    void DrawBow()
    {
        animator.SetBool("DrawBow", true);
        playerAimCam.SetActive(true);
        playerFollowCam.SetActive(false);


    }
    void SwordAttack()
    {
        animator.SetTrigger("Sword Attack");
        Collider[] hits = Physics.OverlapSphere(swordAttackPoint.transform.position, swordRange, enemyLayer);

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
        if (!autoLowerShield)
        {
            shielded = true;
            animator.SetBool("Shield", true);
            shieldTimer += Time.deltaTime * Time.timeScale;
            if (shieldTimer >= 10f)
            {
                ShieldDown();
                autoLowerShield = true;
            }
        }
    }

    void ShieldDown()
    {
        shieldTimer = 0f;
        shielded = false;
        animator.SetBool("Shield", false);
    }
    void Die()
    {
        animator.SetTrigger("Dead");
        dead = true;
        SetComponentsEnabled(false);
    }

    void Heal(int x)
    {
        currentHealth = currentHealth + x < maxHealth ? currentHealth + x : maxHealth;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(swordAttackPoint.transform.position, swordRange);
    }

    void SetComponentsEnabled(bool x)
    {
        controller.enabled = x;
        GetComponent<PlayerInput>().enabled = x;
        playerAimCam.SetActive(x);
        playerFollowCam.SetActive(!x);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("portal1"))
        {
            GameObject[] x = GameObject.FindGameObjectsWithTag("Moblin");
            GameObject[] y = GameObject.FindGameObjectsWithTag("Bokoblin");
            if (x.Length == 0 && y.Length == 0)
                SceneManager.LoadScene("Level_2");
        }
        if (collision.gameObject.CompareTag("shrinegoalz"))
        {
            SceneManager.LoadScene("Boss");
        }
    }
}
