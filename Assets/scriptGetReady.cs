using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptGetReady : MonoBehaviour
{
    public PlayerAttack pa;
    // Start is called before the first frame update

    // Update is called once per frame
   public void SetReady()
   {
       pa.SetReady();
   }
}