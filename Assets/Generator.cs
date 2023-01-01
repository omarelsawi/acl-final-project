using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public bool move;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject rock = GameObject.FindGameObjectWithTag("rock");
        if(rock != null && move)
        {
            attack(rock);
        }
    }

    private void attack(GameObject rock)
    {
        Vector3 moveDirection = (rock.transform.position - this.transform.position).normalized * 50;
        GetComponent<Rigidbody>().velocity = new Vector3(moveDirection.x, moveDirection.y + 2.1f, moveDirection.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("rock"))
        {
            Destroy(gameObject);

        }
    }


}
