using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponCollector : MonoBehaviour
{

    public LayerMask GroundMask;

    private weaponScript PlayerWeapon;

    void Awake()
    {
        PlayerWeapon = GetComponentInParent<weaponScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weapon")
        {
            RaycastHit2D[] hits = Physics2D.LinecastAll(collision.transform.position, transform.position, GroundMask);
            Debug.Log(hits.Length);
            if (hits.Length > 1) return;

            Transform weapon = collision.transform;
            PlayerWeapon.Pickup(weapon);
        }
    }
}
