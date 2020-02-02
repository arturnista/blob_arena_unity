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
        SceneManager.LoadScene("Main");
    }
    public void GoToControl()
    {
        SceneManager.LoadScene("Controls");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credit");
    }

    public void GoToSelectP1()
    {
        SceneManager.LoadScene("Chose1");
    }

    public void GoToSelectP2()
    {
        SceneManager.LoadScene("Chose2");
    }

}
