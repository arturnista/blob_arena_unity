using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerSelect : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{

    public AudioClip Clip;
    private AudioSource source;
    
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;

    private Color selectedColor;
    private Color deselectedColor;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        selectedColor = Color.white;
        deselectedColor = Color.grey;

        Deselect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        Deselect();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("OnSelect");
        Select();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("OnDeselect");
        Deselect();
    }

    void Select()
    {
        source.PlayOneShot(Clip);
        animator.SetBool("isWalking", true);
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.white;
        }
    }

    void Deselect()
    {
        animator.SetBool("isWalking", false);
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = deselectedColor;
        }
    }

}
