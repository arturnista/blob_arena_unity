using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    private static MenuMusic main;
    private AudioSource audioSource;

    void Start()
    {
        if (main != null) 
        {
            Destroy(this.gameObject);
            return;
        }
        main = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += HandleSceneLoad;
    }

    void HandleSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game") audioSource.Pause();
        else if (!audioSource.isPlaying) audioSource.Play();
    }

}
