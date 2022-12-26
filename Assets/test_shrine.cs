using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_shrine : MonoBehaviour
{

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey("left") || Input.GetKey("a")))
            rb.velocity = new Vector3(-5f, this.rb.velocity.y, 0);
        //rb.AddForce(new Vector3(-5f, 0, 0));
        if ((Input.GetKey("right") || Input.GetKey("d")) )
            rb.velocity = new Vector3(5f, this.rb.velocity.y, 0);
        //rb.AddForce(new Vector3(5f, 0, 0));
        if (Input.GetKeyDown("space") )
        {
            rb.AddForce(Vector3.up * 7, ForceMode.Impulse);
        }
    }
}
