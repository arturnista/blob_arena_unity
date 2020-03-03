using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InputSchema : ScriptableObject
{

    public string VerticalAxis;
    public string HorizontalAxis;
    public KeyCode[] Jump;
    public KeyCode[] Attack;
    public KeyCode[] Dash;
    public KeyCode[] Pause;

    public bool GetKeyDown(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }

    public bool GetKeyUp(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyUp(key)) return true;
        }
        return false;
    }

    public bool GetKey(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKey(key)) return true;
        }
        return false;
    }

}
