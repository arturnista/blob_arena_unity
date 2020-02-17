using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float MinForce;
    public float MaxForce;
    public float ChargeSpeed;
    public float atkRadius;
    public float deflectHeight, deflectWidth;
    public Transform AttackReference;
    public AudioClip AttackSound;
    public LayerMask PlayerMask;
    public LayerMask BulletMask;

    private float originalMoveSpeed;
    private float currentForce;

    private bool isReady;
    public bool IsReady { get => isReady ; set => isReady = value; }
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
            animator.SetBool("isCharging", isCharging);

            if (currentForce <= MaxForce)
            {
                currentForce += ChargeSpeed * Time.deltaTime;
            }
        }
        else
            movement.MoveSpeed = originalMoveSpeed;
        if(!isReady)
        {
            StartCoroutine(ReadyCoroutine());
            currentForce = MinForce;
            GetComponent<Player>().IsAttacking = false;
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
        if (!isReady) return;
        DeflectBullet(currentForce);
        Attack(currentForce);
        currentForce = MinForce;
        isCharging = false;
        animator.SetBool("isCharging", isCharging);
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

            if (colliderHit.tag == "Bullet")
            {
                Debug.Log ("Acertou");

                int whichPlayer = 1;
                if(gameObject.tag == "Player1")
                {
                    whichPlayer = 2;
                }

                GameObject opponent = GameObject.FindGameObjectWithTag("Player" + whichPlayer);
                Vector2 direction = (transform.position - opponent.transform.position).normalized;
                colliderHit.GetComponent<Rigidbody2D>().AddForce(direction * 30f, ForceMode2D.Impulse);
            }

            if (force >= MaxForce)
            {
                bag.DropPeca(transform, force, true);
            }
        }

        source.clip = AttackSound;
        source.Play();


        isReady = false;
        
    }


    public void DeflectBullet(float force)
    {
        if (!isReady) return;
        Collider2D[] col = Physics2D.OverlapBoxAll(AttackReference.position, new Vector2(deflectWidth, deflectHeight), 0,BulletMask);

        foreach ( Collider2D colliderHit in col)
        {

            int whichPlayer = 1;
            if(gameObject.tag == "Player1")
            {
                 whichPlayer = 2;
            }

            GameObject opponent = GameObject.FindGameObjectWithTag("Player" + whichPlayer);
            Vector2 direction = (transform.position - opponent.transform.position).normalized;
            colliderHit.GetComponent<Rigidbody2D>().AddForce(new Vector2(-direction.x * (force + 60f), force), ForceMode2D.Impulse);

        }
    }

    IEnumerator ReadyCoroutine()
    {
        yield return new WaitForSeconds(.6f);
        if(!isReady) SetReady();
    }

    public void SetReady()
    {
        isReady = true;
    }
    

    public void changeChargeState()
    {
        isCharging = !isCharging;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(AttackReference.position, atkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackReference.position, new Vector2(deflectWidth, deflectHeight));
    }
}
