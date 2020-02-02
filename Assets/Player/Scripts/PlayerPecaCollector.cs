using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPecaCollector : MonoBehaviour
{
    public LayerMask GroundMask;

    private PlayerBag playerBag;

    void Awake()
    {
        playerBag = GetComponentInParent<PlayerBag>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Peca")
        {
            RaycastHit2D[] hits = Physics2D.LinecastAll(collision.transform.position, transform.position, GroundMask);
            if (hits.Length > 0) return;

            Transform peca = collision.transform;
            playerBag.CollectPeca(peca);
        }
    }
}
