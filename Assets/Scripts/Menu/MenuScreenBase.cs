using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuScreenBase : MonoBehaviour
{
    
    public delegate void NextScreenHandle();
    public NextScreenHandle OnNextScreen;

}
