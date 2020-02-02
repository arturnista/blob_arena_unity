using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SelectCharacter selChar;
    public SpriteRenderer spr;

    public float MoveSpeed;
    public float Acceleration;
    public float JumpHeight;
    public float MaxJumpHeight;
    public LayerMask GroundMask;


    private InputSchema inputSchema;

    private float jumpForce;
    private float maxJumpForce;
    private Vector2 gravity;
    private float gravityModifier;
    public Animator anim;

    private new Rigidbody2D rigidbody;
    private float moveDirection;
    private float lookingDirection;

    private float targetSpeed;
    private Vector2 velocity;
    private Vector2 extraVelocity;
    public Vector2 Velocity { get => velocity + extraVelocity; }

    private float allowJumpTime;
    private bool isGrounded;
    public bool IsGrounded { get => isGrounded; }

    private bool isStopped;
    public bool IsStopped { get => isStopped; set => isStopped = value; }

    private Vector3 originalScale;

    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void Awake()
    {
        if (gameObject.tag =="p1")
        {
            spr.sprite = selChar.Personas[selChar.Player[0]];
        }
        else
        {
            spr.sprite = selChar.Personas[selChar.Player[1]];
        }
        
        inputSchema = GetComponent<Player>().Schema;
        rigidbody = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity;
        jumpForce = Mathf.Sqrt(JumpHeight * 2f * -gravity.y);
        maxJumpForce = Mathf.Sqrt(MaxJumpHeight * 2f * -gravity.y);
        lookingDirection = 1f;
        gravityModifier = 1.3f;
        originalScale = transform.localScale;

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (GroundMask);
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        if (!isStopped)
        {
            moveDirection = Input.GetAxisRaw(inputSchema.HorizontalAxis);

            if (moveDirection != 0 && isGrounded)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
            if (Mathf.Abs(moveDirection) > 0.4f) lookingDirection = moveDirection > 0 ? 1 : -1;

            if (inputSchema.GetKeyDown(inputSchema.Jump) && (isGrounded || allowJumpTime > 0f)) 
            {
                velocity.y = maxJumpForce;
                allowJumpTime = 0f;
                anim.SetTrigger("jumped");
            }
            else if (inputSchema.GetKeyUp(inputSchema.Jump) && velocity.y > jumpForce)
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            moveDirection = 0f;
        }
        
        if (allowJumpTime > 0) allowJumpTime -= Time.deltaTime;

        targetSpeed = moveDirection * MoveSpeed;
        velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, Acceleration * Time.deltaTime);
        extraVelocity = Vector2.MoveTowards(extraVelocity, Vector2.zero, rigidbody.drag * Time.deltaTime);

        FlipSprite();
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(originalScale.x * lookingDirection, originalScale.y, originalScale.z);
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

        // if (velocity.y < 0) gravityModifier = 2f;
        // else gravityModifier = 1f;

        velocity += gravityModifier * gravity * deltaTime;
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
                
                Vector2 currentNormal = hitBuffer[i].normal;
                if (currentNormal.y > .65f) 
                {
                    isGrounded = true;
                    allowJumpTime = .3f;
                }
                
                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }
                
                float modifiedDistance = hitBuffer[i].distance - shellRadius;
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