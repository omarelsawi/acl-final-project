using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public int LevelChosen = 1;


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
                
                break;
            case 2:
                
                break;
            default:
                
                break;
        }

    }
}
