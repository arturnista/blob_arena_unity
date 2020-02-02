using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public Transform weaponPos, spawnBulletPoint;
    public Animator anim;
    private bool isEquiped;
    public bool IsEquiped { get => isEquiped; }

    void Start()
    {
        isEquiped = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEquiped) return;
        if (collision.tag == "weapon")
        {
            

            Transform weapon = collision.transform;
            weapon.SetParent(weaponPos.transform);
            weapon.localPosition = Vector2.zero;
            isEquiped = true;

            Destroy(weapon.GetComponent<Rigidbody2D>());
            foreach (var item in weapon.GetComponents<CircleCollider2D>())
            {
                Destroy(item);
            }
        }
    }

    public void Shoot()
    {
        anim.SetTrigger("atk");
        
        GameObject obj = weaponPos.GetChild(0).gameObject;
        Vector3 spawnPosition = spawnBulletPoint.position;
        GameObject bullet = Instantiate(obj.GetComponent<Weapon>().BulletPrefab, spawnPosition, Quaternion.identity) as GameObject;
        
        Vector2 direction = (spawnBulletPoint.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * 30f, ForceMode2D.Impulse);
        isEquiped = false;
        
        Destroy(obj);        
    }
   
}
