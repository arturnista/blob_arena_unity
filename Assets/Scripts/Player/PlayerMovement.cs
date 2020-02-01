using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public InputSchema Schema;

    public float MoveSpeed;
    public float Acceleration;
    public float JumpHeight;

    private float jumpForce;

    private new Rigidbody2D rigidbody;
    private Vector2 moveDirection;

    private Vector2 targetVelocity;
    private Vector2 velocity;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        jumpForce = Mathf.Sqrt(JumpHeight * 2f * -Physics2D.gravity.y);
    }

    void Update()
    {
        if (Schema.GetKey(Schema.Right))
        {
            moveDirection.x = 1f;
        }
        else if (Schema.GetKey(Schema.Left))
        {
            moveDirection.x = -1f;
        }
        else
        {
            moveDirection.x = 0f;
        }

        if (Schema.GetKeyDown(Schema.Up)) 
        {
            Vector2 velocity = rigidbody.velocity;
            velocity.y += jumpForce;
            rigidbody.velocity = velocity;
        }

        targetVelocity = moveDirection * MoveSpeed;
        velocity = Vector2.MoveTowards(velocity, targetVelocity, Acceleration * Time.deltaTime);
    }

    void FixedUpdate()
    {
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

}
