using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSpeed;
    public float Acceleration;
    public float JumpHeight;
    public LayerMask GroundMask;

    private InputSchema inputSchema;

    private float jumpForce;
    private Vector2 gravity;

    private new Rigidbody2D rigidbody;
    private float moveDirection;
    private float lookingDirection;

    private float targetSpeed;
    private Vector2 velocity;
    private Vector2 extraVelocity;
    public Vector2 Velocity { get => velocity + extraVelocity; }

    private bool isGrounded;
    public bool IsGrounded { get => isGrounded; }

    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.03f;

    void Awake()
    {
        inputSchema = GetComponent<Player>().Schema;
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
        moveDirection = Input.GetAxisRaw(inputSchema.HorizontalAxis);
        if (Mathf.Abs(moveDirection) > 0.4f) lookingDirection = moveDirection > 0 ? 1 : -1;

        if (inputSchema.GetKeyDown(inputSchema.Jump) && isGrounded) 
        {
            velocity.y = jumpForce;
            isGrounded = false;
        }

        targetSpeed = moveDirection * MoveSpeed;
        velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, Acceleration * Time.deltaTime);
        extraVelocity = Vector2.MoveTowards(extraVelocity, Vector2.zero, rigidbody.drag * Time.deltaTime);

        FlipSprite();
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(lookingDirection, 1f, 1f);
    }

    void FixedUpdate()
    {   
        isGrounded = false;
        Vector2 moveMotion = MovePlayer(Time.fixedDeltaTime, ref velocity);
        Vector2 extraMoveMotion = MovePlayer(Time.fixedDeltaTime, ref extraVelocity);

        rigidbody.MovePosition(rigidbody.position + moveMotion + extraMoveMotion);
    }

    Vector2 MovePlayer(float deltaTime, ref Vector2 velocity)
    {
        Vector2 deltaPosition = velocity * deltaTime;

        Vector2 vMove = Vector2.up * deltaPosition.y;
        Vector2 vMovement = Movement (vMove, ref velocity);

        Vector2 hMove = Vector2.right * deltaPosition.x;
        Vector2 hMovement = Movement (hMove, ref velocity);

        velocity += gravity * deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, -50.0f, float.PositiveInfinity);

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