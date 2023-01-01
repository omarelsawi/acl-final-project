using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public int LevelChosen = 1;
    public Slider volume;
    public Slider Audio;
    public void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void PlayLevel1()
    {
        LevelChosen = 1;
    }
    public void PlayLevel2()
    {
        LevelChosen = 2;

    }
    public void PlayBoss()
    {
        LevelChosen = 3;
    }
    public void StartGame()
    {
        //switch to the chosen level's scene
        switch (LevelChosen)
        {
            case 1:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                break;
            case 3:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
                break;
        }

    }
}
