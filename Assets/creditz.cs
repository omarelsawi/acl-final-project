using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditz : MonoBehaviour
    
{
    public GameObject creditzz;
    public Transform cc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cc.transform.position.y>1250)
            Application.Quit();

        creditzz.transform.position += Vector3.up * 15.5f * Time.deltaTime;
    }
}
