using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (extras_link.wantsToDetonate)
        {
            // Check if the bomb collider entered a collider attached to a fragile object or an enemy.
            if (other.gameObject.CompareTag("Fragile"))
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("Moblin") || other.gameObject.CompareTag("Bokoblin"))
            {
                other.gameObject.GetComponent<enemy_agent>().TakeDamage(10);
            }
            //if (other.gameObject.CompareTag("Enemy"))
           // {
                // Apply damage to the enemy and destroy the bomb.
                // other.gameObject.GetComponent<Enemy>().TakeDamage(10);
                // DetonateBomb();
           // }

            Destroy(this.gameObject);
        }
    }
}
