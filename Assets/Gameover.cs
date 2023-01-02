using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playlevel1() { SceneManager.LoadScene("Level_1_Sawi"); }

    public void playlevel2() { SceneManager.LoadScene("Level_2"); }

    public void playlevel3() { SceneManager.LoadScene("Boss"); }

    public void playMenu() { SceneManager.LoadScene("MainMenu"); }


}
