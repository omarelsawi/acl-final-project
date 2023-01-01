using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbingController : MonoBehaviour
{
    public static bool ladderClose = true;
    public static bool columnClose = true;


    // Start is called before the first frame update
    void Start()
    {
        ladderClose = false;
        columnClose = false;
    }

    // Update is called once per frame
    void Update()  {

 }

    

        private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("LadderClose"))
        {
            ladderClose = true;
        }
        if (collision.gameObject.CompareTag("ColumnClose"))
        {
            columnClose = true;
            Debug.Log("col close enter");

        }
    }
   

    private void OnTriggerExit(Collider collision)
{
        if (collision.gameObject.CompareTag("LadderClose"))
        {
            ladderClose = false;
        }
        if (collision.gameObject.CompareTag("ColumnClose"))
        {
            columnClose = false;
            Debug.Log("col close exit");
            StarterAssets.ThirdPersonController.col_climb_x = 46f;
            StarterAssets.ThirdPersonController.col_climb_y = 5.5f;
            StarterAssets.ThirdPersonController.col_climb_z = 79.5f;
            Debug.Log("StarterAssets.ThirdPersonController.col_climb_z: " + StarterAssets.ThirdPersonController.col_climb_z);
        }
    }
}
