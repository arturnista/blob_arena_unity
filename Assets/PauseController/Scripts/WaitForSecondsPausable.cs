using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsPausable : CustomYieldInstruction
{
    private float seconds;
    private float timePassed;

    public override bool keepWaiting
    {
        get
        {
            if(PauseController.Instance.IsPaused) return true;
            
            timePassed += Time.deltaTime;
            return timePassed < seconds;
        }
    }

    public WaitForSecondsPausable(float seconds)
    {
        this.timePassed = 0f;
        this.seconds = seconds;
    }
}
