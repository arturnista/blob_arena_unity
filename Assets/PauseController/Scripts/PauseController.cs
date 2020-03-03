using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    
    public static PauseController Instance;

    public GameObject PauseCanvas;
    public AudioSource MusicAudioSource;

    private bool isPaused;
    public bool IsPaused { get => isPaused; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PauseCanvas.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) OnResume();
            else OnPause();
        }
    }

    public void OnPause()
    {
        foreach (var item in GameObject.FindObjectsOfType<Pausable>())
        {
            item.OnPause();
        }
        isPaused = true;
        PauseCanvas.SetActive(true);
        if (MusicAudioSource != null) MusicAudioSource.Pause();
    }

    public void OnResume()
    {
        foreach (var item in GameObject.FindObjectsOfType<Pausable>())
        {
            item.OnResume();
        }
        isPaused = false;
        PauseCanvas.SetActive(false);
        if (MusicAudioSource != null) MusicAudioSource.Play();
    }

}
