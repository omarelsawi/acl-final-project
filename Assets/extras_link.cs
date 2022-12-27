using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extras_link : MonoBehaviour
{
    private int ablities; //1 = remote bombs, 2=Cryonis, 4=stasis
    public Transform player; //intialize as link
    public Transform player_hand; //intialize as hand position
    public GameObject iceCube;
    public GameObject waterplane;
    private bool bombInstantiated;
    public GameObject bomb;
    void Start()
    {
        ablities = 1;
        bombInstantiated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ablities = 1;

            GameObject[] obstacles_1 = GameObject.FindGameObjectsWithTag("icecube");
            foreach (GameObject obstacle in obstacles_1)
                GameObject.Destroy(obstacle);

            movingplane.stasis = false;

        }

        if (Input.GetKeyDown("2"))
        {
            ablities = 2;

            GameObject[] obstacles_2 = GameObject.FindGameObjectsWithTag("bomb");
            foreach (GameObject obstacle in obstacles_2)
                GameObject.Destroy(obstacle);

            bombInstantiated = false;

            movingplane.stasis = false;

        }

        if (Input.GetKeyDown("4"))
        {
            ablities = 4;

            GameObject[] obstacles_1 = GameObject.FindGameObjectsWithTag("icecube");
            foreach (GameObject obstacle in obstacles_1)
                GameObject.Destroy(obstacle);

            GameObject[] obstacles_2 = GameObject.FindGameObjectsWithTag("bomb");
            foreach (GameObject obstacle in obstacles_2)
                GameObject.Destroy(obstacle);

            bombInstantiated = false;

        }

        if (Input.GetKeyDown("q"))
        {
            runeAbility(ablities);

        }

        if (bombInstantiated) {
            bomb.transform.position = player_hand.transform.position;
        }




        if (player.transform.position.y < -50) { 
        //end game
        //load main menu
        }


    }

    public void runeAbility(int x)
    {
        //remote bombs rune
        if (x == 1)
        {
            if (!bombInstantiated) {
                bomb.SetActive(true);
                //throw bomb


                bombInstantiated = true;
            }
            else
            {
                //detonate instnatiated bomb

            }
        }

        //ice rune
        else if (x == 2)
        {

            GameObject[] obstacles_1 = GameObject.FindGameObjectsWithTag("icecube");
            foreach (GameObject obstacle in obstacles_1)
                GameObject.Destroy(obstacle);

            // Create a ray from the camera's position to the mouse position.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if the ray hits the collider attached to the plane.
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == waterplane)
            {
                    // Create the prefab at the position where the ray hit the plane.
                    Instantiate(iceCube, hit.point, Quaternion.identity);
                }
            }

        //time stop rune
        else if (x == 4)
        {
            movingplane.stasis = true;
        }
    }
}
