using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class extras_link : MonoBehaviour
{
    private int ablities; //1 = remote bombs, 2=Cryonis, 4=stasis
    public Transform player; //intialize as link
    public Transform player_front; //intialize as link
    public GameObject iceCube;
    public GameObject waterplane;
    private bool bombInstantiated;
    public GameObject bomb;
    public GameObject bombprefab;
    public Animator anim;
    private Vector3 bomb_starting;
    static public bool wantsToDetonate;
    public GameObject bombeffect;
    public GameObject bombeffect2;
    GameObject bomb_projectile;
   

    public TMP_Text ability_hud;
    public TMP_Text weapon_hud;



    void Start()
    {
        ablities = 1;
        bombInstantiated = false;
        anim = GetComponent<Animator>();
        wantsToDetonate = false;
       
        

        ability_hud.text = "Bombs";
        weapon_hud.text = "Melee";



    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("1"))
        {
            ablities = 1;
            ability_hud.text = "Bombs";

            GameObject[] obstacles_1 = GameObject.FindGameObjectsWithTag("icecube");
            foreach (GameObject obstacle in obstacles_1)
                GameObject.Destroy(obstacle);

            movingplane.stasis = false;

        }

        if (Input.GetKeyDown("2"))
        {
            ablities = 2;
            ability_hud.text = "Cryonis";

            GameObject[] obstacles_2 = GameObject.FindGameObjectsWithTag("bomb");
            foreach (GameObject obstacle in obstacles_2)
                GameObject.Destroy(obstacle);

            bombInstantiated = false;

            movingplane.stasis = false;

        }

        if (Input.GetKeyDown("4"))
        {
            ablities = 4;
            ability_hud.text = "Stasis";

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

        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (weapon_hud.text.Equals("Melee")) {
                weapon_hud.text = "Ranged";
            }
            else if (weapon_hud.text.Equals("Ranged"))
            {
                weapon_hud.text = "Melee";
            }
        }



        if (player.position.y < -30) {
             SceneManager.LoadScene("GameOver2"); 
        }


    }

    public void runeAbility(int x)
    {
        //remote bombs rune
        if (x == 1)
        {
            if (!bombInstantiated) {
                wantsToDetonate = false;
                bomb.SetActive(true);
                bombInstantiated = true;
                SoundManager.instance.PlaySFX(14);
                anim.Play("Throwing Bomb");
                StartCoroutine(StartTimer());


            }
            else
            {
                //play explosion animation
                SoundManager.instance.PlaySFX(8);
                bombeffect.transform.position = bomb_projectile.transform.position;
                bombeffect2.transform.position = bomb_projectile.transform.position;
                bombeffect.SetActive(true);
                StartCoroutine(StartTimer1());

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


    IEnumerator StartTimer1()
    {
        wantsToDetonate = true;
        bombInstantiated = false;
        yield return new WaitForSeconds(2.80F);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        bombeffect.SetActive(false);
        bombeffect2.SetActive(true);
        StartCoroutine(StartTimer2());

    }


    IEnumerator StartTimer2()
    {
        yield return new WaitForSeconds(2.80F);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        bombeffect2.SetActive(false);

    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.80F);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        bomb.SetActive(false);
        bomb_starting = player_front.position;
        bomb_projectile = Instantiate(bombprefab, bomb_starting, Quaternion.identity);
        bomb_projectile.GetComponent<Rigidbody>().AddForce(player.forward *10, ForceMode.Impulse);


    }
}
