using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputSystem : MonoBehaviour
{

    public InputSchema p1;
    private bool alreadyTested;

    // Start is called before the first frame update

    private void Start()
    {
        alreadyTested = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!alreadyTested)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    //your code here
                    Debug.Log("KeyCode down: " + vKey);
                    p1.Attack[0] = vKey;
                    alreadyTested = true;
                }
            }
        }
       
       
    }
}
