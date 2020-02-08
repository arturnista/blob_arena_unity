using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponCollector : MonoBehaviour
{

    public LayerMask GroundMask;

    private PlayerWeapon PlayerWeapon;

    void Awake()
    {
        PlayerWeapon = GetComponentInParent<PlayerWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.WEAPON)
        {
            RaycastHit2D[] hits = Physics2D.LinecastAll(collision.transform.position, transform.position, GroundMask);
            if (hits.Length > 1) return;

            Transform weapon = collision.transform;
            PlayerWeapon.Pickup(weapon);
        }
    }
}
