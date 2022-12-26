using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camp_area : MonoBehaviour
{
    public Transform player;
    public float alertArea;
    public bool alert;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, this.transform.position);
        if (distanceToPlayer < alertArea)
        {
            alert = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Add objects by which link can attack here
        if (other.gameObject.CompareTag("Arrow"))
        {
            alert = true;
        }
    }
}
