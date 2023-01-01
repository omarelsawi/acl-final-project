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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("rock collision >....");

        if (collision.gameObject.CompareTag("arrow"))
        {
            damage = true;
            Debug.Log("rock hit !!");
        }
      
        else
            Destroy(gameObject);

        Debug.Log("damage : " + damage);
        Debug.Log(collision.gameObject.tag);


    }

 
    public bool getDamage( )
    {
        return damage;
    }


}
