using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptTie : MonoBehaviour
{
    public SelectCharacter selChar;
    public Sprite spr;
    SpriteRenderer sprRnder;
    // Start is called before the first frame update
    void Start()
    {
        sprRnder = GetComponent<SpriteRenderer>();

        if (selChar.Player[0] == selChar.Player[1])
        {
            sprRnder.sprite = spr;
        }
        
    }

    // Update is called once per frame
    
}
