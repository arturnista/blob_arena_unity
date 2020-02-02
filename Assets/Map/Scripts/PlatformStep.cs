using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStep : MonoBehaviour
{

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            animator.SetTrigger("Step");
        }
    }

}
