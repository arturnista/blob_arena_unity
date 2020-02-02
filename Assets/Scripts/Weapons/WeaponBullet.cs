using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{

    public GameObject SplashPrefab;
    public GameObject HitPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBag playerBag = collision.gameObject.GetComponent<PlayerBag>();
        if (playerBag != null)
        {
            playerBag.DropPeca(transform, 5f, true);
            GameObject hitCreated = Instantiate(HitPrefab, playerBag.transform) as GameObject;
            hitCreated.transform.localPosition = new Vector3(0f, .5f, 0f);
        }
        else
        {
            if (SplashPrefab != null)
            {
                Vector2 normal = collision.contacts[0].normal;
                float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;
                Instantiate(SplashPrefab, transform.position, Quaternion.Euler(0f, 0f, angle - 90f));
            }
        }
        Destroy(this.gameObject);
    }

}
