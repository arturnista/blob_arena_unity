using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float MinForce;
    public float MaxForce;
    public float ChargeSpeed;
    public float atkRadius;
    public Transform AttackReference;
    public AudioClip AttackSound;
    public LayerMask PlayerMask;

    private float originalMoveSpeed;
    private float currentForce;

    private bool isReady;
    private bool isCharging;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private PlayerMovement movement;
    private InputSchema inputSchema;
    private AudioSource source;


    void Start()
    {
        source = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
        inputSchema = GetComponent<Player>().Schema;

        isCharging = false;
        currentForce = MinForce;
        originalMoveSpeed = movement.MoveSpeed;
        isReady = true;
    }

    void Update()
    {
        if (isCharging)
        {
            animator.SetBool("isCharging", true);

            if (currentForce <= MaxForce)
            {
                currentForce += ChargeSpeed * Time.deltaTime;
            }
        }
    }

    public void StartAttacking()
    {
        if (!isReady) return;
        isCharging = true;
        movement.MoveSpeed = originalMoveSpeed / 2f;
    }

    public void StopAttacking()
    {
        Attack(currentForce);
        currentForce = MinForce;
        isCharging = false;
        animator.SetBool("isCharging", false);
        movement.MoveSpeed = originalMoveSpeed;
    }

    public void Attack(float force)
    {
        if (!isReady) return;
        Collider2D[] col = Physics2D.OverlapCircleAll(AttackReference.position, atkRadius, PlayerMask);

        foreach ( Collider2D colliderHit in col)
        {
            if (colliderHit.gameObject == gameObject) continue;
            PlayerBag bag = colliderHit.gameObject.GetComponent<PlayerBag>();
            bag.DropPeca(transform, force, true);
            if (force >= MaxForce)
            {
                bag.DropPeca(transform, force, true);
            }
        }

        source.clip = AttackSound;
        source.Play();


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
        Gizmos.DrawWireSphere(AttackReference.position, atkRadius);
    }
}
