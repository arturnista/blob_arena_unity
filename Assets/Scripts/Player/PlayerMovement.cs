using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public InputSchema Schema;

    public float MoveSpeed;
    public float Acceleration;
    public float JumpHeight;
    public LayerMask GroundMask;

    private float jumpForce;
    private Vector2 gravity;

    private new Rigidbody2D rigidbody;
    private float moveDirection;

    private float targetSpeed;
    private Vector2 velocity;

    private bool isGrounded;

    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.03f;

    private Vector2 nextMovement;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity;
        jumpForce = Mathf.Sqrt(JumpHeight * 2f * -gravity.y);

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (GroundMask);
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        // if (Schema.GetKey(Schema.Right))
        // {
        //     moveDirection = 1f;
        // }
        // else if (Schema.GetKey(Schema.Left))
        // {
        //     moveDirection = -1f;
        // }
        // else
        // {
        //     moveDirection = 0f;
        // }

        if (Schema.GetKeyDown(Schema.Jump) && isGrounded) 
        {
            velocity.y = jumpForce;
            isGrounded = false;
        }

        targetSpeed = moveDirection * MoveSpeed;
        velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, Acceleration * Time.deltaTime);

    }

    void FixedUpdate()
    {   
        MovePlayer(Time.fixedDeltaTime);
        
        rigidbody.MovePosition(rigidbody.position + nextMovement);
        nextMovement = Vector2.zero;
    }

    void MovePlayer(float deltaTime)
    {
        isGrounded = false;
        Vector2 deltaPosition = velocity * deltaTime;

        Vector2 vMove = Vector2.up * deltaPosition.y;
        Vector2 vMovement = Movement (vMove);

        Vector2 hMove = Vector2.right * deltaPosition.x;
        Vector2 hMovement = Movement (hMove);

        nextMovement += vMovement + hMovement;
        
        velocity += gravity * deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, -50.0f, float.PositiveInfinity);
    }

    Vector2 Movement(Vector2 move)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance) 
        {
            int count = rigidbody.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
            
            for (int i = 0; i < count; i++) 
            {
                
                Vector2 currentNormal = hitBuffer [i].normal;
                if (currentNormal.y > .65f) 
                {
                    isGrounded = true;
                }

                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBuffer [i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;

            }
        }

        return move.normalized * distance;
    } 

}
