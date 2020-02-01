using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float minForce, maxForce, currentForce, chargeSpd;
    bool isCharging;
    Rigidbody2D rb;
    bool isP1;
    public bool isGrounded;
    public float atkRadius;
    public Transform atkOrigin;
    public Transform groundPos;
    public float checkGroundRadius;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isCharging = false;
        currentForce = minForce;
        isGrounded = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(groundPos.position, checkGroundRadius).tag == "floor")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
        }
        if (isCharging)
        {
            if (currentForce <= maxForce)
                currentForce += chargeSpd * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject opponent;
            if (transform.tag == "p1")
            {
                opponent = GameObject.FindGameObjectWithTag("p2");
            }
            else
            {
                opponent = GameObject.FindGameObjectWithTag("p1");
            }
            Attack(opponent, currentForce);
            //rb.AddForce(new Vector2(0, currentForce));
            currentForce = minForce;
            isCharging = false;

            Debug.Log("Velocity: " + rb.velocity);
        }

        Debug.Log("Force: " + currentForce);
        Debug.Log("isCharging: " + Input.GetKey(KeyCode.Space));
    }

    public void Attack(GameObject obj, float force)
    {
        Vector2 dir = (obj.transform.position - transform.position).normalized;

        Collider2D[] col = Physics2D.OverlapCircleAll(atkOrigin.position, atkRadius);

        foreach ( Collider2D c in col)
        {
            if (c.tag == obj.tag)
            {
                if(rb.velocity.y == 0 && obj.GetComponent<Rigidbody2D>().velocity.y == 0)
                    obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * force, Random.RandomRange(0.5f, 1.0f) * force));
                else
                    obj.GetComponent<Rigidbody2D>().AddForce(dir * force);
            }
        }

        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(atkOrigin.position, atkRadius);

        Gizmos.DrawWireSphere(groundPos.position, checkGroundRadius);
    }
}
