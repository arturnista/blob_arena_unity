using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InputSchema Schema;

    private AttackScript playerAttack;
    private weaponScript playerWeapon;
    private bool isAttacking;

    void Awake()
    {
        isAttacking = false;
        playerAttack = GetComponent<AttackScript>();
        playerWeapon = GetComponent<weaponScript>();
    }

    void Update()
    {
        if (Schema.GetKeyDown(Schema.Attack))
        {
            if (playerWeapon.IsEquiped) playerWeapon.Shoot();
            else
            {
                playerAttack.StartAttacking();
                isAttacking = true;
            }
        }
        if (Schema.GetKeyUp(Schema.Attack))
        {
            if (isAttacking) playerAttack.StopAttacking();
            isAttacking = false;
        }
    }
}
