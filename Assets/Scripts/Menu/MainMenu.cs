using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToControl()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void GoToCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void GoToSelectP1()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToSelectP2()
    {
        SceneManager.LoadScene(2);
    }

}
