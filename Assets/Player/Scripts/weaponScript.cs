using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public Transform weaponPos, spawnBulletPoint;
    private bool isEquiped;
    public bool IsEquiped { get => isEquiped; }

    void Start()
    {
        isEquiped = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weapon")
        {
            Transform weapon = collision.transform;
            weapon.SetParent(weaponPos.transform);
            weapon.localPosition = Vector2.zero;
            isEquiped = true;

            Destroy(weapon.GetComponent<Rigidbody2D>());
            Destroy(weapon.GetComponent<CircleCollider2D>());
            Destroy(weapon.GetComponent<CircleCollider2D>());
        }
    }

    public void Shoot()
    {
        GameObject obj = weaponPos.GetChild(0).gameObject;
        GameObject bullet = Instantiate(obj.GetComponent<Weapon>().BulletPrefab, spawnBulletPoint.position, Quaternion.identity) as GameObject;
        
        Vector2 direction = (spawnBulletPoint.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * 20f, ForceMode2D.Impulse);
        isEquiped = false;
        
        Destroy(obj);        
    }
}
