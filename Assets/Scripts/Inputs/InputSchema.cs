using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InputSchema : ScriptableObject
{

    public string HorizontalAxis;
    public KeyCode[] Jump;

    public bool GetKeyDown(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key)) return true;
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
