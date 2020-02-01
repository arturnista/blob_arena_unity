using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public LayerMask PlayerMask;

    public float minForce, maxForce, currentForce, chargeSpd;
    bool isCharging;
    Rigidbody2D rb;
    bool isP1;
    public float atkRadius;
    public Transform atkOrigin;
    private PlayerMovement movement;
    private InputSchema inputSchema;
    private float originalMoveSpeed;

    private bool isReady;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        isCharging = false;
        currentForce = minForce;
        inputSchema = GetComponent<Player>().Schema;
        originalMoveSpeed = movement.MoveSpeed;
        isReady = true;
    }

    void Update()
    {
        if (isCharging)
        {
            if (currentForce <= maxForce)
            {
                currentForce += chargeSpd * Time.deltaTime;
            }
        }
    }

    public void StartAttacking()
    {
        isCharging = true;
        movement.MoveSpeed = originalMoveSpeed / 2f;
    }

    public void StopAttacking()
    {
        Attack(currentForce);
        currentForce = minForce;
        isCharging = false;
        movement.MoveSpeed = originalMoveSpeed;
    }

    public void Attack(float force)
    {
        if (!isReady) return;
        Collider2D[] col = Physics2D.OverlapCircleAll(atkOrigin.position, atkRadius, PlayerMask);

        foreach ( Collider2D colliderHit in col)
        {
            if (colliderHit.gameObject == gameObject) continue;
            colliderHit.gameObject.GetComponent<PlayerBag>().DropPeca(transform, force, true);
        }

        isReady = false;
        StartCoroutine(ReadyCoroutine());
    }

    IEnumerator ReadyCoroutine()
    {
        yield return new WaitForSeconds(.6f);
        SetReady();
    }

    public void SetReady()
    {
        isReady = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(atkOrigin.position, atkRadius);
    }
}
