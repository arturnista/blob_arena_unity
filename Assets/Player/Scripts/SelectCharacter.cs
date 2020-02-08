using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SelectCharacter : ScriptableObject
{
    public Sprite[] Personas;
    public int[] Player;

    public void SelectP1(int i)
    {
        Player[0] = i;
    }

    public void SelectP2(int i)
    {
        Player[1] = i;
    }

    public Sprite GetPlayerSprite(int i)
    {
        if (Player.Length < i) return null;
        
        return Personas[Player[i]];
    }

}
