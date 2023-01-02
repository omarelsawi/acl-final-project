using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playlevel1() { Time.timeScale = 1; SceneManager.LoadScene("Level_1_Sawi"); }

    public void playlevel2() { Time.timeScale = 1; SceneManager.LoadScene("Level_2"); }

    public void playlevel3() { Time.timeScale = 1; SceneManager.LoadScene("Boss"); }

    public void playMenu() { Time.timeScale = 1; SceneManager.LoadScene("MainMenu"); }

    public void resumePlay() { link_main.notpaused = false; }
}
