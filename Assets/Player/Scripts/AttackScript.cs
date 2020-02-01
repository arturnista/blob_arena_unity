using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public InputSchema Schema;
    public LayerMask PlayerMask;

    public float minForce;
    public float maxForce;
    public float chargeSpeed;
    public float attackRadius;
    public Transform attackOrigin;

    private float currentForce;
    private bool isCharging;
    private Rigidbody2D rb;

    private PlayerMovement movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        isCharging = false;
    }

    void Update()
    {
        if (Schema.GetKeyDown(Schema.Attack))
        {
            currentForce = minForce;       
            isCharging = true;
        }

        if (isCharging)
        {
            if (currentForce <= maxForce)
            {
                currentForce += chargeSpeed * Time.deltaTime;
            }
        }

        if (Schema.GetKeyUp(Schema.Attack))
        {
            Vector3 origin = transform.position + (Vector3)movement.LookingDirection;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, PlayerMask);
            foreach (Collider2D item in colliders)
            {
                Attack(item.gameObject, currentForce);
            }
            //rb.AddForce(new Vector2(0, currentForce));
            currentForce = minForce;
            isCharging = false;

            Debug.Log("Velocity: " + rb.velocity);
        }

    }

    public void Attack(GameObject target, float force)
    {
        Vector2 dir = (target.transform.position - transform.position).normalized;
        // target.GetComponent<PlayerMovement>().AddExtraVelocity(new Vector2(dir.x * force, Random.Range(0.5f, 1.0f) * force));
        PlayerMovement targetMovement = target.GetComponent<PlayerMovement>();
        if(targetMovement.IsGrounded && movement.IsGrounded) {
            targetMovement.AddExtraVelocity(new Vector2(dir.x * force, Random.Range(0.5f, 1.0f) * force));
        } else {
            targetMovement.AddExtraVelocity(dir * force);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}
