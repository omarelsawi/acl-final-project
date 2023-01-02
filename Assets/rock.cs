using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    private bool damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = false;
    }
    private void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("rock collision >....");

        if (collision.gameObject.CompareTag("Arrow"))
        {
            //damage = true;

            if (this.gameObject.GetComponent<Rigidbody>().velocity.x < 1)
            {
                damage = true;
            }
        }

        else
        {
            SoundManager.instance.PlaySFX(0);
            Destroy(gameObject);
        }

        Debug.Log("damage : " + damage);
        Debug.Log(collision.gameObject.tag);


    }

 
    public bool getDamage( )
    {
        return damage;
    }


}
