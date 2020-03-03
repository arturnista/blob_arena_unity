using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pausable))]
public class PausableRigidbody : MonoBehaviour, IPauseListener
{

    private Vector2 originalVelocity;
    private float originalAngularVelocity;
    
    public void OnPause()
    {
        var rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null) return;
        originalVelocity = rigidbody.velocity;
        originalAngularVelocity = rigidbody.angularVelocity;
        rigidbody.bodyType = RigidbodyType2D.Static;
    }

    public void OnResume()
    {   
        var rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null) return;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.velocity = originalVelocity;
        rigidbody.angularVelocity = originalAngularVelocity;
    }

}
