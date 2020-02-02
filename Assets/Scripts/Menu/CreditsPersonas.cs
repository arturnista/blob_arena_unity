using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPersonas : MonoBehaviour
{
    
    private Animator animator;
    public AudioClip Clip;
    private AudioSource source;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
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
        source.PlayOneShot(Clip);
    }

}
