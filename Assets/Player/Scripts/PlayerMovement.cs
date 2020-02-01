using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.03f;
    
    public InputSchema Schema;

    public float MoveSpeed;
    public float Acceleration;
    public float JumpHeight;
    public float AirDrag;
    public LayerMask GroundMask;

    private float jumpForce;
    private Vector2 gravity;

    private new Rigidbody2D rigidbody;
    private float moveDirection;
    private float lookingDirection;
    public Vector2 LookingDirection { get => lookingDirection * Vector2.right; }

    private float targetSpeed;
    private Vector2 moveVelocity;
    private Vector2 extraVelocity;
    public Vector2 Velocity { get => moveVelocity + extraVelocity; }

    private bool isGrounded;
    public bool IsGrounded { get => isGrounded; }


    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity;
        jumpForce = Mathf.Sqrt(JumpHeight * 2f * -gravity.y);

        lookingDirection = 1f;

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (GroundMask);
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        moveDirection = Input.GetAxisRaw(Schema.HorizontalAxis);
        if (moveDirection != 0) lookingDirection = moveDirection; 

        if (Schema.GetKeyDown(Schema.Jump) && isGrounded) 
        {
            moveVelocity.y = jumpForce;
            isGrounded = false;
        }

        targetSpeed = moveDirection * MoveSpeed;
        moveVelocity.x = Mathf.MoveTowards(moveVelocity.x, targetSpeed, Acceleration * Time.deltaTime);
        extraVelocity.x = Mathf.MoveTowards(extraVelocity.x, 0f, AirDrag * Time.deltaTime);

        FlipSprite();
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(lookingDirection, 1f, 1f);
    }

    void FixedUpdate()
    {   
        Vector2 motion = MovePlayer(Time.fixedDeltaTime);

        rigidbody.MovePosition(rigidbody.position + motion);
    }

    Vector2 MovePlayer(float deltaTime)
    {
        isGrounded = false;
        Vector2 finalVelocity = moveVelocity + extraVelocity;
        Vector2 deltaPosition = finalVelocity * deltaTime;

        Vector2 vMove = Vector2.up * deltaPosition.y;
        Vector2 vMovement = Movement (vMove, ref moveVelocity);

        Vector2 hMove = Vector2.right * deltaPosition.x;
        Vector2 hMovement = Movement (hMove, ref moveVelocity);

        moveVelocity += gravity * deltaTime;
        extraVelocity += gravity * deltaTime;

        moveVelocity.y = Mathf.Clamp(moveVelocity.y, -50.0f, float.PositiveInfinity);
        extraVelocity.y = Mathf.Clamp(extraVelocity.y, -50.0f, float.PositiveInfinity);

        return vMovement + hMovement;
    }

    Vector2 Movement(Vector2 move, ref Vector2 velocity)
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

    public void AddExtraVelocity(Vector2 velocity)
    {
        extraVelocity += velocity;
    }

}
