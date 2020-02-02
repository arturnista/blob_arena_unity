using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca : MonoBehaviour
{
    
    private new Collider2D collider;
    void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    public void DisableForTime()
    {
        StartCoroutine(DisableCoroutine());
    }

    IEnumerator DisableCoroutine()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(.2f);
        collider.enabled = true;
    }

}
