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
            Vector2 dir = (playerBag.transform.position - transform.position).normalized;
            playerBag.DropPeca(dir.x > 0 ? 1 : 0);
        } 
        Destroy(this.gameObject);
    }

}
