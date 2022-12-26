using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_shrine_test : MonoBehaviour
{
    public Transform playerr;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerr.transform.position + new Vector3(0, 5, -6);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerr.transform.position + new Vector3(0, 5, -6);

    }
}
