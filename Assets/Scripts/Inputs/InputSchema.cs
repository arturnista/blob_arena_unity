using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InputSchema : ScriptableObject
{

    public KeyCode[] Up;
    public KeyCode[] Down;
    public KeyCode[] Right;
    public KeyCode[] Left;

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
