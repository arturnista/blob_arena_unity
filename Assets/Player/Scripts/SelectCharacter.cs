using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SelectCharacter : ScriptableObject
{
    public Sprite[] Personas;
    public int[] Player = new int[2];

    public void SelectP1(int i)
    {
        Player[0] = i;
    }
    public void SelectP2(int i)
    {
        Player[1] = i;
    }
}
