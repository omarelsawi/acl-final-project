using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moblin") || collision.gameObject.CompareTag("Bokoblin"))
        {
            enemy_agent script = collision.gameObject.GetComponent<enemy_agent>();
            script.TakeDamage(5);
            script.campArea.GetComponent<camp_area>().alert = true;
        }

        GetComponent<BoxCollider>().enabled = false;
        Destroy(transform.GetComponent<Rigidbody>());
        Destroy(gameObject, 3f);
    }
}
