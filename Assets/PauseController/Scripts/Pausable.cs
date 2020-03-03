using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausable : MonoBehaviour
{
    
    public void OnPause()
    {
        IPauseListener[] pauseListeners = GetComponents<IPauseListener>();
        foreach (var item in pauseListeners)
        {
            item.OnPause();
        }
    }
    
    public void OnResume()
    {
        IPauseListener[] pauseListeners = GetComponents<IPauseListener>();
        foreach (var item in pauseListeners)
        {
            item.OnResume();
        }
    }

}
