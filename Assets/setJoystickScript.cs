using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setJoystickScript : MonoBehaviour
{

    string [] joysticks;
    public InputSchema[] schema;
    // Start is called before the first frame update
    void Start()
    {
        joysticks= Input.GetJoystickNames();

        for(int i=0; i<schema.Length; i++)
        {
            if(schema.Length <= joysticks.Length)
            {
                // PS4
                if(joysticks[i] == "Wireless Controller")
                {
                    if(i == 0)
                    {
                        schema[i].Jump[1] = KeyCode.Joystick1Button1;
                        schema[i].Attack[1] = KeyCode.Joystick1Button0;
                        schema[i].Dash[1] = KeyCode.Joystick1Button7;
                        schema[i].Pause[1] = KeyCode.Joystick1Button9;
                    }
                    else if(i == 1)
                    {
                        schema[i].Jump[1] = KeyCode.Joystick2Button1;
                        schema[i].Attack[1] = KeyCode.Joystick2Button0;
                        schema[i].Dash[1] = KeyCode.Joystick2Button7;
                        schema[i].Pause[1] = KeyCode.Joystick2Button9;
                    }
                }
                // XBox
                else
                {
                    if(i == 0)
                    {
                        schema[i].Jump[1] = KeyCode.Joystick1Button0;
                        schema[i].Attack[1] = KeyCode.Joystick1Button2;
                        schema[i].Dash[1] = KeyCode.Joystick1Button5;
                        schema[i].Pause[1] = KeyCode.Joystick1Button7;
                    }
                    else if(i == 1)
                    {
                        schema[i].Jump[1] = KeyCode.Joystick2Button0;
                        schema[i].Attack[1] = KeyCode.Joystick2Button2;
                        schema[i].Dash[1] = KeyCode.Joystick2Button5;
                        schema[i].Pause[1] = KeyCode.Joystick2Button7;
                    }
                }
            }
            else if(joysticks.Length == 1)
            {
                // PS4
                if(joysticks[0] == "Wireless Controller")
                {
                    schema[0].Jump[1] = KeyCode.Joystick1Button1;
                    schema[0].Attack[1] = KeyCode.Joystick1Button0;
                    schema[0].Dash[1] = KeyCode.Joystick1Button7;
                    schema[0].Pause[1] = KeyCode.Joystick1Button9;
                }
                // XBox
                else
                {
                    schema[0].Jump[1] = KeyCode.Joystick1Button0;
                    schema[0].Attack[1] = KeyCode.Joystick1Button2;
                    schema[0].Dash[1] = KeyCode.Joystick1Button5;
                    schema[0].Pause[1] = KeyCode.Joystick2Button7;
                }
            }
            
        }   
    }

 }   // Update is called once per frame
    