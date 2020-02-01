using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBag playerBag = collision.GetComponent<PlayerBag>();
        if (playerBag != null)
        {
            playerBag.DropPeca(transform, 5f);
        } 
        Destroy(this.gameObject);
    }

}
