using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPauseListener
{
    public InputSchema Schema;

    private PlayerAttack playerAttack;
    private PlayerWeapon playerWeapon;
    private bool isAttacking;
    public bool IsAttacking { get => isAttacking ; set => isAttacking = value ; }

    private bool isStopped;
    public bool IsStopped { get => isStopped; set => isStopped = value; }

    void Awake()
    {
        isAttacking = false;
        isStopped = false;
        playerAttack = GetComponent<PlayerAttack>();
        playerWeapon = GetComponent<PlayerWeapon>();
    }

    void Update()
    {
        if (isStopped) return;
        
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
        if (Schema.GetKeyUp(Schema.Pause))
        {
            PauseController.Instance.Pause(Schema);
        }
    }

    public void OnPause()
    {
        enabled = false;
    }

    public void OnResume()
    {
        enabled = true;
    }
}
