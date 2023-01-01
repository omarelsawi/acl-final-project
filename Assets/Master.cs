using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Master : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        transform.parent.GetComponent<Boss>().CollisionDetected(collision);
    }

}
