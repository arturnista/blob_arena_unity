using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeSprite : MonoBehaviour
{
    
    public SelectCharacter SelectCharacter;
    public SpriteRenderer BodySpriteRenderer;

    void Start()
    {
        if (gameObject.tag == Tags.PLAYER1)
        {
            BodySpriteRenderer.sprite = SelectCharacter.Personas[SelectCharacter.Player[0]];
        }
        else
        {
            BodySpriteRenderer.sprite = SelectCharacter.Personas[SelectCharacter.Player[1]];
        }    
    }
    
}
