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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        isCharging = false;
        currentForce = minForce;
        inputSchema = GetComponent<Player>().Schema;
       
    }

    // Update is called once per frame
    void Update()
    {

        if (inputSchema.GetKeyDown(inputSchema.Attack))
        {
            isCharging = true;
        }
        if (isCharging)
        {
            if (currentForce <= maxForce)
                currentForce += chargeSpd * Time.deltaTime;
        }
        if (inputSchema.GetKeyUp(inputSchema.Attack))
        {
            Attack(currentForce);
            currentForce = minForce;
            isCharging = false;
        }
    }

    public void Attack(float force)
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(atkOrigin.position, atkRadius, PlayerMask);

        foreach ( Collider2D colliderHit in col)
        {
            if (colliderHit.gameObject == gameObject) continue;
            
            PlayerMovement movementHit = colliderHit.gameObject.GetComponent<PlayerMovement>();
            Vector2 dir = (movementHit.transform.position - transform.position).normalized;

            if(movement.IsGrounded && movementHit.IsGrounded)
                movementHit.AddExtraVelocity(new Vector2(dir.x * force, Random.Range(0.5f, 1.0f) * force));
            else
                movementHit.AddExtraVelocity(dir * force);

            colliderHit.gameObject.GetComponent<PlayerBag>().DropPeca(dir.x > 0 ? 1 : 0);
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(atkOrigin.position, atkRadius);
    }
}
