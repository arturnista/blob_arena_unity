using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform WeaponReference;
    public Transform SpawnBulletPoint;
    
    private Animator animator;

    private bool isEquiped;
    public bool IsEquiped { get => isEquiped; }

    public AudioClip atkSound;
    private AudioSource source;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        isEquiped = false;
    }

    public void Pickup(Transform weapon)
    {
        if (isEquiped) return;

        source = GetComponent<AudioSource>();

        weapon.SetParent(WeaponReference.transform);
        weapon.localPosition = Vector2.zero;
        isEquiped = true;

        Destroy(weapon.GetComponent<Rigidbody2D>());
        foreach (var item in weapon.GetComponents<CircleCollider2D>())
        {
            Destroy(item);
        }

        GameController.main.PickupWeapon();
    }

    public void Shoot()
    {
        animator.SetTrigger("atk");

        source.clip = atkSound;
        source.Play();

        GameObject obj = WeaponReference.GetChild(0).gameObject;
        Vector3 spawnPosition = SpawnBulletPoint.position;
        GameObject bullet = Instantiate(obj.GetComponent<Weapon>().BulletPrefab, spawnPosition, Quaternion.identity) as GameObject;
        
        Vector2 direction = (SpawnBulletPoint.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * 30f, ForceMode2D.Impulse);
        isEquiped = false;
        
        Destroy(obj);        
    }
   
}
