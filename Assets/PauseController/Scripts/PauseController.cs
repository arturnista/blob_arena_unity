using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseController : MonoBehaviour
{
    
    public static PauseController Instance;

    public GameObject PauseCanvas;
    public AudioSource MusicAudioSource;

    private InputSchema schema;
    private StandaloneInputModule standaloneInputModule;

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
        if (schema != null)
        {
            if (isPaused && schema.GetKeyUp(schema.Pause))
            {
                Resume();
            }
        }
    }

    public void Pause(InputSchema schema)
    {
        if (GameController.main.GameEnded) return;

        this.schema = schema;

        standaloneInputModule = PauseCanvas.GetComponentInChildren<StandaloneInputModule>();
        standaloneInputModule.verticalAxis = schema.VerticalAxis;
        standaloneInputModule.horizontalAxis = schema.HorizontalAxis;

        foreach (var item in GameObject.FindObjectsOfType<Pausable>())
        {
            item.OnPause();
        }
        isPaused = true;
        PauseCanvas.SetActive(true);
        if (MusicAudioSource != null) MusicAudioSource.Pause();
    }

    public void Resume()
    {
        if (GameController.main.GameEnded) return;
        foreach (var item in GameObject.FindObjectsOfType<Pausable>())
        {
            item.OnResume();
        }
        isPaused = false;
        PauseCanvas.SetActive(false);
        if (MusicAudioSource != null) MusicAudioSource.Play();
    }

}
