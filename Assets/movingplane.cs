using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingplane : MonoBehaviour
{

    private float delta = 20.5f;  // Amount to move left and right from the start point
    private float speed = 10.0f;
    private Vector3 startPos;
    private Vector3 initPos;
    public static bool stasis;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        initPos = startPos;
        stasis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stasis) { 
        Vector3 v = startPos;
        v.z += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
        if (stasis) {
            transform.position = initPos;
            StartCoroutine(StartTimer());
        }

}

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(10.0f);
        stasis = false;
    }
}
