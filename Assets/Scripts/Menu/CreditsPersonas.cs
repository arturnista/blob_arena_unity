using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPersonas : MonoBehaviour
{
    
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OnMouseEnter()
    {
        animator.SetBool("isWalking", true);
    }

    public void OnMouseExit()
    {
        animator.SetBool("isWalking", false);
    }

    public void OnMouseDown()
    {
        animator.SetTrigger("tookDmg");
    }

}
